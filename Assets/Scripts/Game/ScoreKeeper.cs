using System;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;
    public static ScoreKeeper Singleton;
    
    private void Awake()
    {
        //Create only one instance
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        //Set equal to player score on login once player settings are set up
        score = 0;
        scoreText.text = score.ToString();
    }
    
    //Decreasing player score/currency
    public void BuyItem(int cost)
    {
        SetScore(score - cost);
    }
    //Increasing player score/currency
    public void GeneratedPoints(int points)
    {
        SetScore(score + points);
    }

    public void SellItem(int marketPrice)
    {
        float sellBackPercentage = 0.4f;
        int sellBackTotal = (int)Math.Round(sellBackPercentage * marketPrice);
        SetScore(score + sellBackTotal);
    }

    //Retrieve score/currency
    public int GetScore()
    {
        return score;
    }

    public void Save(ref ScoreData saveScore)
    {
        saveScore.score = score;
    }

    public void Load(ScoreData saveScore)
    {
        SetScore(saveScore.score);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }
    
}

[System.Serializable]
public class ScoreData
{
    public int score;
}