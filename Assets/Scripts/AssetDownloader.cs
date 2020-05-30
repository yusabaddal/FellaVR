using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetDownloader : MonoBehaviour {

    public Transform Content;
    public SceneModel scene;
	// Use this for initialization
	void Start () {
        //StartCoroutine(LoadBundle());
        getScene();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void getScene()
    {
        StartCoroutine(_getscene("1"));
    }
    IEnumerator _LoadBundle()
    {

        
        while (!Caching.ready)
        {
            yield return null;
        }

        string url = "http://files.production.cmosteknoloji.com/Assets/AskiAsset.unity3d";
      
        
        //Begin download
        WWW www = WWW.LoadFromCacheOrDownload(url, 0);

        yield return www;
        if (www.assetBundle != null)
        {
            //Load the downloaded bundle
            AssetBundle bundle = www.assetBundle;
            //Load an asset from the loaded bundle

            AssetBundleRequest bundleRequest = bundle.LoadAssetAsync("AskiAsset.prefab", typeof(GameObject));
            yield return bundleRequest;
           
            if (bundleRequest.asset != null)
            {
                GameObject obj = Instantiate(bundleRequest.asset as GameObject,Content);

            }
            else
            {
                //item bulunamadı
                Debug.Log("model bulunamadı.");
               
            }

            bundle.Unload(false);

        }
        else
        {
            // koleksiyon bulunamadı
            Debug.Log("Ürün dosyası bulunamadı");
         
        }
        www.Dispose();
    
    }
    IEnumerator _getscene(string id)
    {
        PostCtrl post = new PostCtrl();
        //error.text = "Giriş Yapılıyor";

        yield return StartCoroutine(post.gettData(EndPoint.getscene,id ));

        if (post.resultObj.responseCode != 200)  //on server fail
        {
            //error.text = post.resultObj.error;

        }
        else //on server success
        {
            string resultStr = post.resultObj.downloadHandler.text;

            var res = JsonConvert.DeserializeObject<Response>(resultStr);
            if (resultStr != "0")
            {

            }
            else
            {

            }
        }


    }

}
