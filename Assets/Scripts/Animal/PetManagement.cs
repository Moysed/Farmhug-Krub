using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PetManagement : MonoBehaviour
{
    public static PetManagement singleton;

    public GameObject storePanel;
    
    AnimalStatus[] status;

    public Inventory inventory;

    public CoopManager cm;

    public InfoObject selectedAnimal;

    BaseStatus _tempAnimalStatus;

    public float timer;

     void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        status = this.GetComponentsInChildren<AnimalStatus>();
        inventory = FindObjectOfType<Inventory>();
        storePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_tempAnimalStatus == null)
        {
            cm.isPeting = false;
        }
    }


        public void IsPeted(BaseStatus _objBase)
        {
            _tempAnimalStatus =(BaseStatus)_objBase;

                    if (!cm.isPeting)
                    {
                        
                        if (_tempAnimalStatus.animalStage <= 0)
                        {
                            storePanel.SetActive(true);
                        }
                    }

                     if (_tempAnimalStatus.IsPeted)
                        {
                            //_tempAnimalStatus.timer -= Time.deltaTime;
                            storePanel.SetActive(false);
                            if (_tempAnimalStatus.animalStage >= 1)
                            {
                                _tempAnimalStatus.Collected();
                            }                                
                        }
                    }

     public void tempAnimal()
    {
        _tempAnimalStatus.UpdateInfo(cm.selectAnimal.animal);
        _tempAnimalStatus = null;
    }
}