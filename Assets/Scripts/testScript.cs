using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class testScript : MonoBehaviour {

    public string myClothID;
    public GameObject objUI;

    public List<GameObject> ListObject,ListModel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void showSelected(int selected)
    {
        closeAllObjects(ListObject);
        ListObject[selected].SetActive(true);
    }
    public void showSelectedModel(int selected)
    {
        closeAllObjects(ListModel);
        ListModel[selected].SetActive(true);
    }

    void closeAllObjects(List<GameObject>list)
    {
        foreach(var obj in list)
        {
            obj.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("girdi");
        var otherCloth = other.transform.GetComponent<ClothesHolder>();
        if (otherCloth != null)
        {
            otherCloth.findClotes(myClothID);
        }
    }
   public void setUIActive(bool state)
    {
        if (objUI != null)
            objUI.SetActive(state);
    }


    public void gestureTest(bool state)
    {
        Debug.Log(state);
    }
}
