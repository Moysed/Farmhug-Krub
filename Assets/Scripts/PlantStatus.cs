using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantStatus : MonoBehaviour
{
    public GameObject StatusPrefab;
    public Vector3 statusPos;
    public int waterTime;
    public  bool isWater = false;

    //public FarmManager fm;
    public GroundMangement gm;
    //PlantInventory plantInventory;

    public enum InstanceMode
    {
        Instance,
        Pool
    }

    public InstanceMode instanceMode = InstanceMode.Pool;

    void Start()
    {
        //plantInventory = FindObjectOfType<PlantInventory>();
        gm  = transform.parent.GetComponent<GroundMangement>();
        //fm = transform.parent.GetComponent<FarmManager>();
        waterTime = 0; // Adjust the initial grow time as needed
        //ShowStatus();
    }

    void Update()
    {
        if (gm.IsPlanted == true)
        {
            waterTime += 1;
        }
        if (gm.plantStage == gm.selectedPlant.plantStages.Length - 1)
        {
            waterTime = 0;
        }

        if (waterTime == 600)
        {
            //waterTime = -1; // Reset grow time for the new plant
            //if(waterTime == 0)
            //{
                Debug.Log("Watering");
                ShowStatus();
            
            //}
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
                    Debug.Log("Tapped");
                    Lean.Pool.LeanPool.Despawn(hit.collider.gameObject);
                    isWater = true;
                    //plantInventory.AddToInventory(hit.collider.gameObject.name);
                    waterTime = 0; // Reset grow time
                }
            }
        }
    }

    void ShowStatus()
    {
        GameObject newPlant = InstantiateObject(StatusPrefab);
        newPlant.tag = "Status";

        // Set the new plant's position
        newPlant.transform.position = statusPos;

        // Reset grow time for the new plant
        //waterTime = 10f;
        isWater = false;
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
