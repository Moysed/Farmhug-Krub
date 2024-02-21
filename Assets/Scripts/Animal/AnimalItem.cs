using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItem : MonoBehaviour
{
    public AnimalObject animal;

    CoopManager coop;

    void Start()
    {
        coop = FindObjectOfType<CoopManager>();
    }

    public void BuyAnimal()
    {
        Inventory.singleton.coin -= animal.price;
        Debug.Log("Buy :" + animal.animalName);
        coop.SelectAnimal(this);
        PetManagement.singleton.tempAnimal();
    }
}
