using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject cartPrefab;
    [SerializeField] private Cart cart;
    [SerializeField] private ItemType itemType;
    private enum ItemType
    {
        Generators, Managers, Upgrades
    }
    
    private Generator gen;
    private Manager manager;
    private string name;
    private int marketPrice;
    private string description;
    //TODO: add icon code
    private Image icon;
    
    public void Awake()
    {
        if (itemType == ItemType.Generators)
        {
            gen = itemPrefab.GetComponent<Generator>();
            name = gen.name;
            marketPrice = gen.marketPrice;
        } else if (itemType == ItemType.Managers)
        {
            manager = itemPrefab.GetComponent<Manager>();
            name = manager.name;
            marketPrice = manager.marketPrice;
        }
        else
        {
            //TODO: upgrade code
            /*upgrade = itemPrefab.GetComponent<Upgrade>();
            name = upgrade.name;
            marketPrice = upgrade.marketPrice;*/
        }
        

    }

    public void Select()
    { 
        int playerScore = ScoreKeeper.Singleton.GetScore();

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
            
        }
        
    }
    
}