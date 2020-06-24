using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class ChangeMesh : MonoBehaviour
{

    public GameObject target;
    public Transform rootBone;

    //bones List gets all bones from the SkinnedMeshRenderer at Start()
    public List<Transform> bones = new List<Transform>();
    //boneArray Transform gets filled in the assign function. It has to have the same bones in it as the bones List, or there will be an error.
    //If the main body mesh is missing any bones, try giving them weights or keyframes and export again.
    public Transform[] boneArray;

    public bool isUpper;

    private void Awake()
    {
        if (rootBone == null)
        {
            var selfMesh = transform.GetComponent<SkinnedMeshRenderer>();
            if (selfMesh != null)
            {
                rootBone = selfMesh.rootBone;
            }
        }
    }
   
    //void Update()
    //{


    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        if (target != null)
    //        {
    //            AssignToActor();
    //        }
    //        else
    //        {
    //            findTarget(isUpper);
    //        }
    //    }
    //}


    public void AssignToActor()
    {
        if (target == null)
        {
            findTarget(isUpper);
        }
        
        transform.parent = target.transform;
        transform.localPosition = Vector3.zero;

        SkinnedMeshRenderer targetRenderer = target.GetComponent<SkinnedMeshRenderer>();
        Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
        foreach (Transform bone in targetRenderer.bones)
        {
            boneMap[bone.name] = bone;
        }

        SkinnedMeshRenderer thisRenderer = GetComponent<SkinnedMeshRenderer>();

        boneArray = thisRenderer.bones;

        for (int i = 0; i < boneArray.Length; i++)
        {

            string boneName = boneArray[i].name;

            if (boneMap.TryGetValue(boneName, out boneArray[i]) == false)
            {
                Debug.LogError("failed to get bone: " + boneName);
                Debug.LogError(i);

                //Debug.Break();
            }
        }
        thisRenderer.bones = boneArray; //take effect
        targetRenderer.enabled = false;
        transform.GetComponent<SkinnedMeshRenderer>().enabled = true;
    }

    void findTarget(bool isUpper)
    {
        if (isUpper)
        {
            target = GameManager.instance.pManken.upperTarget;
        }
        else
        {
            target = GameManager.instance.pManken.bottomTarget;
        }
    }

}
