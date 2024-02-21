using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class GroundMangement : MonoBehaviour
{
    public static GroundMangement singleton;

    public GameObject storePanel;
    
    PlantStatus[] status;

    public Inventory inventory;

    public FarmManager fm;

    public InfoObject selectedPlant;

    BaseStatus _tempPlantStatus;

    public float timer;

     void Awake()
    {
        singleton = this;
    }

    void Start()
   {
        status = this.GetComponentsInChildren<PlantStatus>();
        inventory = FindObjectOfType<Inventory>();
        storePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_tempPlantStatus == null)
        {
            fm.isPlanting = false;
        }
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
                            storePanel.SetActive(false);
                            if (_tempPlantStatus.plantStage >= 1)
                            {
                                _tempPlantStatus.Harvest();
                            }
                        }
                    else if (fm.isPlanting)
                    {
                        //_tempPlantStatus.Plant(fm.selectPlant.plant);
                    }
                
            }
        }*/
    }

    public void Isplanted(BaseStatus _objBase)
    {
        _tempPlantStatus = (BaseStatus)_objBase;

        if (!fm.isPlanting)
        {
            if (_tempPlantStatus.plantStage == 0)
            {
                storePanel.SetActive(true);
            }
        }

        if (_tempPlantStatus.IsPlanted)
        {
            storePanel.SetActive(false);
            if (_tempPlantStatus.plantStage >= 1)
            {
                _tempPlantStatus.Collected();
            }
        }
    }

    public void tempPlanting()
    {
        Debug.Log(fm.selectPlant.plant);
        _tempPlantStatus.UpdateInfo(fm.selectPlant.plant);
        _tempPlantStatus = null;
    }
}
