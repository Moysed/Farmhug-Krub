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

    public BaseStatus _tempAnimalStatus;

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
        if (_tempAnimalStatus == null)
        {
            cm.isPeting = false;
        }
    }


    public void IsPeted(BaseStatus _objBase)
    {

        _tempAnimalStatus = (BaseStatus)_objBase;

        if(_tempAnimalStatus == null )
        {
            Debug.Log(_tempAnimalStatus);
            return;
        }

        Debug.Log(_tempAnimalStatus);
        if (!cm.isPeting)
        {
            if (_tempAnimalStatus.ObjectStage == 0)
            {
                storePanel.SetActive(true);
            }
        }

        if (_tempAnimalStatus.IsPlanted)
        {
            storePanel.SetActive(false);
            if (_tempAnimalStatus.ObjectStage >= 1)
            {
                _tempAnimalStatus.Collected();
                _tempAnimalStatus.collectCheck = true;
            }
        }
    }

    public void tempAnimal()
    {
        _tempAnimalStatus.UpdateInfo(cm.selectAnimal.animal);
        _tempAnimalStatus = null;
    }
}