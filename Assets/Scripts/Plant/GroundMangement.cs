using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GroundMangement : MonoBehaviour
{
    public static GroundMangement singleton;

    public GameObject storePanel;
    

    PlantStatus[] status;

    public PlantInventory plantInventory;

    public FarmManager fm;

    public PlantObject selectedPlant;

    PlantStatus _tempPlantStatus;

    public float timer;

     void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        status = this.GetComponentsInChildren<PlantStatus>();
        plantInventory = FindObjectOfType<PlantInventory>();
        storePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    //Debug.Log("Tapped");
                    _tempPlantStatus = hit.collider.GetComponent<PlantStatus>();

                    if (!fm.isPlanting)
                    {
                        if (_tempPlantStatus.plantStage == 0)
                        {
                            storePanel.SetActive(true);
                        }
                    }

                     if (_tempPlantStatus.IsPlanted)
                        {
                            if (_tempPlantStatus.plantStage == selectedPlant.plantStages.Length - 1)
                            {
                                _tempPlantStatus.Harvest();
                            }
                        }
                    else if (fm.isPlanting)
                    {
                        //_tempPlantStatus.Plant(fm.selectPlant.plant);
                    }
                }
            }
        } 
    }

    public void tempPlanting()
    {
        _tempPlantStatus.Plant(fm.selectPlant.plant);
        _tempPlantStatus = null;
    }
}