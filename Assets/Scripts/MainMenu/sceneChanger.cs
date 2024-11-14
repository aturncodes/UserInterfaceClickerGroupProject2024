using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    [SerializeField] private Canvas sidebarMenu;
    [SerializeField] private Object upgrades;
    [SerializeField] private Object managers;
    [SerializeField] private Object settings;
    [SerializeField] private Object acheivements;
    [SerializeField] private Object generators;
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
        SceneManager.LoadScene(upgrades.name);
    }
    public void managerChange(){
        SceneManager.LoadScene(managers.name);
    }
    public void settingsChange(){
        SceneManager.LoadScene(settings.name);
    }
    public void acheivementsChange(){
        SceneManager.LoadScene(acheivements.name);
    }
    public void generatorsChange(){
        SceneManager.LoadScene(generators.name);
    }

}
