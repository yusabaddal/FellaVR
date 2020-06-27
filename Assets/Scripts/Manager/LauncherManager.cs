using FellaVR;
using Newtonsoft.Json;
using Oculus.Platform.Models;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Oculus.Platform;

public class LauncherManager : MonoBehaviour {
    public List<GameObject> pages;
    public InputField mailInput, pwInput;
    //public Dropdown drop;
    public Image loadingImage;
    public bool loadingGame;
    float current;
    public float loadingSpeed;
    public Text loadingtext, error;

    public Text userNameTextfield;
    public Dropdown userScenesDrop;
    AsyncOperation async;

    public GameObject openButton;
    ResponseLogin user;
    List<ResponseUserScenes> userScenes;
    EventSystem system;
    void Start () {
        Screen.SetResolution(1280, 720, false);

        system = EventSystem.current;// EventSystemManager.currentSystem;
    }

    // Update is called once per frame
    void Update () {


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null)
            {

                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null)
                    inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
            //else Debug.Log("next nagivation element not found");

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Login();
        }

        //if (loadingGame)
        //{
        //    current += loadingSpeed;
        //    loadingImage.fillAmount = current;
        //    loadingtext.text = "%" + (int)(current * 100);
        //    if (current > 1)
        //    {
        //        loadingGame = false;
        //        OpenPage(5);

        //    }
        //    //loadingImage.fillAmount = async.progress;
        //    //loadingtext.text = "%" +(int)async.progress*100;
        //    //if (async.isDone)
        //    //{
        //    //    loadingGame = false;
        //    //    OpenPage(5);
        //    //}

        //}

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
        async = UnityEngine.Application.LoadLevelAsync(levelName);
        async.allowSceneActivation = false;
        yield return async;
        loadingGame = false;
    }

    public void dropdownChanged()
    {
        if (userScenesDrop.value != 0)
        {
            PlayerPrefs.SetInt("GameID",userScenes[userScenesDrop.value-1].scenario_id);
            Debug.Log(userScenes[userScenesDrop.value - 1].scenario_id);
            openButton.SetActive(true);
        }
        else
        {
            openButton.SetActive(false);

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
        UnityEngine.Application.LoadLevel(scene);
        //async.allowSceneActivation = true;

    }



    public void Login()
    {
        if (mailInput.text != "" && pwInput.text != "")
        {
            PostLogin login = new PostLogin();

            login.email = mailInput.text;
            login.password = pwInput.text;
            //login.mac = "12345678";
            login.mac = GetMacAddress();
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
            error.text = post.resultObj.downloadHandler.text;
            //error.text = "Hata";
            Debug.Log(post.resultObj.downloadHandler.text);
        }
        else //on server success
        {
            string resultStr = post.resultObj.downloadHandler.text;
            var resp = JsonConvert.DeserializeObject<List<ResponseLogin>>(resultStr);
            if (resp.Count > 0)
            {
                user = resp[0];
                PlayerPrefs.SetInt("UserID", user.user_id);
                setUserName();
                StartCoroutine(getUserScenes(user.user_id.ToString()));
                OpenPage(1);
            }
        }

    
    }


    void setUserName()
    {
        userNameTextfield.text = "Hoşgeldiniz, " +user.user_firstname + " " + user.user_lastname;
    }
    void setUserScenes()
    {
        List<string> scenelist = new List<string>();
        scenelist.Add("Sahne Seçiniz.");
        foreach (var s in userScenes)
        {
            scenelist.Add(s.scenario_name);
        }

        userScenesDrop.AddOptions(scenelist);
        
    }
    IEnumerator getUserScenes(string id)
    {
        PostCtrl post = new PostCtrl();
        //error.text = "Giriş Yapılıyor";

        yield return StartCoroutine(post.gettData(EndPoint.getUserScenes, id));

        if (post.resultObj.responseCode != 200)  //on server fail
        {
            //error.text = post.resultObj.error;

        }
        else //on server success
        {
            string resultStr = post.resultObj.downloadHandler.text;

            if (resultStr != "")
            {
                userScenes = JsonConvert.DeserializeObject<List<ResponseUserScenes>>(resultStr);
                setUserScenes();
            }
            else
            {

            }
        }


    }
    string GetMacAddress()
    {
        var macAdress = "";
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        var i = 0;
        foreach (var adapter in nics)
        {
            PhysicalAddress address = adapter.GetPhysicalAddress();
            if (address.ToString() != "")
            {
                macAdress = address.ToString();
                return macAdress;
            }
        }
        return "";
    }
}
