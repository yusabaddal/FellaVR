using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapHandleController : HangerOrganizer {
    protected InteractionBehaviour _intObj;

    // Use this for initialization
    void Start () {
        //_intObj = GetComponent<InteractionBehaviour>();
        //_intObj.OnGraspBegin += onGraspBegin;
        //_intObj.OnGraspEnd += onGraspEnd;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void onGraspBegin()
    {
        isGrabbed(true);
    }

    private void onGraspEnd()
    {
        isGrabbed(false);
    }
}
