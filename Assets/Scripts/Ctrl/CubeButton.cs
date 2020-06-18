using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CubeButton : MonoBehaviour
{
    public Text buttonText;
    public Image buttonImg;
    public Sprite buttonSprite;
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
        if (buttonSprite != null)
        {
            buttonImg.sprite = buttonSprite;
            buttonImg.gameObject.SetActive(true);
            return;
        }
        else
        {
            buttonImg.gameObject.SetActive(false);
        }
        if (buttonString != null)
        {
            buttonText.text = buttonString;
        }
    }
}
