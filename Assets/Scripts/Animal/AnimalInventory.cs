using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInventory : MonoBehaviour
{
    public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Use a dictionary to store plant types and quantities
    private float timer;

    private CoinManagement coinManager;
    public int coin;

    void Start()
    {
        coinManager = FindObjectOfType<CoinManagement>();
        coin = 5;
    }

    void Update()
    {
        coinManager.UpdateCoin(coin);

        if(coin <= 0)
        {
            coin = 0;
        }
    }

    // Add an animal to the inventory
    public void AddToInventory(string animalType)
    {
        if (inventory.ContainsKey(animalType))
        {
            inventory[animalType]++;
        }
        else
        {
            inventory.Add(animalType, 1);
        }

        Debug.Log("Added " + animalType + " to inventory. Total: " + inventory[animalType]);
    }

    // Sell a specific amount of an animal type from the inventory
    public void SellFromInventory(string animalType, int amount)
    {
        if (inventory.ContainsKey(animalType))
        {
            if (inventory[animalType] >= amount)
            {
                inventory[animalType] -= amount;
                Debug.Log("Sold " + amount + " " + animalType + "(s) from inventory. Remaining: " + inventory[animalType]);

                coin += amount;
            }
            else
            {
                Debug.Log("Not enough " + animalType + " in inventory to sell.");
            }
        }
        else
        {
            Debug.Log("Plant type " + animalType + " not found in inventory.");
        }
    }

    // Get the quantity of a specific animal type in the inventory
    public int GetPlantQuantity(string animalType)
    {
        if (inventory.ContainsKey(animalType))
        {
            return inventory[animalType];
        }
        else
        {
            return 0;
        }
    }
}
