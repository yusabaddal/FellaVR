using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FellaVR
{
    [System.Serializable]
    public class ResultLogin : Result
    {
        public User User;
    }

    [System.Serializable]
    public class User
    {
        public string Name;
        public string Surname;
        public string Email;
        public string Token;
        public string Permissions;
    }
}
