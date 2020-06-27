using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{

    public ColorVaryant selfVariant;
    public int hangerLocation,hangerPos;
    public int selfColor;
    public GameObject selfObject, selfAvatar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void prepareModel()
    {
        if(selfVariant.varyant_model!=null || selfVariant.varyant_model != "")
        {
            StartCoroutine(this._LoadBundle(selfVariant.varyant_model, true,selfVariant.varyant_name+"_Aski"));
        }
        if (selfVariant.varyant_manken_model != null || selfVariant.varyant_manken_model != "")
        {
            StartCoroutine(this._LoadBundle(selfVariant.varyant_manken_model, false, selfVariant.varyant_name + "_Avatar"));
        }
    }
    void checkModels()
    {
        if(selfObject!=null && selfAvatar != null)
        {
            this.findSelfHanger();
        }
    }


    IEnumerator _LoadBundle(string url,bool isObject,string itemname)
    {


        while (!Caching.ready)
        {
            yield return null;
        }

        Debug.Log(url);

        //Begin download
        WWW www = WWW.LoadFromCacheOrDownload(url, selfVariant.varyant_version);

        yield return www;
        if (www.assetBundle != null)
        {
            //Load the downloaded bundle
            AssetBundle bundle = www.assetBundle;
            //Load an asset from the loaded bundle

            AssetBundleRequest bundleRequest = bundle.LoadAssetAsync(itemname+".prefab", typeof(GameObject));
            yield return bundleRequest;

            if (bundleRequest.asset != null)
            {
               
                var obj = Instantiate(bundleRequest.asset as GameObject,GameManager.instance.assetsContent);
                if (obj == null)
                {
                    obj = new GameObject();
                }
                 obj.name = itemname;
                if (isObject)
                {
                    selfObject = obj;
                }
                else
                {
                    selfAvatar = obj;
                }

            }
            else
            {
                //item bulunamadı
                Debug.Log("model bulunamadı.");
                if (isObject)
                {
                    selfObject = new GameObject();
                }
                else
                {
                    selfAvatar = new GameObject();
                }

            }

            bundle.Unload(false);

        }
        else
        {
            // koleksiyon bulunamadı
            Debug.Log("Ürün dosyası bulunamadı");

            if (isObject)
            {
                selfObject = new GameObject();
            }
            else
            {
                selfAvatar = new GameObject();
            }

        }
        www.Dispose();
       this.checkModels();

    }


    public void findSelfHanger()
    {
        foreach(var hanger in GameManager.instance.hangerManagers)
        {
            if (hanger.location == hangerLocation)
            {
               if(hanger.HangerList[hangerPos].ListModel.Count>0)
                {
                    try
                    {
                        var currenthanger = hanger.HangerList[hangerPos].ListModel[selfColor].variantList;
                        if (currenthanger != null)
                        {
                            currenthanger.Add(new HangerVariant
                            {
                                productCode = this.selfVariant.varyant_name,
                                productName = this.selfVariant.varyant_name,
                                productID = this.selfVariant.varyant_id
                            });
                            Debug.Log("Founded Hanger");

                        }
                    }
                    catch(Exception e) {
                        Debug.Log(this.selfVariant.varyant_name);
                    }
                    
                }
                
            }
        }

        GameManager.instance.assetDownloaded();
    }
}
