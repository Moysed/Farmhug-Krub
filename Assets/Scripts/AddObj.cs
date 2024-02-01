using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObj : MonoBehaviour
{
    public List<GameObject> plantGmaeobj = new List<GameObject>();

    float growTime;
    bool isDisplay = false;
    GameObject currentPlant;
    Animator m_animator;

    PlantInventory plantInventory;

    void Start()
    {
        m_animator = GetComponent<Animator>();
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
                if (hit.transform.tag == "Animal")
                {
                    m_animator.SetBool("IsCall", true);

                    if (isDisplay)
                    {
                        Debug.Log("Tapped");
                        Destroy(currentPlant);
                        isDisplay = false;
                        plantInventory.AddToInventory(currentPlant.name);
                        growTime = 10f; // Reset grow time
                    }
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
        if (currentPlant != null)
        {
            Destroy(currentPlant);
        }

        currentPlant = Instantiate(plantGmaeobj[Random.Range(0, 1)], new Vector3(Random.Range(0, 2), Random.Range(0, 2)), Quaternion.identity);
        currentPlant.tag = "Animal";
        isDisplay = true;
        growTime = 10f; // Reset grow time for the new plant
    }

    void SellPlantsFromInventory()
    {
        int amountToSell = 5;

        plantInventory.SellFromInventory(plantInventory.name, amountToSell);
    }
}
