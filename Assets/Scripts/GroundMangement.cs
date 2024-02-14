using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GroundMangement : MonoBehaviour
{
    public bool IsPlanted = false;
    public SpriteRenderer plant;
    public int plantStage = 0;

    public GameObject storePanel;
    public PlantObject selectedPlant;

    PlantStatus status;

    PlantInventory plantInventory;

    FarmManager fm;

    BoxCollider2D plantCollider;

    public float timer;

    void Start()
    {
        status = FindObjectOfType<PlantStatus>();
        plantInventory = FindObjectOfType<PlantInventory>();
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>(); 
        fm = transform.parent.GetComponent<FarmManager>();
        storePanel.SetActive(false);
        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
            timer -= Time.deltaTime;

        if(timer < 0)
        {
            plantInventory.SellFromInventory(selectedPlant.plantName, plantInventory.GetPlantQuantity(selectedPlant.plantName));
            timer = 5;
        }

        Debug.Log(plantStage);
        Debug.Log(status.isWater);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground 1"))
                {
                    Debug.Log("Tapped");


                    if (!fm.isPlanting)
                    {
                        if (plantStage == 0)
                        {
                            storePanel.SetActive(true);
                        }
                        
                    }
                     if (IsPlanted)
                    {
                        if (plantStage == selectedPlant.plantStages.Length - 1)
                        {
                            Harvest();
                        }
                    }
                     else if (fm.isPlanting)
                    {
                        Plant(fm.selectPlant.plant);
                    }
                }
/*_____________________________________________________________________________________________________________________________*/

                if (hit.collider.CompareTag("Ground 2"))
                {
                    Debug.Log("Tapped");


                    if (!fm.isPlanting)
                    {
                        if (plantStage == 0)
                        {
                            storePanel.SetActive(true);
                        }

                    }
                    if (IsPlanted)
                    {
                        if (plantStage == selectedPlant.plantStages.Length - 1)
                        {
                            Harvest();
                        }
                    }
                    else if (fm.isPlanting)
                    {
                        Plant(fm.selectPlant.plant);
                    }
                }
            }
        }
        if (IsPlanted == true)
        {

            if (plantStage <= selectedPlant.plantStages.Length - 1)
            {
                if (status.isWater == true)
                {
                    plantStage++;
                }

                if(plantStage >= selectedPlant.plantStages.Length)
                {
                    plantStage = 1;
                }
                UpdatePlant();
            }
        }
    }

    void Harvest()
    {
        Debug.Log("Harvested");
        IsPlanted = false;
        fm.isPlanting = false;
        plantStage = 0;
        status.isWater = false;
        plant.gameObject.SetActive(false);
        plantInventory.AddToInventory(selectedPlant.plantName);
    }

    void Plant(PlantObject newPlant)
    {
        Debug.Log("Planted");
        selectedPlant = newPlant;
        IsPlanted = true;  
        plantStage = 0;
        UpdatePlant();
        plant.gameObject.SetActive(true);
    }

    void UpdatePlant()
    {
        plant.sprite = selectedPlant.plantStages[plantStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.size.y / 2);
    }
}
