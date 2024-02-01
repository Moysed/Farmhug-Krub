using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInventory : MonoBehaviour
{
    private Dictionary<string, int> inventory = new Dictionary<string, int>(); // Use a dictionary to store plant types and quantities

    // Add a plant to the inventory
    public void AddToInventory(string plantType)
    {
        if (inventory.ContainsKey(plantType))
        {
            inventory[plantType]++;
        }
        else
        {
            inventory.Add(plantType, 1);
        }

        Debug.Log("Added " + plantType + " to inventory. Total: " + inventory[plantType]);
    }

    // Sell a specific amount of a plant type from the inventory
    public void SellFromInventory(string plantType, int amount)
    {
        if (inventory.ContainsKey(plantType))
        {
            if (inventory[plantType] >= amount)
            {
                inventory[plantType] -= amount;
                Debug.Log("Sold " + amount + " " + plantType + "(s) from inventory. Remaining: " + inventory[plantType]);
            }
            else
            {
                Debug.Log("Not enough " + plantType + " in inventory to sell.");
            }
        }
        else
        {
            Debug.Log("Plant type " + plantType + " not found in inventory.");
        }
    }

    // Get the quantity of a specific plant type in the inventory
    public int GetPlantQuantity(string plantType)
    {
        if (inventory.ContainsKey(plantType))
        {
            return inventory[plantType];
        }
        else
        {
            return 0;
        }
    }
}
