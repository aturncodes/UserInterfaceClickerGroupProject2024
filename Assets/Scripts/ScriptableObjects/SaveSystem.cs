/*
using System;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using System.IO;
using System.Linq;

[System.Serializable]
public class GameSave
{
    public GameManagerData gameManagerdata;

    public ScoreData scoreData;
}
[System.Serializable]
public struct PlayerSave
{
    public PlayerData playerData;
}

[CreateAssetMenu(fileName = "SaveSystem", menuName = "ScriptableObjects/SaveSystem")]
public class SaveSystem : ScriptableObject
{
    
    public static SaveSystem Singleton;
    
    private static GameSave gameSave;
    private static PlayerSave playerSave;
    
    private int nextPlayerId;
    private bool newGame;
    private const string PLAYER = "players";
    private const string GAME = "game";
    private string date;
    //private static Player currentPlayer;
        
    private void OnEnable()
    {
        if (Singleton != null)
        {
            Destroy(this);
        }

        Singleton = this;
        
        gameSave = new GameSave()
        {
            gameManagerdata = new GameManagerData(),
            scoreData = new ScoreData()
        };
        playerSave = new PlayerSave()
        {
            playerData = new PlayerData()
        };

        /*if (File.Exists(GetPlayerSavePath()))
        {
            LoadPlayerData();
        } #1#
    }

    public bool DoesGameSaveExist(int playerId)
    {
        if(File.Exists(GetGameSavePath(playerId)))
        {
            return true;
        }
        Debug.LogError("No save exists");
        return false;
    }

    public bool DoesPlayerSaveExist()
    {
        if (File.Exists(GetPlayerSavePath()))
        {
            return true;
        }
        Debug.Log("No player save exists");
        return false;
    }

    public string GetPlayerSavePath()
    {
        string fileName = Application.persistentDataPath + "/" + PLAYER + ".save";
        return fileName;
    }

    public string GetGameSavePath(int playerId)
    {
        string fileName = Application.persistentDataPath + "/" + GAME +
                          "_" + playerId + ".save";
        return fileName;
    }

    public void LoadGameData(int playerId)
    {
        string jsonString = File.ReadAllText(GetGameSavePath(playerId));
        gameSave = JsonUtility.FromJson<GameSave>(jsonString);
        //handleloaddata gm.load
        GameManager.Singleton.Load(gameSave.gameManagerdata);
        ScoreKeeper.Singleton.Load(gameSave.scoreData);
    }

    public void SaveGameData()
    {
        //include handlesavedata from gm.save
        GameManager.Singleton.Save(ref gameSave.gameManagerdata);
        ScoreKeeper.Singleton.Save(ref gameSave.scoreData);
        foreach (Slot slot in gameSave.gameManagerdata.slots)
        {
            Debug.Log(slot.generatorPrefab.name + "gen");
        }
        File.WriteAllText(GetGameSavePath(PlayerSystem.Singleton.GetCurrentPlayer()), JsonUtility.ToJson(gameSave, true));
    }
    
    public void LoadPlayerData()
    {
        string jsonString = File.ReadAllText(GetPlayerSavePath());
        playerSave = JsonUtility.FromJson<PlayerSave>(jsonString);
        //handleloaddata gm.load
        PlayerSystem.Singleton.Load(playerSave.playerData);
    }

    public void SavePlayerData()
    {
        //include handlesavedata from gm.save
        PlayerSystem.Singleton.Save(ref playerSave.playerData);
        File.WriteAllText(GetPlayerSavePath(), JsonUtility.ToJson(playerSave, true));
    }
    
}
*/
