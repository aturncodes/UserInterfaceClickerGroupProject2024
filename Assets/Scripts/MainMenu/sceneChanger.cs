using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{

    [SerializeField] private CanvasGroup settingsMenu;
    [SerializeField] private CanvasGroup achievements;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        generators();
    }
    public void settings(){
        Debug.Log($"Before Change - Alpha: {settingsMenu.alpha}, Interactable: {settingsMenu.interactable}, BlocksRaycasts: {settingsMenu.blocksRaycasts}");

        settingsMenu.alpha = 1f;           
        settingsMenu.interactable = true;  
        settingsMenu.blocksRaycasts = true; 

        Debug.Log($"After Change - Alpha: {settingsMenu.alpha}, Interactable: {settingsMenu.interactable}, BlocksRaycasts: {settingsMenu.blocksRaycasts}");
    }
    public void acheivements(){
        achievements.alpha = 1f;           
        achievements.interactable = true;  
        achievements.blocksRaycasts = true; 
    }
    public void generators(){
        settingsMenu.alpha = 0f;           
        settingsMenu.interactable = false;  
        settingsMenu.blocksRaycasts = false;
        achievements.alpha = 0;           
        achievements.interactable = false;  
        achievements.blocksRaycasts = false; 
    }

    public void quitGame() {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }

}
