using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PodiumManager : MonoBehaviour
{
    public PodiumModel pModel;
    public HangerUIManager genderUI, dressUI,typeUI,subtypeUI,productsUI;
    public List<GameObject> canvasList;
    int currentgender, currenttype, currentdress,currentPage;
    public List<ColorVaryant> productList;
    // Start is called before the first frame update
    void Start()
    {
        pModel = new PodiumModel();
        getPodium();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public void setKiosk()
    //{
    //    for(int i=0;i<pModel.genderList.Count;i++)
    //    {
    //        var gender = pModel.genderList[i];

    //        for(int j = 0; j < gender.dressList.Count; j++)
    //        {
    //            var dress = gender.dressList[j];

    //            for(int z = 0; z < dress.typeList.Count;z++)
    //            {
    //                var type = dress.typeList[z];

    //                for(int c = 0; c < type.subtypeList.Count; c++)
    //                {
    //                    var subtype = type.subtypeList[c];


    //                }
    //            }
    //        }
    //    }
    //}3


    void getPodium()
    {
        StartCoroutine(_getPodium());
    }

    public void prepareGenderList()
    {
        for (int i = 0; i < pModel.genderList.Count; i++)
        {
            string buttonname = pModel.genderList[i].gender_name;
            string imgpath= pModel.genderList[i].gender_icon;
            genderUI.buttonNames.Add(new buttonModel {buttonName=buttonname,buttonImagePath=imgpath });
        }
        genderUI.setUI();
    }
    public void prepareDressList(int gender)
    {
        currentgender = gender;
        openPage(1);
        for (int i = 0; i < pModel.genderList[gender].gender_dress.Count; i++)
        {
            string buttonname = pModel.genderList[gender].gender_dress[i].dress_name;
            string imgpath = pModel.genderList[gender].gender_dress[i].dress_icon;

            dressUI.buttonNames.Add(new buttonModel { buttonName = buttonname, buttonImagePath = imgpath });
        }
        dressUI.setUI();

    }
    public void prepareTypeList(int dress)
    {
        currentdress = dress;
        openPage(2);
        for (int i = 0; i < pModel.genderList[currentgender].gender_dress[currentdress].dress_types.Count; i++)
        {
            string buttonname = pModel.genderList[currentgender].gender_dress[currentdress].dress_types[i].type_name;
            string imgpath = pModel.genderList[currentgender].gender_dress[currentdress].dress_types[i].type_icon;

            typeUI.buttonNames.Add(new buttonModel { buttonName = buttonname, buttonImagePath = imgpath });
        }
        typeUI.setUI();

    }
    public void prepareSubtypeList(int type)
    {
        currenttype = type;
        openPage(3);
        for (int i = 0; i < pModel.genderList[currentgender].gender_dress[currentdress].dress_types[currenttype].type_subtypes.Count; i++)
        {
            string buttonname = pModel.genderList[currentgender].gender_dress[currentdress].dress_types[currenttype].type_subtypes[i].subtype_name;
            string imgpath = pModel.genderList[currentgender].gender_dress[currentdress].dress_types[currenttype].type_subtypes[i].subtype_icon;

            subtypeUI.buttonNames.Add(new buttonModel { buttonName = buttonname, buttonImagePath = imgpath });
        }
        subtypeUI.setUI();

    }

    public void prepareProducts(int subtype)
    {
        int selectedSubtypeID = pModel.genderList[currentgender].gender_dress[currentdress].dress_types[currenttype].type_subtypes[subtype].subtype_id;
        //int selectedSubtypeID = 15;
        productList = new List<ColorVaryant>();
        openPage(4);

        foreach(var prod in GameManager.instance.productList)
        {
            if (prod.varyant_subtype_id == selectedSubtypeID)
            {
                productList.Add(prod);
            }
        }

        for (int i = 0; i < productList.Count; i++)
        {
            string buttonname = productList[i].varyant_stock_code;
            string imgpath = "";

            productsUI.buttonNames.Add(new buttonModel { buttonName = buttonname, buttonImagePath = imgpath });
        }
        productsUI.setUI();
    }


    void closeAllPages()
    {
        foreach(var p in canvasList)
        {
            p.SetActive(false);
        }
    }
    public void openPage(int page)
    {
        closeAllPages();
        currentPage = page;
        canvasList[page].SetActive(true);
    }
    public void backPage()
    {
        if(currentPage>0)
        openPage(currentPage - 1);
    }

    IEnumerator _getPodium()
    {
        PostCtrl post = new PostCtrl();
        //error.text = "Giriş Yapılıyor";

        yield return StartCoroutine(post.gettData(EndPoint.getpodium,""));

        if (post.resultObj.responseCode != 200)  //on server fail
        {
            //error.text = post.resultObj.error;

        }
        else //on server success
        {
            string resultStr = post.resultObj.downloadHandler.text;

            var res = JsonConvert.DeserializeObject<List<podiumGender>>(resultStr);          
            if (res != null)
            {
                pModel.genderList = res;
                prepareGenderList();
            }
        }
    }
}
