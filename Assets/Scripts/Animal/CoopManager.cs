using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopManager : MonoBehaviour
{
    public AnimalItem selectAnimal;
    public bool isPeting = false;

    public CoinManagement coin;
    
    public InfoObject animal;
    
    private Inventory inventory;
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void SelectAnimal(AnimalItem newAnimal)
    {
        
            selectAnimal = newAnimal;
            if(inventory.coin >= selectAnimal.animal.price)
            {
                isPeting = true;
                //inventory.coin -= selectAnimal.animal.price;
            }
            else
            {
                isPeting = false;
            }
            //coin.UpdateCoin(inventory.coin);
        
    }
}
