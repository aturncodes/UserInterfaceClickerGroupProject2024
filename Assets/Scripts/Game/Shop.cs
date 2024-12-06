using System;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button generatorButton;
    [SerializeField] private Button managerButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject searchBar;
    
    //TODO: Switch GameObject type to Actual UI type
    [SerializeField] private GameObject generators;
    [SerializeField] private GameObject managers;
    [SerializeField] private GameObject upgrades;

    [SerializeField] private GameObject testGenPrefab;
    [SerializeField] private GameObject testManPrefab;

    
    public void GeneratorSelect()
    {
        generators.gameObject.SetActive(true);
        managers.gameObject.SetActive(false);
        upgrades.gameObject.SetActive(false);
    }
    
    public void MangerSelect()
    {
        generators.gameObject.SetActive(false);
        managers.gameObject.SetActive(true);
        upgrades.gameObject.SetActive(false);
    }
    
    public void UpgradeSelect()
    {
        generators.gameObject.SetActive(false);
        managers.gameObject.SetActive(false);
        upgrades.gameObject.SetActive(true);
    }
    
    public void AddToCart()
    {
        
    }
    
    public void Buy()
    { 
        /*int playerScore = ScoreKeeper.Singleton.GetScore();

        if (marketPrice <= playerScore)
        {
            ScoreKeeper.Singleton.BuyItem(marketPrice); 
            if (itemType == ItemType.Generators)
            {
                GameObject genObject = GameManager.Singleton.GetLockedGenerator();
                if (genObject == null)
                {
                    Debug.LogError("Could not find open slot");
                    return;
                }
                
                Generator gen = genObject.GetComponent<Generator>();
                GameManager.Singleton.AddGenerator(itemPrefab, gen.slotId);
            } else if (itemType == ItemType.Managers)
            {
                GameObject genObject = GameManager.Singleton.GetOpenManagerGenerator();
                if (genObject == null)
                {
                    Debug.LogError("Could not find open manager pos");
                    return;
                }

                Generator gen = genObject.GetComponent<Generator>();
                GameManager.Singleton.AddManager(itemPrefab, gen.slotId);

            }
            else
            {
                //TODO: upgrades code
            }
            
        }*/


        int playerScore = ScoreKeeper.Singleton.GetScore();

        //int marketPrice = testBuyPrefab.GetComponent<Generator>().marketPrice;
        int marketPrice = 3;
        if (marketPrice <= playerScore)
        {
            GameObject genObject = GameManager.Singleton.GetLockedGenerator();
            if (genObject == null)
            {
                Debug.LogError("Could not find open gen slot");
                return;
            }
            
            ScoreKeeper.Singleton.BuyItem(marketPrice);
            Generator gen = genObject.GetComponent<Generator>();
            GameManager.Singleton.AddGenerator(testGenPrefab, gen.slotId);

        }

    }
    
    public void TestBuyManager()
    { 
        
        int playerScore = ScoreKeeper.Singleton.GetScore();
        //int marketPrice = testBuyPrefab.GetComponent<Generator>().marketPrice;
        int marketPrice = 3;
        if (marketPrice <= playerScore)
        {
            GameObject genObject = GameManager.Singleton.GetOpenManagerGenerator();
            if (genObject == null)
            {
                Debug.LogError("Could not find open gen slot");
                return;
            }
            
            Generator gen = genObject.GetComponentInChildren<Generator>();
            if (gen == null)
            {
                Debug.LogError("Gen is null!");
            }
            GameManager.Singleton.AddManager(testManPrefab, gen.slotId);
            ScoreKeeper.Singleton.BuyItem(marketPrice);

        }

    }
    
}