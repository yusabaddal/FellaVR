using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class HangerVariant
{
    public string productCode;
    public string productName;
    public Sprite productImage;
    public GameObject productModel;
    public GameObject avatarModel;

}
[System.Serializable]
public class VariantColor
{
    public string ColorName;
    public List<HangerVariant> variantList;
}

public class HangerVariantManager : MonoBehaviour {
    
    int currentSelected;
    int currentColor;
    public HangerUIManager uımanager;

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
    }

    void closeAllObjects()
    {
        foreach (var obj in ListModel[currentColor].variantList)
        {
            if(obj.productModel!=null)
            obj.productModel.SetActive(false);
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
        for(int i=0;i<this.ListModel.Count;i++)
        {
            if (this.ListModel[i].variantList.Count == 0)
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }
            for( int j=0;j < this.ListModel[i].variantList.Count;j++)
            {
                var modelVariant = this.ListModel[i].variantList[j];
                //modelVariant.productModel = Instantiate(modelVariant.productModel, this.transform);
                //Debug.Log(transform.parent.gameObject.name);
                //var findObj = GameObject.Find(modelVariant.productCode+"_Aski");
                for(int a = 0; a < GameManager.instance.assetsContent.childCount; a++)
                {
                    if (GameManager.instance.assetsContent.GetChild(a).gameObject.name.ToString() == modelVariant.productCode + "_Aski")
                    {
                        uımanager.buttonNames.Add(new buttonModel { buttonName= modelVariant.productCode } );
                        GameObject obj = Instantiate(GameManager.instance.assetsContent.GetChild(a).gameObject, this.transform);
                        obj.transform.localPosition = new Vector3(0, 0, 0);
                        //obj.name = modelVariant.productCode;
                        var selfVariant = obj.transform.GetComponent<VariantManager>();
                        if (selfVariant == null)
                        {
                            selfVariant = obj.AddComponent<VariantManager>();
                        }
                        selfVariant.selfVariant = new HangerVariant {
                            productCode=modelVariant.productCode,
                            productName=modelVariant.productName,
                            productModel=obj                            
                        };
                        var col =obj.AddComponent<BoxCollider>();
                        col.isTrigger = true;
                        col.center = new Vector3(0, 5, 0);
                       //modelVariant.productModel = obj;
                        Debug.Log(modelVariant.productCode);
                        modelVariant.productModel = obj;
                        if (a > 0)
                        {
                            obj.SetActive(false);
                        }
                    }
                }
                //if (findObj != null)
                //{
                    
                //}
            }
        }
        showSelectedModel(0);
        uımanager.setUI();
    }

}
