using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public enum EndPoint
{
   login,
   getscene,
   getUserScenes,
   getpodium,
   addFav,
   deleteFav
    
}

public class PostCtrl
{
    string server = "https://www.fellavr.com";

    string LoginEndpoint = "/Users_Login";
    string GetSceneEndpoint = "/DownloadGame/";
    string GetUserScenesEndpoint = "/Scenarios_GetListForGame/";
    string GetPodiumEndpoint = "/AllTypes";
    string AddFavEndpoint = "/Varyants_AddFavorite";
    string DeleteFavEndpoint = "/Varyants_DeleteFavorite";



    public UnityWebRequest resultObj;

 	public string GetEndPointURL (EndPoint endPointType){
		switch (endPointType) {
			case EndPoint.login: return server + LoginEndpoint;
            case EndPoint.getscene: return server + GetSceneEndpoint;
            case EndPoint.getUserScenes: return server + GetUserScenesEndpoint;
            case EndPoint.getpodium: return server + GetPodiumEndpoint;
            case EndPoint.addFav: return server + AddFavEndpoint;
            case EndPoint.deleteFav: return server + DeleteFavEndpoint;
            default:
				return "";
		}
	}
       
	public IEnumerator postData(EndPoint endPointType, string jsonData){

		string url = GetEndPointURL(endPointType);
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
		request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        //request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("Token"));
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");
        request.chunkedTransfer = false;

        yield return request.SendWebRequest();
		resultObj = request;

        //Debug.Log(request);

        if (request.isNetworkError)
		{
			//Debug.Log(request.error);
		}
		else
		{
            //Debug.Log(request.downloadHandler.text);
        }
    }
    public IEnumerator gettData(EndPoint endPointType, string jsonData)
    {

        string url = GetEndPointURL(endPointType)+jsonData;
        
        UnityWebRequest request = new UnityWebRequest(url, "GET");
        //request.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("Token"));
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.Send();
        resultObj = request;

        //Debug.Log(request);

        if (request.isNetworkError)
        {
            //Debug.Log(request.error);
        }
        else
        {
            //Debug.Log(request.downloadHandler.text);
        }
    }

}