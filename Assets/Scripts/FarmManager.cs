using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;

    public CoinManagement coin;
    

    private PlantInventory inventory;
    void Start()
    {
        inventory = FindObjectOfType<PlantInventory>();
    }

    public void SelectPlant(PlantItem newPlant)
    {
        
            selectPlant = newPlant;
            if(inventory.coin >= 5)
            {
                isPlanting = true;
                inventory.coin -= 5;
            }
            else
            {
                isPlanting = false;
            }
            coin.UpdateCoin(inventory.coin);
        
    }
}
