using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Generator : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private int points;
    [SerializeField] private float waitTime; 
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    
    //private static ScoreKeeper scoreKeeper;
    
    void Start()
    {
        text.text = waitTime.ToString();
    }


    void Update()
    {
        
    }

    public void StartTimer()
    {
        StartCoroutine(TimerRoutine());
    }
    
    //TODO: Disallow clicking during cooldown
    IEnumerator TimerRoutine()
    {
        float timer = 0; 
        while (timer <= waitTime)
        {
            // Update the text as time remaining
            text.text = (waitTime - timer).ToString("F1"); // Display with 1 decimal place

            // Increase timer by time since last frame
            timer += Time.deltaTime;

            // Update the slider based on the remaining time, normalized to [0, 1]
            slider.value = timer/waitTime;

            yield return null;  // Wait for the next frame
        }
        
        //Update Score
        ScoreKeeper.Singleton.IncreaseScore(points);
        
        // reset timer and slider value
        timer = 0;
        slider.value = 0;
        //remove
        text.text = waitTime.ToString();
    }
}
