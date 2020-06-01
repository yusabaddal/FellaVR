using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class LauncherManager : MonoBehaviour {
    public List<GameObject> pages;
    public InputField mailInput, pwInput;
    public Dropdown drop;
    public Image loadingImage;
    public bool loadingGame;
    float current;
    public float loadingSpeed;
    public Text loadingtext;

    AsyncOperation async;

    void Start () {
        Screen.SetResolution(1280, 720, false);
	}
	
	// Update is called once per frame
	void Update () {

        if (loadingGame)
        {
            current += loadingSpeed;
            loadingImage.fillAmount = current;
            loadingtext.text = "%" + (int)(current * 100);
            if (current > 1)
            {
                loadingGame = false;
                OpenPage(5);

            }
            //loadingImage.fillAmount = async.progress;
            //loadingtext.text = "%" +(int)async.progress*100;
            //if (async.isDone)
            //{
            //    loadingGame = false;
            //    OpenPage(5);
            //}

        }

	}

    public void FullScreen()
    {
        Screen.SetResolution(1920, 1080, true);
    }


    public void startDownload()
    {
        loadingGame = true;
        //StartCoroutine(load(1));
    }

    IEnumerator load(int levelName)
    {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
           "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        async = Application.LoadLevelAsync(levelName);
        async.allowSceneActivation = false;
        yield return async;
        loadingGame = false;
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
        //async.allowSceneActivation = true;

    }



    public void Login()
    {
        if (mailInput.text != "" && pwInput.text != "")
        {
            PostLogin login = new PostLogin();

            login.email = mailInput.text;
            login.password = pwInput.text;
            login.mac = "1234";

            StartCoroutine(post(login));
        }
    }


    public IEnumerator post(PostLogin login)
    {

        Debug.Log(JsonUtility.ToJson(login));

        PostCtrl post = new PostCtrl();
        //error.text = "Giriş Yapılıyor";
        //error.text = "Giriş Yapılıyor";

        yield return StartCoroutine(post.postData(EndPoint.login, JsonConvert.SerializeObject(login)));

        if (post.resultObj.responseCode != 200)  //on server fail
        {
            //error.text = post.resultObj.error;
            //error.text = "Hata";
            Debug.Log(post.resultObj.downloadHandler.text);
        }
        else //on server success
        {
            string resultStr = post.resultObj.downloadHandler.text;
            if (resultStr != "0")
            {
                OpenPage(1);
            }
            else
            {
                //error.text = "Hata";
            }
        }

      
}


    //string GetMacAddress()
    //{
    //    var macAdress = "";
    //    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
    //    var i = 0;
    //    foreach (var adapter  in nics)
    //    {
    //        PhysicalAddress address = adapter.GetPhysicalAddress();
    //        if (address.ToString() != "")
    //        {
    //            macAdress = address.ToString();
    //            return macAdress;
    //        }
    //    }
    //    return "";
    //}
}
