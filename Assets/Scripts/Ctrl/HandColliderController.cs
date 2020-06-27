using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColliderController : MonoBehaviour
{
    public bool isLeft;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance != null)
            GameManager.instance.setHandCollider(transform,isLeft);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
