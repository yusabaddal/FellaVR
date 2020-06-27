using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FavoritePostModel 
{
    public int id { get; set; }
    public int favorite_user { get; set; }
    public int scenario_id { get; set; }
}
