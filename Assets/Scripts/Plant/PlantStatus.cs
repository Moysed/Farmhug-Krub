using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlantStatus : BaseStatus
{
    public GameObject StatusPrefab;
    public Vector3 statusPos;
    GroundMangement gm;
    public SpriteRenderer plant;
 
 
    [SerializeField]
     BoxCollider2D plantCollider;
 
    [SerializeField]
    InfoObject _selfPlantObjectInfo;
    Object Plant;
 
    public enum InstanceMode
    {
        Instance,
        Pool
    }
 
    public InstanceMode instanceMode = InstanceMode.Pool;
 
    void Start()
    {
        gm = GroundMangement.singleton;
        waterTime = 0; // Adjust the initial grow time as needed
    }
 
    void Update()
    {
        if(IsPlanted && waterTime >= 0 && waterTime <= 600 )
        {
            if(afterWatertime <= 0)
            {
                waterTime++;
            }

        }
        if (IsPlanted == true)
        {
            afterWatertime -= Time.deltaTime;
        }
        if(gm.selectedPlant != null)
            if (plantStage == gm.selectedPlant.ObjectStages.Length - 1)
            {
                waterTime = 0;
            }
 
        if (waterTime == 600)
        {
                Debug.Log("Watering");
                ShowStatus();
        }
        else if(waterTime > 600)
        {
            waterTime = 601;
        }
 
        if (IsPlanted == true)
        {
            if ( afterWatertime <= 0 && isWater == true)
            {

                        plantStage++;


 
                if (plantStage >= gm.selectedPlant.ObjectStages.Length)
                {
                    plantStage = 1;
                }
                UpdatePlant();
            }
        }   

        if (gm.inventory.autoSell.sellTime <= 0)
        {
            gm.inventory.SellFromInventory(_selfPlantObjectInfo.ObjectName, gm.inventory.GetPlantQuantity(plantName));
        }
    }
    void ShowStatus()
    {
        Object newPlant = InstantiateObject(StatusPrefab).GetComponent<Object>();
        newPlant.tag = "PlantStatus'";
 
        // Set the new plant's position
        newPlant.transform.position = statusPos;
        newPlant._ownerPlantObjectPrefabs = this;
        // Reset grow time for the new plant
        //waterTime = 10f;
    }
 
    // Single Game Object
 
    public override void Collected()
    {
        Debug.Log("Harvested");
        IsPlanted = false;
        gm.fm.isPlanting = false;
        plantStage = 0;
        isWater = false;
        plant.gameObject.SetActive(false);
        gm.inventory.AddToInventory(_selfPlantObjectInfo.ObjectName);
    }
 
    // Single Game object
    void UpdatePlant()
    {
        if (plantStage >= _selfPlantObjectInfo.ObjectStages.Length)
            plantStage = _selfPlantObjectInfo.ObjectStages.Length;
 
        plant.sprite = _selfPlantObjectInfo.ObjectStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.size.y / 2);
    }
 
    GameObject InstantiateObject(GameObject obj)
    {
        if (instanceMode == InstanceMode.Instance)
            return Instantiate(obj);
        else if (instanceMode == InstanceMode.Pool)
            return Lean.Pool.LeanPool.Spawn(obj);
 
        return null;
    }

    public override void CallUpdate()
    {
        //waterCheck
        //base.CallUpdate();
    }

    public override void UpdateInfo(InfoObject newPlant)
    {
        //Debug.Log("Yo");
        IsPlanted = true;
        _selfPlantObjectInfo = newPlant;
        //Debug.Log("Planted");
        gm.selectedPlant = newPlant;
        plantStage = 0;
        plantName = newPlant.name;
        
        UpdatePlant();
        plant.gameObject.SetActive(true);
    }

    public override void statusTap(Object plant)
    {
        
        Plant = plant;
        if (plant._ownerPlantObjectPrefabs.name.Contains(this.name))
        {
            afterWatertime = 5;
            //isWater = true;
            isWater = true;            
 
            Lean.Pool.LeanPool.Despawn(plant);
 
            //plantInventory.AddToInventory(hit.collider.gameObject.name);
            waterTime = 0; // Reset grow time
        } 
                
    }
}
