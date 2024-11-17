using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager: MonoBehaviour
{
    [Header("MainMenu UI")]
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private TMP_InputField username_mainMenu;
    [SerializeField] private TMP_InputField password_mainMenu;
    [SerializeField] private TextMeshProUGUI errorMsg_mainMenu;
    [SerializeField] private UserInfo userInfo;

    [Header("CreateAccount UI")] [SerializeField]
    private Canvas createAccount;
    [SerializeField] private TMP_InputField username_createAccount;
    [SerializeField] private TMP_InputField password_createAccount;
    [SerializeField] private TextMeshProUGUI errorMsg_createAccount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       // createAccount.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateAccount()
    {
        errorMsg_mainMenu.text = "";
        username_mainMenu.text = "";
        password_mainMenu.text = "";
        createAccount.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        errorMsg_createAccount.text = "";
        username_createAccount.text = "";
        password_createAccount.text = "";
        mainMenu.gameObject.SetActive(true);
        createAccount.gameObject.SetActive(false);
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

    public void SignUp()
    {
        if (username_createAccount.text.Equals("") || password_createAccount.text.Equals(""))
        {
            errorMsg_createAccount.text = "Please fill required fields";
            return;
        }

        if (userInfo.VerifyUser(username_createAccount.text, password_createAccount.text))
        {
            errorMsg_createAccount.text = "This username is already taken";
            return;
        }
        
        userInfo.AddUser(username_createAccount.text, password_createAccount.text);
        MainMenu();
    }

    public void Options()
    {
        //Code to show settings canvas
    }
    
    
    
}
