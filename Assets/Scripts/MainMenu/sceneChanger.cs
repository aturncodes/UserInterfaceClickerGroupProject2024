using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void upgradeChange(){
        Debug.Log("Upgrade Button Clicked");
        SceneManager.LoadScene("Upgrades");
    }
    public void managerChange(){
        SceneManager.LoadScene("Managers");
    }
    public void settingsChange(){
        SceneManager.LoadScene("Settings");
    }
    public void acheivementsChange(){
        SceneManager.LoadScene("Achievements");
    }
    public void generatorsChange(){
        SceneManager.LoadScene("Game");
    }

    public void quitGame() {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }

}
