using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauncherManager : MonoBehaviour {
    public List<GameObject> pages;
    public InputField mailInput, pwInput;
    public string mail, pw;
    public Dropdown drop;
    public Image loadingImage;
    public bool loadingGame;
    float current;
    public float loadingSpeed;
    public Text loadingtext;
	// Use this for initialization
	void Start () {
        Screen.SetResolution(1280, 720, false);
	}
	
	// Update is called once per frame
	void Update () {

        if (loadingGame)
        {
            current += loadingSpeed;
            loadingImage.fillAmount = current;
            loadingtext.text = "%" +(int)(current*100);
            if (current > 1)
            {
                loadingGame = false;
                OpenPage(5);
                
            }
        }

	}

    public void FullScreen()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    public void Login()
    {
        if(mailInput.text==mail && pwInput.text == pw)
        {
            OpenPage(1);
        }
    }

    public void startDownload()
    {
        loadingGame = true;

    }

    public void dropdownChanged()
    {
        if (drop.value != 0)
        {
            OpenPage(3);
        }
    }

    public void OpenPage(int page)
    {
        closeAllpage();
        pages[page].SetActive(true);
    }
    void closeAllpage()
    {
        foreach(var p in pages)
        {
            p.SetActive(false);
        }
    }

    public void OpenScene(int scene)
    {
        Application.LoadLevel(scene);
    }
}
