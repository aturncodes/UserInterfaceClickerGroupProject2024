using System;
using System.Collections.Generic;
using UnityEngine;
public class Cart : MonoBehaviour
{
    [SerializeField] private List<GameObject> cart;
    public static Cart Singleton;

    public void Awake()
    {
        if (Singleton != null)
        {
            Destroy(this.gameObject);
        }

        Singleton = this;
        cart = new List<GameObject>();
    }

    public void AddToCart(GameObject itemPrefab)
    {
        //GameObject item = Instantiate(itemPrefab, vlg);
    }

    public void RemoveFromCart(GameObject item)
    {
        
        cart.Remove(item);
    }

    public void Undo()
    {
        
    }
    
    //public void 
    
}