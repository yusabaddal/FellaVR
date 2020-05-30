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
        for(int i=0;i<ModelClothesList.Count;i++)
        {
            var item = ModelClothesList[i];
            if (clothName == item.productCode)
            {
                showEquipedClothes(i);
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
