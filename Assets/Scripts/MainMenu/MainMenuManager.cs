using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MainMenuManager: MonoBehaviour
{
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TextMeshProUGUI errorMsg;
    
    public void Login()
    {
        errorMsg.text = "";
        if (username.text.Equals("") || password.text.Equals(""))
        {
            errorMsg.text = "Please fill required fields: ";
            if (username.text.Equals(""))
                errorMsg.text += "Username | ";
            if(password.text.Equals(""))
                errorMsg.text += "Password";
            return;
        }

        if (!PlayerSystem.Singleton.LoginAttempt(username.text, password.text))
        {
            errorMsg.text = "Login Failed. The credentials you entered are incorrect.";
            return;
        }
        
        Player player = PlayerSystem.Singleton.PlayerLookup(username.text, password.text);

        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }
            
        PlayerSystem.Singleton.SetCurrentPlayer(player.playerId);

        /*if (SaveSystem.Singleton.DoesGameSaveExist(player.playerId))
        {
            SaveSystem.Singleton.LoadGameData(player.playerId);
            //TODO: loading animation/transition
        }
        else*/
        {
            GameManager.Singleton.NewGame();
            //TODO: loading animation/transition
        }

    }

    public void CreateAccount()
    {
        SceneManager.LoadScene("CreateAccount");
    }
    public void Quit()
    {
        Application.Quit();
    }
    
    public void Options()
    {
        //Code to show settings canvas
    }
    
}
