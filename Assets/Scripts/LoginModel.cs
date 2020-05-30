using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginModel : MonoBehaviour
{
    public InputField id, sifre;
    public Text error;

    public void Login()
    {
        PostLogin login = new PostLogin();

        login.Email = id.text;
        login.Password = sifre.text;


        StartCoroutine(post(login));

    }

    public void gonext()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    public IEnumerator post(PostLogin login)
    {

        Debug.Log(JsonUtility.ToJson(login));

        PostCtrl post = new PostCtrl();
        //error.text = "Giriş Yapılıyor";
        error.text = "Giriş Yapılıyor";

        yield return StartCoroutine(post.gettData(EndPoint.login, ("id=" + login.Email + "&ps=" + login.Password)));

        if (post.resultObj.responseCode != 200)  //on server fail
        {
            //error.text = post.resultObj.error;
            error.text = "Hata";

        }
        else //on server success
        {
            string resultStr = post.resultObj.downloadHandler.text;
            if (resultStr != "0")
            {
                gonext();

            }
            else
            {
                error.text = "Hata";
            }
        }


    }
}
