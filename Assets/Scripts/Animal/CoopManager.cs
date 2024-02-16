using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopManager : MonoBehaviour
{
    public AnimalItem selectAnimal;
    public bool isPeting = false;

    public CoinManagement coin;
    

    private AnimalInventory inventory;
    void Start()
    {
        inventory = FindObjectOfType<AnimalInventory>();
    }

    public void SelectAnimal(AnimalItem newAnimal)
    {
        
            selectAnimal = newAnimal;
            if(inventory.coin >= 5)
            {
                isPeting = true;
                inventory.coin -= 5;
            }
            else
            {
                isPeting = false;
            }
            //coin.UpdateCoin(inventory.coin);
        
    }
}
