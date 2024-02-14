using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStatus : MonoBehaviour
{
    public GameObject StatusPrefab;
    public Vector3 statusPos;
    public int waterTime;
    public  bool isWater = false;

    GroundMangement gm;

    public bool IsPlanted = false;
    public SpriteRenderer plant;
    public int plantStage = 0;
    public string plantName;

    [SerializeField]
    BoxCollider2D plantCollider;
    float timer = 5;

    [SerializeField]
    PlantObject _selfPlatObjectInfo;

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
        if (IsPlanted == true)
        {
            waterTime += 1;
        }
        if(gm.selectedPlant != null)
            if (plantStage == gm.selectedPlant.plantStages.Length - 1)
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
                if (hit.collider.CompareTag("Status"))
                {
                    Plant plant = hit.collider.GetComponent<Plant>();

                    if(plant._ownerPlantObjectPrefabs.name.Contains(this.name))
                    {
                        Debug.Log("Tapped on Object:" + this.name);
                        isWater = true;

                        Debug.Log("Is Water : " + isWater);

                        Lean.Pool.LeanPool.Despawn(hit.collider.gameObject);

                        //plantInventory.AddToInventory(hit.collider.gameObject.name);
                        waterTime = 0; // Reset grow time
                    }

                    
                }
            }
        }

        if (IsPlanted == true)
        {

            if (plantStage <= gm.selectedPlant.plantStages.Length - 1)
            {
                if (isWater == true)
                {
                    plantStage++;
                }

                if (plantStage >= gm.selectedPlant.plantStages.Length)
                {
                    plantStage = 1;
                }
                UpdatePlant();
            }
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            gm.plantInventory.SellFromInventory(plantName, gm.plantInventory.GetPlantQuantity(plantName));
            timer = 5;
        }
    }

    void ShowStatus()
    {
        Plant newPlant = InstantiateObject(StatusPrefab).GetComponent<Plant>();
        newPlant.tag = "Status";

        // Set the new plant's position
        newPlant.transform.position = statusPos;
        newPlant._ownerPlantObjectPrefabs = this;
        // Reset grow time for the new plant
        //waterTime = 10f;
    }

    // Single Game Object
    public void Plant(PlantObject newPlant)
    {
        _selfPlatObjectInfo = newPlant;
        Debug.Log("Planted");
        gm.selectedPlant = newPlant;
        IsPlanted = true;
        plantStage = 0;
        plantName = newPlant.name;
        UpdatePlant();
        plant.gameObject.SetActive(true);
    }

    public void Harvest()
    {
        Debug.Log("Harvested");
        IsPlanted = false;
        gm.fm.isPlanting = false;
        plantStage = 0;
        isWater = false;
        plant.gameObject.SetActive(false);
        gm.plantInventory.AddToInventory(gm.selectedPlant.plantName);
    }

    // Single Game object
    void UpdatePlant()
    {
       

        if (plantStage >= _selfPlatObjectInfo.plantStages.Length)
            plantStage = _selfPlatObjectInfo.plantStages.Length - 1;

        plant.sprite = _selfPlatObjectInfo.plantStages[plantStage];
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
}
