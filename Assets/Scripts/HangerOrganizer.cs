using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerOrganizer : MonoBehaviour {
    bool isOut=false,sabitle;
    float waitTime=1;
    private float stayCount = 0.0f;
    Vector3 startPos;
    Quaternion startRot;
    public GameObject SelfObject;
    GameObject secObject;
   [HideInInspector] public bool Grabbed;
    public GameObject objUI;
    // Use this for initialization
    void Start () {
        startPos = transform.localPosition;
        startRot = transform.localRotation;  
    }
	
	// Update is called once per frame
	void Update () {
        //if (sabitle)
        //{
        //    transform.localPosition = startPos;
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isOut && other.transform.tag == "Dress")
        {
            Debug.Log("İtem girdi");
            

        }
        if (other.transform.tag == "Hand")
        {
            var obj = Instantiate(transform.gameObject, other.transform);
        }

    }

    private void OnDestroy()
    {
        setToHanger();
    }
    public void setToHanger()
    {
        //if (SelfObject != null)
        //{
        //    secObject = Instantiate(SelfObject, transform.parent);
        //    secObject.transform.localPosition = startPos;
        //    secObject.SetActive(true);
        //}
        //transform.GetComponent<Rigidbody>().isKinematic = false;
        //Destroy(transform.GetComponent<Rigidbody>());
        transform.localPosition = startPos;
        transform.localRotation = startRot;
        setUIActive(false);

       
    }

    private void OnTriggerStay(Collider other)
    {
        if (isOut && other.transform.tag == "Dress")
        {
            
            if (!Grabbed)
            {
                isOut = false;
                setToHanger();
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (!isOut)
        {
            Debug.Log("İtem çıktı");
            isOut = true;
            setUIActive(true);
            //sabitle = false;
        }
    }

    public void isGrabbed(bool v)
    {
        Grabbed = v;
        Debug.Log(v);

        setUIActive(v);
    }
    void setUIActive(bool state)
    {
        if (objUI != null)
            objUI.SetActive(state);
    }
}
