using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClothesHolder : MonoBehaviour {
    public List<AvatarModel> ModelClothesList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void findClotes(string clothName)
    {
        closeOthers();
        for (int i=0;i<ModelClothesList.Count;i++)
        {
            var item = ModelClothesList[i];
            if (clothName == item.productCode)
            {
                showEquipedClothes(i);
                return;
            }
        }

        for (int a = 0; a < GameManager.instance.assetsContent.childCount; a++)
        {
            if (GameManager.instance.assetsContent.GetChild(a).gameObject.name.ToString() == clothName + "_Avatar")
            {
                var avatarObj = Instantiate(GameManager.instance.assetsContent.GetChild(a).gameObject,transform);
                avatarObj.transform.localPosition = new Vector3(0, 0, 0);
                ModelClothesList.Add(new AvatarModel {
                    productCode=clothName,
                    productModel= avatarObj
                });
            }

        }

    }
    public void showEquipedClothes(int selected)
    {
        closeOthers();
        ModelClothesList[selected].productModel.SetActive(true);
    }
    void closeOthers()
    {
        foreach(var item in ModelClothesList)
        {
            item.productModel.SetActive(false);
        }
    }

   
}
