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
    public GameObject UICanvas;
    

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
        ListModel[currentColor].variantList[selected].productModel.SetActive(true);
    }

    void closeAllObjects()
    {
        foreach (var obj in ListModel[currentColor].variantList)
        {
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
        if (UICanvas != null)
            UICanvas.SetActive(state);
    }

    public void fillVariant()
    {
        for(int i=0;i<this.ListModel.Count;i++)
        {
           
            for( int j=0;j < this.ListModel[i].variantList.Count;j++)
            {
                var modelVariant = this.ListModel[i].variantList[j];
                //modelVariant.productModel = Instantiate(modelVariant.productModel, this.transform);
                //Debug.Log(transform.parent.gameObject.name);
                var findObj = GameObject.Find(modelVariant.productCode);
                if (findObj != null)
                {
                    var obj = Instantiate(findObj, this.transform);
                    obj.transform.localPosition = new Vector3(0, 0, 0);
                    //obj.name = modelVariant.productCode;
                    modelVariant.productModel = obj;
                    Debug.Log(modelVariant.productCode);
                }
            }
        }
        showSelectedModel(0);
    }

}
