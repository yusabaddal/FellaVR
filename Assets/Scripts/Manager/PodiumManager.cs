using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PodiumManager : MonoBehaviour
{
    public PodiumModel pModel;
    public HangerUIManager genderUI, dressUI,typeUI,subtypeUI;
    public List<GameObject> canvasList;
    int currentgender, currenttype, currentdress;
    // Start is called before the first frame update
    void Start()
    {
        prepareGenderList();
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

    }

    public void prepareGenderList()
    {
        for (int i = 0; i < pModel.genderList.Count; i++)
        {
            genderUI.buttonNames.Add(pModel.genderList[i].name);
        }
        genderUI.setUI();
    }
    public void prepareDressList(int gender)
    {
        currentgender = gender;
        openPage(1);
        for (int i = 0; i < pModel.genderList[gender].dressList.Count; i++)
        {
            dressUI.buttonNames.Add(pModel.genderList[gender].dressList[i].name);
        }
        dressUI.setUI();

    }
    public void prepareTypeList(int dress)
    {
        currentdress = dress;
        openPage(2);
        for (int i = 0; i < pModel.genderList[currentgender].dressList[currentdress].typeList.Count; i++)
        {
            typeUI.buttonNames.Add(pModel.genderList[currentgender].dressList[currentdress].typeList[i].name);
        }
        typeUI.setUI();

    }
    public void prepareSubtypeList(int type)
    {
        currenttype = type;
        openPage(3);
        for (int i = 0; i < pModel.genderList[currentgender].dressList[currentdress].typeList[currenttype].subtypeList.Count; i++)
        {
            subtypeUI.buttonNames.Add(pModel.genderList[currentgender].dressList[currentdress].typeList[currenttype].subtypeList[i].name);
        }
        subtypeUI.setUI();

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
        canvasList[page].SetActive(true);
    }
}
