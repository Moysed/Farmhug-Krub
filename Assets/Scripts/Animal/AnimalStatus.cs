using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalStatus : BaseStatus
{
    public GameObject StatusPrefab;
    public Vector3 statusPos;
    PetManagement pm;
   public SpriteRenderer animal;
 
    [SerializeField]
    BoxCollider2D animalCollider;
 
    [SerializeField]
    InfoObject _selfAnimalObjectInfo;
    Object Animal;
 
    public enum InstanceMode
    {
        Instance,
        Pool
    }
 
    public InstanceMode instanceMode = InstanceMode.Pool;
 
    void Start()
    {
        pm = PetManagement.singleton;
        feedTime = 0; // Adjust the initial grow time as needed
    }
 
    void Update()
    {
        if(IsPeted && feedTime >= 0 && feedTime <= 600 )
        {
            if(afterFeedtime <= 0)
            {
                feedTime++;
            }

        }
        if (IsPeted == true)
        {
            afterFeedtime -= Time.deltaTime;
        }
        if(pm.selectedAnimal != null)
            if (animalStage == pm.selectedAnimal.ObjectStages.Length - 1)
            {
                feedTime = 0;
            }
 
        if (feedTime == 600)
        {
                Debug.Log("Feeding");
                ShowStatus();
        }
        else if(feedTime > 600)
        {
            feedTime = 601;
        }

        if (IsPeted == true)
        {
            if ( afterFeedtime <= 0 && isfeed == true )  
            {
                        animalStage++;
                if (animalStage >= pm.selectedAnimal.ObjectStages.Length)
                {
                    animalStage = 1;
                }
                UpdateAnimal();
            }
        }

       if (pm.inventory.autoSell.sellTime <= 0)
        {
            pm.inventory.SellFromInventory(animalName, pm.inventory.GetPlantQuantity(animalName));
        }
       
    }
 
    void ShowStatus()
    {
        Object newAnimal = InstantiateObject(StatusPrefab).GetComponent<Object>();
        newAnimal.tag = "Status";
 
        // Set the new animal's position
        newAnimal.transform.position = statusPos;
        newAnimal._ownerAnimalObjectPrefabs = this;
    }
 
    public override void Collected()
    {
        Debug.Log("Harvested");
        IsPeted = false;
        pm.cm.isPeting = false;
        animalStage = 0;
        isfeed = false;
        animal.gameObject.SetActive(false);
        pm.inventory.AddToInventory(_selfAnimalObjectInfo.ObjectName);
    }
 
    // Single Game object
    void UpdateAnimal()
    {     
        if (animalStage >= _selfAnimalObjectInfo.ObjectStages.Length)
            animalStage = _selfAnimalObjectInfo.ObjectStages.Length;
 
        animal.sprite = _selfAnimalObjectInfo.ObjectStages[animalStage];
        animalCollider.size = animal.bounds.size;
        animalCollider.offset = new Vector2(0, animal.size.y / 2);
        
    }
 
    GameObject InstantiateObject(GameObject obj)
    {
        if (instanceMode == InstanceMode.Instance)
            return Instantiate(obj);
        else if (instanceMode == InstanceMode.Pool)
            return Lean.Pool.LeanPool.Spawn(obj);
 
        return null;
    }

    public override void CallUpdate()
    {
        //waterCheck
        //base.CallUpdate();
    }

    // Single Game Object
    public override void UpdateInfo(InfoObject newAnimal)
    {
        _selfAnimalObjectInfo = newAnimal;
        pm.selectedAnimal = newAnimal;
        IsPeted = true;
        animalStage = 0;
        animalName = newAnimal.name;
        UpdateAnimal();
        animal.gameObject.SetActive(true);
    }

    public override void statusTap(Object animal)
    {
        Animal = animal;

            //Debug.Log(animal);
            if (animal != null)
            {
                if (animal._ownerAnimalObjectPrefabs.name.Contains(this.name))
                {
                    //Debug.Log("Tapped on Object:" + this.name);
                    afterFeedtime = 5;
                            
                    isfeed = true;
                    //Debug.Log("Is Feed : " + isfeed);

                    Lean.Pool.LeanPool.Despawn(animal);

                    feedTime = 0; // Reset grow time
                }
            }
        }
    }
