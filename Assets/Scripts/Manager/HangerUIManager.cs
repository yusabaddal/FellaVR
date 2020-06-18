using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangerUIManager : MonoBehaviour
{
    public List<Transform> buttonContents;
    public List<string> buttonNames;
    // Start is called before the first frame update
    void Start()
    {
        buttonNames = new List<string>();
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
        for(int i = 0; i < buttonNames.Count; i++)
        {
            buttonContents[i].gameObject.SetActive(true);
            if (buttonContents[i].GetComponent<CubeButton>() == null) {
                buttonContents[i].GetComponentInChildren<Text>().text = buttonNames[i];
            }
            else
            {
                //buttonContents[i].GetComponent<CubeButton>().buttonText.text = buttonNames[i];
                buttonContents[i].GetComponent<CubeButton>().buttonString = buttonNames[i];
                buttonContents[i].GetComponent<CubeButton>().setbutton();
            }
         

        }
    }
}
