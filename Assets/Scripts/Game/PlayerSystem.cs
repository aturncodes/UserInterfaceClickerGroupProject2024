using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public int playerId;
    public string name;
    public string age;
    public string username;
    public string password;
    public string iconName;
    //public Settings settings;
    //Additional player info
    
}

[System.Serializable]
public class PlayerData
{
    public Player[] players;
    public int nextPlayerId; 
}

public class PlayerSystem : MonoBehaviour
{

    [SerializeField] private Dictionary<string, string> playerLogin;
    [SerializeField] private int nextPlayerId;

    public static PlayerSystem Singleton;
    private List<Player> playerList;

    private int currentPlayer;
    void OnEnable()
    {
        if (Singleton != null)
        {
            Destroy(this);
        }
        
        Singleton = this;
        DontDestroyOnLoad(this);

        playerLogin = new Dictionary<string, string>();
        playerList = new List<Player>();

        /*if (SaveSystem.Singleton.DoesPlayerSaveExist())
        {
            SaveSystem.Singleton.LoadPlayerData();
        }*/
        //else
        {
            currentPlayer = -1;
            nextPlayerId = 0;
        }
    }
    
    public bool TryAddLogin(string username, string password)
    {
        if (PlayerLookup(username, password) != null)
        {
            Debug.LogError("A player has taken this username!");
            return false;
        }
        return true;
    }

    public void AddPlayer(Player player)
    {
        if (player == null)
        {
            Debug.LogError("Player is null!");
            return;
        }
        player.playerId = nextPlayerId;
        playerList.Add(player);
        playerLogin.Add(player.username, player.password);
        Debug.Log("Player Add to List");
        nextPlayerId++;
    }
    
    public bool LoginAttempt(string username, string password)
    {
        return (playerLogin.ContainsKey(username) && playerLogin[username] == password);
    }

    public Player PlayerLookup(string username, string password)
    {
        if (playerList.Count == 0)
        {
            Debug.LogError("The player list is empty!");
            return null;
        }
        
        foreach (Player player in playerList)
        {
            if (username.Equals(player.username) && password.Equals(player.password))
            {
                return player;
            }
        }

        return null;
    }

    public void Save(ref PlayerData data)
    {
        /*List<Player> savePlayers = new List<Player>();
        for (int i = playerList.Count - 1; i >= 0; i--)
        {
            if (playerList[i] != null)
            {
                savePlayers.Add(playerList[i]);
                //TODO: try Player player = new Player{}
            }
            else
            {
                playerList.Remove(playerList[i]);
            }
        }*/
        data.players = playerList.ToArray();
        data.nextPlayerId = nextPlayerId;
    }

    public void SetCurrentPlayer(int playerId)
    {
        currentPlayer = playerId;
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void Load(PlayerData data)
    {

        if (data.players.Length <= 0)
        {
            Debug.LogError("Players not found");
            return;
        }

        playerList.Clear();
        playerList.TrimExcess();

        nextPlayerId = data.nextPlayerId;

        foreach (Player player in data.players)
        {
            if (player != null)
            {
                playerList.Add(player);
                playerLogin.Add(player.username, player.password);
            }
        }
    

    }
    
}