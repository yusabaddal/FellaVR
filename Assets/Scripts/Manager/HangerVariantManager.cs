using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HangerVariant
{
    public int productID;
    public string productCode;
    public string productName;
    public Sprite productImage;
    public GameObject productModel;
    public GameObject avatarModel;
    public bool isFavourite;
}
[System.Serializable]
public class VariantColor
{
    public string color_name;
    public string color_code;

    public List<HangerVariant> variantList;
}

public class HangerVariantManager : MonoBehaviour {
    
    int currentSelected;
    int currentColor;
    public GameObject favouriteBut,unfavoriteBut;
    public HangerUIManager uımanager;

    public Transform colorContent;

    [Header("-------------------------------***************-------------------------------")]

    [Header("Variant Settings")]
    public List<VariantColor> ListModel;
   
    
    // Use this for initialization
    void Start () {
		
	}

    public void showSelectedModel(int selected)
    {
        currentSelected = selected;
        closeAllObjects();
        if (ListModel[currentColor].variantList != null && ListModel[currentColor].variantList.Count!=0)
        {
            if (ListModel[currentColor].variantList[selected].productModel != null)
            {
                ListModel[currentColor].variantList[selected].productModel.SetActive(true);
            }
        }
        getFavourite();
    }

    public void showSelectedColor(int selected)
    {
        currentColor = selected;
        fillUI();
    }
    void closeAllObjects()
    {
        for(int i = 0; i < ListModel.Count; i++)
        {
            foreach (var obj in ListModel[i].variantList)
            {
                if (obj.productModel != null)
                    obj.productModel.SetActive(false);
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherCloth = other.transform.GetComponent<ClothesHolder>();
        if (otherCloth != null)
        {
            otherCloth.findClotes(ListModel[currentColor].variantList[currentSelected].productCode);
        }
    }

    public void setUIActive(bool state)
    {
        if (uımanager != null)
            uımanager.gameObject.SetActive(state);
    }

    public void fillVariant()
    {

        for (int i = 0; i < this.ListModel.Count; i++)
        {
            if (this.ListModel[i].variantList.Count == 0)
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }
            for ( int j=0;j < this.ListModel[i].variantList.Count;j++)
            {
                var modelVariant = this.ListModel[i].variantList[j];
                
                for(int a = 0; a < GameManager.instance.assetsContent.childCount; a++)
                {
                    if (GameManager.instance.assetsContent.GetChild(a).gameObject.name.ToString() == modelVariant.productCode + "_Aski")
                    {
                        uımanager.buttonNames.Add(new buttonModel { buttonName= modelVariant.productCode } );
                        GameObject obj = Instantiate(GameManager.instance.assetsContent.GetChild(a).gameObject, this.transform);
                        obj.transform.localPosition = new Vector3(0, 0, 0);
                        var selfVariant = obj.transform.GetComponent<VariantManager>();
                        if (selfVariant == null)
                        {
                            selfVariant = obj.AddComponent<VariantManager>();
                        }
                        selfVariant.selfVariant = new HangerVariant {
                            productCode=modelVariant.productCode,
                            productName=modelVariant.productName,
                            productModel=obj,
                            productID=modelVariant.productID
                        };
                        var col =obj.AddComponent<BoxCollider>();
                        col.isTrigger = true;
                        col.center = new Vector3(0, 5, 0);
                        Debug.Log(modelVariant.productCode);
                        modelVariant.productModel = obj;
                        if (a > 0)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
              
            }
        }
        fillUI();
    }
    void fillUI()
    {
        uımanager.buttonNames = new List<buttonModel>();
        for (int j = 0; j < this.ListModel[currentColor].variantList.Count; j++)
        {
            var modelVariant = this.ListModel[currentColor].variantList[j];

            for (int a = 0; a < GameManager.instance.assetsContent.childCount; a++)
            {
                if (GameManager.instance.assetsContent.GetChild(a).gameObject.name.ToString() == modelVariant.productCode + "_Aski")
                {
                    uımanager.buttonNames.Add(new buttonModel { buttonName = modelVariant.productCode });
                }
            }

        }
        //}
        showSelectedModel(0);
        uımanager.setUI();
        Color color;
        ColorUtility.TryParseHtmlString("#" + ListModel[currentColor].color_code, out color);
        //System.Windows.Media.Color color = (System.Windows.Media.Color)ColorConverter.ConvertFromString("#"+ListModel[currentColor].ColorName);
        //uımanager.setColor(new UnityEngine.Color(color.R,color.G,color.B));
        uımanager.setColor(color);
    }

    public void fillColor()
    {
        for(int i = 0; i < ListModel.Count; i++)
        {
            colorContent.GetChild(i).gameObject.SetActive(true);
            colorContent.GetChild(i).GetComponentInChildren<Text>().text = ListModel[i].color_name;
            Color color;
            ColorUtility.TryParseHtmlString("#" + ListModel[i].color_code, out color);
            var colormat = colorContent.GetChild(i).GetChild(0).GetComponent<MeshRenderer>();
            for (int c = 0; c < colormat.materials.Length; c++)
            {
                colormat.materials[c] = new Material(GameManager.instance.shader);
                //colormat.materials[c].color = color;
                colormat.materials[c].SetColor("_EmissionColor", color);

            }
        }
        fillVariant();
    }

    public void setFavourite(bool state)
    {
        ListModel[currentColor].variantList[currentSelected].isFavourite=state;
        getFavourite();
        FavoritePostModel FPM = new FavoritePostModel();
        FPM.id = ListModel[currentColor].variantList[currentSelected].productID;
        FPM.scenario_id =PlayerPrefs.GetInt("GameID");
        FPM.favorite_user = PlayerPrefs.GetInt("UserID");
        if (state)
        {
            StartCoroutine(addFav(FPM));
        }
        else
        {
            StartCoroutine(deleteFav(FPM));
        }
    }
    public void getFavourite()
    {
        bool isfavorite = ListModel[currentColor].variantList[currentSelected].isFavourite;
        favouriteBut.SetActive(isfavorite);
        unfavoriteBut.SetActive(!isfavorite);
    }

    public IEnumerator addFav(FavoritePostModel data)
    {

        Debug.Log(JsonUtility.ToJson(data));

        PostCtrl post = new PostCtrl();
        //error.text = "Giriş Yapılıyor";
        //error.text = "Giriş Yapılıyor";
        
        yield return StartCoroutine(post.postData(EndPoint.addFav, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)  //on server fail
        {
            //error.text = "Hata";
            Debug.Log(post.resultObj.downloadHandler.text);
        }
        else //on server success
        {
          
        }


    }
    public IEnumerator deleteFav(FavoritePostModel data)
    {

        Debug.Log(JsonUtility.ToJson(data));

        PostCtrl post = new PostCtrl();
        //error.text = "Giriş Yapılıyor";
        //error.text = "Giriş Yapılıyor";

        yield return StartCoroutine(post.postData(EndPoint.deleteFav, JsonConvert.SerializeObject(data)));

        if (post.resultObj.responseCode != 200)  //on server fail
        {
            //error.text = "Hata";
            Debug.Log(post.resultObj.downloadHandler.text);
        }
        else //on server success
        {

        }


    }

}
