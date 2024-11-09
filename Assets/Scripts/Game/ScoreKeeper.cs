using System;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private int score;
    
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set equal to player score on login once player settings are set up
        score = 0;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseScore(int points)
    {
        score -= points;
        scoreText.text = "Score: " + score;
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
    
    
    public void SetScore(int score)
    {
        this.score = score;
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }
    
}
