using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public PlantObject plant;

    FarmManager farm;

    void Start()
    {
        farm = FindObjectOfType<FarmManager>();
    }

    public void BuyPlant()
    {
        Inventory.singleton.coin -= plant.price;
        Debug.Log("Buy :" + plant.plantName);
        farm.SelectPlant(this);
        GroundMangement.singleton.tempPlanting();
    }
}
