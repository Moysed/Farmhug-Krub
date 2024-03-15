using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItem : MonoBehaviour
{
    public InfoObject animal;
    SFXManager sfx;
    CoopManager coop;

    void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>();
    }

    void Start()
    {
        coop = FindObjectOfType<CoopManager>();
    }

    public void BuyAnimal()
    {
        if(Inventory.singleton.coin >= animal.price)
        {
        Inventory.singleton.coin -= animal.price;
        Debug.Log("Buy :" + animal.ObjectName);
        coop.SelectAnimal(this);
        PetManagement.singleton.closePanel();
        PetManagement.singleton.tempAnimal();
        sfx.PlaySFX(sfx.BuyPlant);
        }
        else
        {
            Debug.Log("Not Enough Coin");
        }
    }
}
