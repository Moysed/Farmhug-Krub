using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public InfoObject plant;
    SFXManager sfx;
    FarmManager farm;

    void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>();
    }

    void Start()
    {
        farm = FindObjectOfType<FarmManager>();
    }

    public void BuyPlant()
    {
        if(Inventory.singleton.coin >= plant.price)
        {
            Inventory.singleton.coin -= plant.price;
            Debug.Log("Buy :" + plant.ObjectName);
            farm.SelectPlant(this);
            GroundMangement.singleton.closePanel();
            GroundMangement.singleton.tempPlanting();
            sfx.PlaySFX(sfx.BuyPlant);
        }
        else
        {
            Debug.Log("Not Enough Coin");
        }
        
    }
}
