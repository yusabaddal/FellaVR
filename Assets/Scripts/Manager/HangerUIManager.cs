using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class buttonModel
{
    public string buttonName;
    public string buttonImagePath;
}
public class HangerUIManager : MonoBehaviour
{
    public List<Transform> buttonContents;
    public List<buttonModel> buttonNames;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Awake()
    {
        if(buttonNames==null)
        buttonNames = new List<buttonModel>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUI()
    {
        //if (buttonNames.Count < 2)
        //{
        //    return;
        //}
        closeAll();
        for(int i = 0; i < buttonNames.Count; i++)
        {
            if (i >= buttonContents.Count)
            {
                Debug.Log("Buton yetmedi");
                return;
            }
            buttonContents[i].gameObject.SetActive(true);
            if (buttonContents[i].GetComponent<CubeButton>() == null) {
                buttonContents[i].GetComponentInChildren<Text>().text = buttonNames[i].buttonName;
            }
            else
            {
                //buttonContents[i].GetComponent<CubeButton>().buttonText.text = buttonNames[i];
                buttonContents[i].GetComponent<CubeButton>().buttonString = buttonNames[i].buttonName;
                buttonContents[i].GetComponent<CubeButton>().buttonImgPath = buttonNames[i].buttonImagePath;
                buttonContents[i].GetComponent<CubeButton>().setbutton();
            }
         

        }
    }

    void closeAll()
    {
        foreach(var cont in buttonContents)
        {
            cont.gameObject.SetActive(false);
        }
    }

    
}
