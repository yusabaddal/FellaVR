using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<HangerManager> hangerManagers;
    public List<ScenarioTree> resp;

    public GameObject assetPrefab;
    public Transform assetsContent;
    private int totalAssetCount,downloadedAssetCount;
    public List<ColorVaryant> productList;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        getScene();
        resp = new List<ScenarioTree>();
        if (productList == null)
            productList = new List<ColorVaryant>();

        //fillHangers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    void fillHangers()
    {
        Debug.Log("fill");
        totalAssetCount = 0;
        downloadedAssetCount = 0;
        for(int i = 0; i < resp.Count; i++)
        {
            ScenarioTree respHangermanager = resp[i];
            
            for(int j=0;j< respHangermanager.tree_products.Count; j++)
            {
                TreeProduct respHanger = respHangermanager.tree_products[j];
               

                for (int c = 0; c < respHanger.product_colors.Count; c++)
                {
                    var colorvariant = respHanger.product_colors[c];

                    for(int v = 0; v < colorvariant.color_varyant.Count; v++)
                    {
                        GameObject obj = Instantiate(assetPrefab);
                        totalAssetCount++;
                        obj.name = respHanger.product_name;
                        AssetManager assetman = obj.transform.GetComponent<AssetManager>();
                        productList.Add(colorvariant.color_varyant[v]);
                        if (assetman == null)
                        {
                            assetman = obj.AddComponent<AssetManager>();
                        }

                        //assetman.selfVariant = colorvariant.color_varyant[v];
                        assetman.selfVariant = new ColorVaryant{
                            varyant_dress_id=colorvariant.color_varyant[v].varyant_dress_id,
                            varyant_dress_name = colorvariant.color_varyant[v].varyant_dress_name,
                            varyant_gender_id = colorvariant.color_varyant[v].varyant_gender_id,
                            varyant_gender_name = colorvariant.color_varyant[v].varyant_gender_name,
                            varyant_id = colorvariant.color_varyant[v].varyant_id,
                            varyant_manken_model = colorvariant.color_varyant[v].varyant_manken_model,
                            varyant_name = colorvariant.color_varyant[v].varyant_name,
                            varyant_model = colorvariant.color_varyant[v].varyant_model,
                            varyant_subtype_id = colorvariant.color_varyant[v].varyant_subtype_id,
                            varyant_subtype_name = colorvariant.color_varyant[v].varyant_subtype_name,
                            varyant_type_id = colorvariant.color_varyant[v].varyant_type_id,
                            varyant_type_name = colorvariant.color_varyant[v].varyant_type_name,
                            varyant_version = colorvariant.color_varyant[v].varyant_version,
                            
                        };
                        assetman.hangerLocation = respHangermanager.tree_location;
                        assetman.hangerPos = j;
                        assetman.prepareModel();
                    }

                }
            }
        }
    }

    void checkAllAsset()
    {
        if (downloadedAssetCount == totalAssetCount)
        {
            //finish
            foreach(var hangerManager in hangerManagers)
            {
                foreach(var hanger in hangerManager.HangerList)
                {
                    hanger.fillVariant();
                }
            }
        }
    }

    public void assetDownloaded()
    {
        downloadedAssetCount++;
        checkAllAsset();
    }

    void getScene()
    {
        int sceneID = PlayerPrefs.GetInt("GameID");
        Debug.Log(sceneID + " loading");
        StartCoroutine(_getscene(sceneID.ToString()));
    }

    IEnumerator _getscene(string id)
    {
        PostCtrl post = new PostCtrl();
        //error.text = "Giriş Yapılıyor";

        yield return StartCoroutine(post.gettData(EndPoint.getscene, id));

        if (post.resultObj.responseCode != 200)  //on server fail
        {
            //error.text = post.resultObj.error;

        }
        else //on server success
        {
            string resultStr = post.resultObj.downloadHandler.text;

           var res= JsonConvert.DeserializeObject<List<Response>>(resultStr);
            if(res!=null && res.Count>0)
            {
            resp = res[0].scenario_trees;
            if (resp != null)
            {
                fillHangers();
            }
            }
        }


    }
}

/* OLD
 
 void fillHangers()
    {
        foreach(var hanger in hangerManagers)
        {
           foreach(var hangerObj in hanger.HangerList)
            {
                hangerObj.ListModel[0].variantList = new List<HangerVariant>();
               
                foreach(var testobj in testObjs)
                {
                    var variant = new HangerVariant();
                    variant.productCode = testobj.productCode;
                    variant.productName = testobj.productName;
                    hangerObj.ListModel[0].variantList.Add(variant);

                    var obj = GameObject.Find(testobj.productCode);
                    if (obj == null)
                    {
                        obj = Instantiate(testobj.productModel);
                        obj.transform.position = new Vector3(999, 999, 999);
                        obj.name = testobj.productCode;
                        Debug.Log(testobj.productCode);
                    }
                }
                hangerObj.fillVariant();
            }
        }
    }     
     
*/
