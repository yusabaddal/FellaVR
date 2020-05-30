using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<HangerManager> hangerManagers;
    public List<HangerVariant> testObjs;
    public Response resp;
    // Start is called before the first frame update
    void Start()
    {
        resp = new Response();

        

        fillHangers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void fillHangers()
    {
        foreach(var hanger in hangerManagers)
        {
           foreach(var hangerObj in hanger.HangerList)
            {
                hangerObj.ListModel[0].variantList = new List<HangerVariant>();
               
                foreach(var testobj in testObjs)
                {
                    var variant = new HangerVariant();
                    variant.productCode = testobj.productCode;
                    variant.productName = testobj.productName;
                    hangerObj.ListModel[0].variantList.Add(variant);

                    var obj = GameObject.Find(testobj.productCode);
                    if (obj == null)
                    {
                        obj = Instantiate(testobj.productModel);
                        obj.transform.position = new Vector3(999, 999, 999);
                        obj.name = testobj.productCode;
                        Debug.Log(testobj.productCode);
                    }
                }
                hangerObj.fillVariant();
            }
        }
    }
}
