using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariantManager : MonoBehaviour
{
    public HangerVariant selfVariant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherCloth = other.transform.GetComponent<ClothesHolder>();
        if (otherCloth != null)
        {
            otherCloth.findClotes(selfVariant.productCode);
            Debug.Log("girdi");

        }
    }
}
