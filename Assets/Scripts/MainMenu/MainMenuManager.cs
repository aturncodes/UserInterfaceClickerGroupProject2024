using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MainMenuManager: MonoBehaviour
{
    [Header("MainMenu UI")]
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private TMP_InputField username_mainMenu;
    [SerializeField] private TMP_InputField password_mainMenu;
    [SerializeField] private TextMeshProUGUI errorMsg_mainMenu;
    [SerializeField] private UserInfo userInfo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateAccount()
    {
        SceneManager.LoadScene("CreateAccount");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Login()
    {
        //Code to verify login credentials
        //if doesnt match or doesnt exist error

        if (username_mainMenu.text.Equals("") || password_mainMenu.text.Equals(""))
        {
            errorMsg_mainMenu.text = "Please fill required fields";
            return;
        }

        if (!userInfo.VerifyUser(username_mainMenu.text, password_mainMenu.text))
        {
            errorMsg_mainMenu.text = "Username or password is incorrect";
            return;
        }
        
        SceneManager.LoadScene("Game");

    }
    public void Options()
    {
        //Code to show settings canvas
    }
    
    
    
}
