using System.IO;
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
            }
            return;
        }

        if (!password.text.Equals(password2.text))
        {
            errorMsg.text += "\nPasswords do not match";
            return;
        }
        
        if (!PlayerSystem.Singleton.TryAddLogin(username.text, password.text))
        {
            errorMsg.text += "\nThis username is already taken";
            
            if (!password.text.Equals(password2.text))
            {
                errorMsg.text += "\nPasswords do not match";
            }
            
            return;
            
        }
        
        Player player = PlayerSystem.Singleton.PlayerLookup(username.text, password.text);

        if (player != null)
        {
            Debug.LogError("Account taken!");
            return;
        }
        
        player = new Player()
        {
            playerId = 0,
            name = this.name.text,
            age = this.age.text,
            username = this.username.text,
            password = this.password.text,
            //TODO: code for icon
            //iconName = 
            iconName = null,
            //Settings, etc
        };
        PlayerSystem.Singleton.AddPlayer(player);
            
        PlayerSystem.Singleton.SetCurrentPlayer(player.playerId);

        /*if (SaveSystem.Singleton.DoesGameSaveExist(player.playerId))
        {
            Debug.LogError("Save Exists!");
        }
        else
        {
            if (GameManager.Singleton == null)
            {
                Debug.LogError("Game manager is null");
            }
            GameManager.Singleton.NewGame();
        }*/
        GameManager.Singleton.NewGame();
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
