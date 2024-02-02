using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObj : MonoBehaviour
{
    public List<GameObject> planTobjects = new List<GameObject>();
    public Vector3 plantpos;
    float growTime;
    bool isDisplay = false;
    List<GameObject> spawnedPlants = new List<GameObject>(); // Track all spawned plants

    PlantInventory plantInventory;

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
                if (hit.collider.CompareTag("Animal"))
                {
                    Debug.Log("Tapped");
                    Destroy(hit.collider.gameObject); // Destroy the touched plant
                    isDisplay = false;
                    plantInventory.AddToInventory(hit.collider.gameObject.name);
                    growTime = 10f; // Reset grow time
                }
            }
        }

        if (growTime <= 0 && !isDisplay)
        {
            Debug.Log("Spawned");
            SpawnPlant();
            isDisplay = true;
        }
    }

    void SpawnPlant()
    {
        GameObject newPlant = Instantiate(planTobjects[Random.Range(0, 2)], plantpos, Quaternion.identity);
        newPlant.tag = "Animal";
        spawnedPlants.Add(newPlant); // Add the new plant to the list
        isDisplay = true;
        growTime = 10f; // Reset grow time for the new plant
    }

    void SellPlantsFromInventory()
    {
        int amountToSell = 5;

        // Assuming you have a method in PlantInventory to handle selling from the inventory
        plantInventory.SellFromInventory(plantInventory.name, amountToSell);
    }
}