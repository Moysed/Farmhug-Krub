using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlantItem : MonoBehaviour
{
    public TextMeshProUGUI text;
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
    private void Update()
    {

        if (Inventory.singleton.inventory.ContainsKey(plant.name) && text != null)
            text.text = Inventory.singleton.inventory[plant.name].ToString();
    }
    public void BuyPlant()
    {
        if(Inventory.singleton.coin >= plant.price)
        {
            Inventory.singleton.coin -= plant.price;
            Inventory.singleton.AddSeedtoInventory(plant.name);
            
            
            sfx.PlaySFX(sfx.BuyPlant);
        }
        else
        {
            Debug.Log("Not Enough Coin");
        }
        
    }

    public void ChoosePlant()
    {
        if(Inventory.singleton.inventory.ContainsKey(plant.name))
        {
            if(Inventory.singleton.inventory[plant.name] >= 1)
            {
                if(GroundMangement.singleton._tempPlantStatus!= null)
                {
                    Inventory.singleton.inventory[plant.name]--;
                    Debug.Log(Inventory.singleton.inventory[plant.name]);
                    farm.SelectPlant(this);
                    GroundMangement.singleton.closePanel();
                    GroundMangement.singleton.tempPlanting();
                }
                
            }
            
        }
        
    }
}
