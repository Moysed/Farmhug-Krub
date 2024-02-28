using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItem : MonoBehaviour
{
    public InfoObject animal;

    CoopManager coop;

    void Start()
    {
        coop = FindObjectOfType<CoopManager>();
    }

    public void BuyAnimal()
    {
        Inventory.singleton.coin -= animal.price;
        Debug.Log("Buy :" + animal.ObjectName);
        coop.SelectAnimal(this);
        PetManagement.singleton.closePanel();
        PetManagement.singleton.tempAnimal();
    }
}
