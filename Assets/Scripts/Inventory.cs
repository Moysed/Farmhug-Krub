using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory singleton;
    public float sellTime;
    public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Use a dictionary to store product types and quantities

    private CoinManagement coinManager;
    public int coin;

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        
        coinManager = FindObjectOfType<CoinManagement>();
        coin = 15;
        sellTime = 30;
    }

    void Update()
    {
        coinManager.UpdateCoin(coin);
        sellTime -= Time.deltaTime;

        if(coin <= 0)
        {
            coin = 0;
        }

        if (sellTime < 0)
        {
            sellTime = 30;
        }

        
    }

    // Add a plant to the inventory
    public void AddToInventory(string productType)
    {
        if (inventory.ContainsKey(productType))
        {
            inventory[productType]++;
        }
        else
        {
            inventory.Add(productType, 1);
        }

        Debug.Log("Added " + productType + " to inventory. Total: " + inventory[productType]);
    }

    // Sell a specific amount of a product type from the inventory
    public void SellFromInventory(string productType, int amount)
    {
        if (inventory.ContainsKey(productType))
        {
            if (inventory[productType] >= amount)
            {
                inventory[productType] -= amount;
                Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);

                coin += amount;
            }
            else
            {
                Debug.Log("Not enough " + productType + " in inventory to sell.");
            }
        }
        else
        {
            Debug.Log("Plant type " + productType + " not found in inventory.");
        }
    }

    // Get the quantity of a specific product type in the inventory
    public int GetPlantQuantity(string productType)
    {
        if (inventory.ContainsKey(productType))
        {
            return inventory[productType];
        }
        else
        {
            return 0;
        }
    }
}
