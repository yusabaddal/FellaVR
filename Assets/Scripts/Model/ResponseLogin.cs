
using UnityEngine;

[System.Serializable]
public class ResponseLogin 
{
    public int user_id { get; set; }
    public string user_firstname { get; set; }
    public string user_lastname { get; set; }
    public string user_email { get; set; }
    public string user_password { get; set; }
}
