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

        Debug.Log("Buy :" + animal.animalName);
        coop.SelectAnimal(this);
        PetManagement.singleton.tempAnimal();
    }
}
