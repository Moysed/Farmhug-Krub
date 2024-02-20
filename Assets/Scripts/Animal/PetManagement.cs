using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PetManagement : MonoBehaviour
{
    public static PetManagement singleton;

    public GameObject storePanel;
    

    AnimalStatus[] status;

    public AnimalInventory animalInventory;

    public CoopManager cm;

    public AnimalObject selectedAnimal;

    AnimalStatus _tempAnimalStatus;

    public float timer;

     void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        status = this.GetComponentsInChildren<AnimalStatus>();
        animalInventory = FindObjectOfType<AnimalInventory>();
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
                if (hit.collider.CompareTag("Animal"))
                {
                    //Debug.Log("Tapped");
                    _tempAnimalStatus = hit.collider.GetComponent<AnimalStatus>();

                    if (!cm.isPeting)
                    {
                        
                        if (_tempAnimalStatus.animalStage == 0)
                        {
                            storePanel.SetActive(true);
                        }
                    }
                    /*else if (cm.isPeting)
                    {
                        //_tempPlantStatus.Plant(fm.selectPlant.plant);
                        //storePanel.SetActive(false);
                    }*/

                     if (_tempAnimalStatus.IsPeted)
                        {
                            //_tempAnimalStatus.timer -= Time.deltaTime;
                            //storePanel.SetActive(false);
                            if (_tempAnimalStatus.animalStage == selectedAnimal.animalStages.Length - 1)
                            {
                                _tempAnimalStatus.Harvest();
                            }                                
                        }
                    }
                }
            }
        }   
     public void tempAnimal()
    {
        Debug.Log(_tempAnimalStatus);
        _tempAnimalStatus.Animal(cm.selectAnimal.animal);
        _tempAnimalStatus = null;
    }
}