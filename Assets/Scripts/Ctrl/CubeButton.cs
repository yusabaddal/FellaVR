using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CubeButton : MonoBehaviour
{
    public Text buttonText;
    public Image buttonImg;
    public Sprite buttonSprite;
    public string buttonImgPath;
    public string buttonString;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setbutton()
    {
        //if (buttonSprite != null)
        //{
        //    buttonImg.sprite = buttonSprite;
        //    buttonImg.gameObject.SetActive(true);
        //    return;
        //}
        //else
        //{
        //    if (buttonImgPath != "")
        //    {
        //        Davinci.get().load(buttonImgPath).into(buttonImg).start();
        //        buttonImg.gameObject.SetActive(true);
        //        return;
        //    }
          
        //}
        if (buttonString != null)
        {
            buttonText.text = buttonString;
        }
        
    }

    public void callProduct()
    {
        for (int a = 0; a < GameManager.instance.assetsContent.childCount; a++)
        {
            if (GameManager.instance.assetsContent.GetChild(a).gameObject.name.ToString() == buttonString + "_Avatar")
            {
                var avatarObj = Instantiate(GameManager.instance.assetsContent.GetChild(a).gameObject, transform);
                avatarObj.transform.SetParent(GameManager.instance.pManken.transform.parent);
                avatarObj.transform.localPosition = Vector3.zero;
                var change= avatarObj.transform.GetComponentInChildren<ChangeMesh>();
                if(change!=null)
                change.AssignToActor();

            }

        }

    }


}
