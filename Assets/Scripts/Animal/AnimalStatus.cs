using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalStatus : MonoBehaviour
{
    public GameObject StatusPrefab;
    public Vector3 statusPos;
    public int feedTime;
    public  bool isfeed = false;
    PetManagement pm;
   
    public bool IsPeted = false;
    public SpriteRenderer animal;
    public int animalStage = 0;
    public string animalName;
 
    [SerializeField]
    BoxCollider2D animalCollider;
    public float timer = 30;
    public float afterFeedtime = 0;
 
    [SerializeField]
    AnimalObject _selfAnimalObjectInfo;
 
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

        timer -= Time.deltaTime;
        if(IsPeted && feedTime >= 0 && feedTime <= 600 )
        {
            if(afterFeedtime <= 0)
            feedTime++;
        }
        if (IsPeted == true)
        {
            afterFeedtime -= Time.deltaTime;
        }
        if(pm.selectedAnimal != null)
            if (animalStage == pm.selectedAnimal.animalStages.Length - 1)
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

                    Debug.Log(animal);
                    if (animal != null)
                    {
                        if (animal._ownerAnimalObjectPrefabs.name.Contains(this.name))
                        {
                            Debug.Log("Tapped on Object:" + this.name);
                            afterFeedtime = 5;
                            
                            isfeed = true;
                            Debug.Log("Is Feed : " + isfeed);

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
             
                        animalStage++;
             

 
                if (animalStage >= pm.selectedAnimal.animalStages.Length)
                {
                    animalStage = 1;
                }
                UpdateAnimal();
            }
        }
 
       
        if (timer < 0)
        {
            pm.animalInventory.SellFromInventory(animalName, pm.animalInventory.GetPlantQuantity(animalName));
            timer = 30;
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
    public void Animal(AnimalObject newAnimal)
    {
        _selfAnimalObjectInfo = newAnimal;
        pm.selectedAnimal = newAnimal;
        IsPeted = true;
        animalStage = 0;
        animalName = newAnimal.name;
        UpdateAnimal();
        animal.gameObject.SetActive(true);
    }
 
    public void Harvest()
    {
        Debug.Log("Harvested");
        IsPeted = false;
        pm.cm.isPeting = false;
        animalStage = 0;
        isfeed = false;
        animal.gameObject.SetActive(false);
        pm.animalInventory.AddToInventory(pm.selectedAnimal.animalName);
    }
 
    // Single Game object
    void UpdateAnimal()
    {
      
            
        if (animalStage >= _selfAnimalObjectInfo.animalStages.Length)
            animalStage = _selfAnimalObjectInfo.animalStages.Length;
 
        animal.sprite = _selfAnimalObjectInfo.animalStages[animalStage];
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
}
