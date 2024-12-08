using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Generator : MonoBehaviour
{

    [SerializeField] public string name;
    [SerializeField] public int level;
    [SerializeField] public int slotId;
    [SerializeField] public int points;
    [SerializeField] public int marketPrice;
    [SerializeField] public float waitTime;
    [SerializeField] private Slider slider;
    //[SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button generateButton;
    public GameObject manager;
    [SerializeField] private Button sellButton;
    
    private void Awake()
    { 
        manager = null;
    }

    void Start()
    {
        //text.text = waitTime.ToString();
    }

    public void Sell()
    {
        ScoreKeeper.Singleton.SellItem(marketPrice);
        if (manager != null)
        {
            ScoreKeeper.Singleton.SellItem(manager.GetComponent<Manager>().marketPrice);
        }
        GameManager.Singleton.SellGenerator(slotId);
        Destroy(transform.parent.gameObject);
    }

    public Vector3 GetButtonPosition()
    {
        Vector3 pos = generateButton.transform.position;
        Destroy(generateButton.gameObject);
        return pos;
    }

    public void StartTimer()
    {
        StartCoroutine(TimerRoutine());
        
        IEnumerator TimerRoutine()
        {
            //disable clicking
            if (manager == null)
            {
                generateButton.interactable = false;
            }
            float timer = 0; 
            while (timer <= waitTime)
            {
                // Update the text as time remaining
                //text.text = (waitTime - timer).ToString("F1"); // Display with 1 decimal place

                // Increase timer by time since last frame
                timer += Time.deltaTime;

                // Update the slider based on the remaining time, normalized to [0, 1]
                slider.value = timer/waitTime;

                yield return null;  // Wait for the next frame
            }
        
            //Update Score
            ScoreKeeper.Singleton.GeneratedPoints(points);
        
            //Delay clear speed
            yield return new WaitForSeconds(0.05f);
        
            // reset timer and slider value and enable clicking
            timer = 0;
            slider.value = 0;
            if (manager == null)
            {
                generateButton.interactable = true;
            }
        
            //TODO: remove testing
            //text.text = waitTime.ToString();
        }
    }
    
    public void ManagerActivate()
    {
        StartCoroutine(ContinuousTimer());
        
        IEnumerator ContinuousTimer()
        {
            while(true){
                float timer = 0; 
                while (timer <= waitTime)
                {
                    // Update the text as time remaining
                    //text.text = (waitTime - timer).ToString("F1"); // Display with 1 decimal place

                    // Increase timer by time since last frame
                    timer += Time.deltaTime;

                    // Update the slider based on the remaining time, normalized to [0, 1]
                    slider.value = timer/waitTime;

                    yield return null;  // Wait for the next frame
                }
            
                //Update Score
                ScoreKeeper.Singleton.GeneratedPoints(points);
            
                //Delay clear speed
                yield return new WaitForSeconds(0.05f);
            
                // reset timer and slider value and enable clicking
                timer = 0;
                slider.value = 0;
            
                //TODO: remove testing
                //text.text = waitTime.ToString();
            }
        }
    }

    public int GetSlotId()
    {
        return slotId;
    }

    public void SetSlotId(int slotId)
    {
        this.slotId = slotId;
    }
    
    
}
