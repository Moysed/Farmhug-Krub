using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalStatus : BaseStatus
{
    public GameObject StatusPrefab;
    public Vector3 statusPos;
    public int feedTime;
    public  bool isfeed = false;
    PetManagement pm;
   
   
    public SpriteRenderer animal;
   
 
    [SerializeField]
    PolygonCollider2D animalCollider;
    public float afterFeedtime = 0;
 
    //[SerializeField]
    //InfoObject _selfAnimalObjectInfo;
 
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
            if (ObjectStage == pm.selectedAnimal.ObjectStages.Length - 1)
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


        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Status"))
                {
                    Animal animal = hit.collider.GetComponent<Animal>();

                    //Debug.Log(animal);
                    if (animal != null)
                    {
                        if (animal._ownerAnimalObjectPrefabs.name.Contains(this.name))
                        {
                            //Debug.Log("Tapped on Object:" + this.name);
                            afterFeedtime = 5;
                            
                            isfeed = true;
                            //Debug.Log("Is Feed : " + isfeed);

                            Lean.Pool.LeanPool.Despawn(hit.collider.gameObject);

                            feedTime = 0; // Reset grow time
                        }
                    }
                }
            }
        }
 
        if (IsPeted == true)
        {
            
         
            if ( afterFeedtime <= 0 && isfeed == true )  
            {
             
                        ObjectStage++;
             

 
                if (ObjectStage >= pm.selectedAnimal.ObjectStages.Length)
                {
                    ObjectStage = 1;
                }
                UpdateAnimal();
            }
        }

       if (pm.inventory.autoSell.sellTime <= 0)
        {
            //animalName
            pm.inventory.SellFromInventory(_selfObjectInfo.ObjectName, pm.inventory.GetPlantQuantity(_selfObjectInfo.ObjectName));
        }
       
    }
 
    void ShowStatus()
    {
        Animal newAnimal = InstantiateObject(StatusPrefab).GetComponent<Animal>();
        newAnimal.tag = "Status";
 
        // Set the new animal's position
        newAnimal.transform.position = statusPos;
        newAnimal._ownerAnimalObjectPrefabs = this;
    }
 
    // Single Game Object
    public void Animal(InfoObject newAnimal)
    {
        _selfObjectInfo = newAnimal;
        pm.selectedAnimal = newAnimal;
        IsPeted = true;
        ObjectStage = 0;
        ObjectName = newAnimal.name;
        UpdateAnimal();
        animal.gameObject.SetActive(true);
    }
 
    // Single Game object
    void UpdateAnimal()
    {     
        if (ObjectStage >= _selfObjectInfo.ObjectStages.Length)
            ObjectStage = _selfObjectInfo.ObjectStages.Length;
 
        animal.sprite = _selfObjectInfo.ObjectStages[ObjectStage];
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

    public override void UpdateInfo(InfoObject newAnimal)
    {
        _selfObjectInfo = newAnimal;
        pm.selectedAnimal = newAnimal;
        IsPeted = true;
        ObjectStage = 0;
        ObjectName = newAnimal.name;
        UpdateAnimal();
        animal.gameObject.SetActive(true);
    }

    public override void Collected()
    {
        Debug.Log("Harvested");
        IsPeted = false;
        pm.cm.isPeting = false;
        ObjectStage = 0;
        isfeed = false;
        animal.gameObject.SetActive(false);
        pm.inventory.AddToInventory(_selfObjectInfo.ObjectName);
    }
}
