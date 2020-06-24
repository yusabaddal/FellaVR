using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerCursorController : MonoBehaviour
{
    public GameObject cursor;
    // Start is called before the first frame update
    void Start()
    {
        if (cursor == null)
        {
            if (transform.childCount > 0)
            {
                cursor = transform.GetChild(0).gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void cursorState(bool state)
    {
        cursor.SetActive(state);
    }
}
