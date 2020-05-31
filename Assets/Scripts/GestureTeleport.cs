using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GestureTeleport : MonoBehaviour
{
    bool pointed;
    bool teleporting;
    public Transform finger;
    public LineRenderer laserLine;
    public GameObject player;
    Transform teleportingTo;
    public float range;

    public Image loadingImg;
    public float waitTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
    void LateUpdate()
    {
        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (pointed)
        {

            laserLine.gameObject.SetActive(true);
            // Create a vector at the center of our camera's viewport
            //Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            // Declare a raycast hit to store information about what our raycast has hit
            //RaycastHit hit;
            RaycastHit[] hits;
            hits = Physics.RaycastAll(finger.transform.position, finger.transform.right, range);

          

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if (hit.collider.transform.tag == "Teleport")
                {
                    //player.transform.position = hit.collider.transform.position;
                    teleportingTo = hit.collider.transform;
                    teleporting = true;
                }
                //Debug.Log(hit.collider.gameObject.name);
            }
            if (hits.Length == 0)
                teleporting = false;

            // Check if our raycast has hit anything
            //if (Physics.Raycast(finger.transform.position, finger.transform.forward, out hit, range))
            //{
            //    //Vector3 forward = finger.transform.TransformDirection(Vector3.forward) * range;
            //    //Debug.DrawRay(finger.transform.position, forward, Color.green);

            //    //laserLine.SetPosition(1, hit.point);

            //    if (hit.collider.transform.tag == "Teleport")
            //    {
            //        player.transform.position = hit.collider.transform.position;
            //        Debug.Log(hit.collider.gameObject.name);
            //    }

            //    //laserLine.SetPosition(1, rayOrigin + (Camera.main.transform.forward * 100));

            //}
            //else
            //{
            //    Debug.Log("nothing");
            //}

        }
        else
        {
            laserLine.gameObject.SetActive(false);
            teleporting = false;
        }

        if (teleporting)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                teleport();
                return;
            }

            if(timer!=0)
            loadingImg.fillAmount = timer/waitTime;

        }
        else
        {
            resetTeleporting();
        }
    }

    public void pointerGesture(bool state)
    {
        Debug.Log(state);
        pointed = state;
    }

    void teleport()
    {
        //var child = teleportingTo.GetChild(0);

        //player.transform.position = new Vector3(teleportingTo.position.x, player.transform.position.x, teleportingTo.position.z);
        player.transform.SetParent(teleportingTo);
        player.transform.localPosition = Vector3.zero;
        teleportingTo.DetachChildren();
        resetTeleporting();
    }
    void resetTeleporting()
    {
        teleporting = false;
        loadingImg.fillAmount = 0;
        timer = 0;
    }
}
