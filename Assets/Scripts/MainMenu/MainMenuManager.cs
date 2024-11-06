using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager: MonoBehaviour
{
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Canvas createAccount;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateAccount()
    {
        mainMenu.enabled = false;
        createAccount.enabled = true;
    }

    public void MainMenu()
    {
        mainMenu.enabled = true;
        createAccount.enabled = false;
    }

    public void Login()
    {
        //Code to verify login credentials
        //if doesnt match or doesnt exist error
        SceneManager.LoadScene("Game");
    }

    public void Options()
    {
        //Code to show settings canvas
    }
    
    
    
}
