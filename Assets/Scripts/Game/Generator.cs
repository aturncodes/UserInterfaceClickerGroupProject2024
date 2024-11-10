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
    
    IEnumerator TimerRoutine()
    {
        //disable clicking
        button.interactable = false;
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
        
        //Delay clear speed
        yield return new WaitForSeconds(0.05f);
        
        // reset timer and slider value and enable clicking
        timer = 0;
        slider.value = 0;
        button.interactable = true;
        
        //TODO: remove testing
        text.text = waitTime.ToString();
    }
}
