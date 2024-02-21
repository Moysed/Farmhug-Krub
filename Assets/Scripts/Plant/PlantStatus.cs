using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlantStatus : BaseStatus
{
    public GameObject StatusPrefab;
    public Vector3 statusPos;
    public int waterTime;
    public  bool isWater = false;
    GroundMangement gm;
    public SpriteRenderer plant;
 
 
    [SerializeField]
     BoxCollider2D plantCollider;
    public float afterWatertime = 0;
 
    [SerializeField]
    InfoObject _selfPlantObjectInfo;
 
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
 
 
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
 
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
 
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("PlantStatus'"))
                {
                    Plant plant = hit.collider.GetComponent<Plant>();
                    if (plant._ownerPlantObjectPrefabs.name.Contains(this.name))
                    {
                       
                        //Debug.Log("Tapped on Object:" + this.name);
                        //Debug.Log("Is Water : " + isWater);
                        afterWatertime = 5;
                        //isWater = true;
                        isWater = true;
                        
 
                        Lean.Pool.LeanPool.Despawn(hit.collider.gameObject);
 
                        //plantInventory.AddToInventory(hit.collider.gameObject.name);
                        waterTime = 0; // Reset grow time
                    } 
                }
            }
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
        Plant newPlant = InstantiateObject(StatusPrefab).GetComponent<Plant>();
        newPlant.tag = "PlantStatus'";
 
        // Set the new plant's position
        newPlant.transform.position = statusPos;
        newPlant._ownerPlantObjectPrefabs = this;
        // Reset grow time for the new plant
        //waterTime = 10f;
    }
 
    // Single Game Object
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

    public void waterCheck()
    {
        Plant plant = GetComponent<Plant>();
        if (plant._ownerPlantObjectPrefabs.name.Contains(this.name))
        {

            //Debug.Log("Tapped on Object:" + this.name);
            //Debug.Log("Is Water : " + isWater);
            afterWatertime = 5;
            //isWater = true;
            isWater = true;

            waterTime = 0; // Reset grow time
        }
    }


}
