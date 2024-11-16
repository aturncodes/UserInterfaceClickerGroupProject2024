using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UserInfo", menuName = "Scriptable Objects/UserInfo")]
public class UserInfo : ScriptableObject
{

    [SerializeField] private Dictionary<string, string> userInfo;

    void OnEnable()
    {
        userInfo = new Dictionary<string, string>();
    }

    public bool AddUser(string username, string password)
    {

        if (userInfo.ContainsKey(username))
        {
            return false;
        }
        else
        {
            userInfo.Add(username, password);
            return true;
        }
        
    }

    public bool VerifyUser(string username, string password)
    {
        return (userInfo.ContainsKey(username) && userInfo[username] == password);
    }
    
    
}
