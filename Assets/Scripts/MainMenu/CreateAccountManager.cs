using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Object = System.Object;

public class CreateAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField name;
    [SerializeField] private TMP_InputField age;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField password2;
    [SerializeField] private TextMeshProUGUI errorMsg;
    [SerializeField] private UserInfo userInfo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void LogIn()
    {
        errorMsg.text = "";
        if (username.text.Equals("") || password.text.Equals("") || password2.text.Equals("") || age.text.Equals("") || name.text.Equals(""))
        {
            errorMsg.text = "Please fill required fields: ";
            if(name.text.Equals(""))
                errorMsg.text += "Name | ";
            if(age.text.Equals(""))
                errorMsg.text += "Age | ";
            if (username.text.Equals(""))
                errorMsg.text += "Username | ";
            if(password.text.Equals(""))
                errorMsg.text += "Password | ";
            if(password2.text.Equals(""))
                errorMsg.text += "Password (2) |";
            if (!password.text.Equals(password2.text))
            {
                errorMsg.text += "\nPasswords do not match";
                return;
            }
            return;
        }

        if (!password.text.Equals(password2.text))
        {
            errorMsg.text += "\nPasswords do not match";
            return;
        }
        
        if (!userInfo.TryAddUser(username.text, password.text))
        {
            errorMsg.text += "\nThis username is already taken";
            
            if (!password.text.Equals(password2.text))
            {
                errorMsg.text += "\nPasswords do not match";
            }
            
            return;
            
        }

        SceneManager.LoadScene("MainMenu");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
