using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneModel
{
    public List<Hangers> hangers;
}
[System.Serializable]
public class Hangers
{
    public int orderID;
    public GameObject hanger;
    public List<Transform> hangerContent;
    public Transform modelContent;
}
