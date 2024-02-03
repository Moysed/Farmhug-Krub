using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject plantObjectPrefabs;
    public Vector3 plantPos;
    float growTime;
    PlantInventory plantInventory;

    public enum InstanceMode
    {
        Instance,
        Pool
    }

    public InstanceMode instanceMode = InstanceMode.Pool;

    void Start()
    {
        plantInventory = FindObjectOfType<PlantInventory>();
        growTime = 10f; // Adjust the initial grow time as needed
        SpawnPlant();
    }

    void Update()
    {
        growTime -= Time.deltaTime;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Plant"))
                {
                    Debug.Log("Tapped");
                    Lean.Pool.LeanPool.Despawn(hit.collider.gameObject);
                    plantInventory.AddToInventory(hit.collider.gameObject.name);
                    growTime = 10f; // Reset grow time
                }
            }
        }

        if (growTime <= 0)
        {
            Debug.Log("Spawned");
            SpawnPlant();
            growTime = 10f; // Reset grow time for the new plant
        }
    }

    void SpawnPlant()
    {
        GameObject newPlant = InstantiateObject(plantObjectPrefabs);
        newPlant.tag = "Plant";

        // Set the new plant's position
        newPlant.transform.position = plantPos;

        // Reset grow time for the new plant
        growTime = 10f;
    }

    GameObject InstantiateObject(GameObject obj)
    {
        if (instanceMode == InstanceMode.Instance)
            return Instantiate(obj);
        else if (instanceMode == InstanceMode.Pool)
            return Lean.Pool.LeanPool.Spawn(obj);

        return null;
    }

    void SellPlantsFromInventory()
    {
        int amountToSell = 5;

        // Assuming you have a method in PlantInventory to handle selling from the inventory
        plantInventory.SellFromInventory(plantInventory.name, amountToSell);
    }
}
