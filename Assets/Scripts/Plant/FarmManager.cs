using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;

    public CoinManagement coin;
    

    private Inventory inventory;
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void SelectPlant(PlantItem newPlant)
    {
            selectPlant = newPlant;
            if(inventory.coin >= 0)
            {
                isPlanting = true;
            }
            else
            {
                isPlanting = false;
            }
            coin.UpdateCoin(inventory.coin);
    }
}
