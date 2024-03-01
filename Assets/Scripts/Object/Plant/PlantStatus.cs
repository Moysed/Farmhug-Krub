using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlantStatus : BaseStatus
{
    public int _spacePrice;
    public GameObject StatusPrefab;
    public Vector3 statusPos;
    public int waterTime;
    public  bool isWater = false;
    GroundMangement gm;
    public SpriteRenderer plant;
    float destroyTime = 0;
    
    [SerializeField]
     BoxCollider2D plantCollider;
    public float afterWatertime = 0;

    public bool _isBought = false;
 
    
 
    public enum InstanceMode
    {
        Instance,
        Pool
    }
 
    public InstanceMode instanceMode = InstanceMode.Pool;
 
    void Start()
    {
        gm = GroundMangement.singleton;
        waterTime = 0; // Adjust the initial grow time as needed
    }
 
    void Update()
    {
        /*if(GroundMangement.singleton.fm.isPlanting == true && isLock == false)
        {
            Inventory.singleton.coin -= _spacePrice;
        }*/

        //water Time
        if(IsPlanted && waterTime >= 0 && waterTime <= 600 )
        {
            if(afterWatertime <= 0)
            {
                waterTime++;
            }

        }
        if (IsPlanted == true)
        {
            afterWatertime -= Time.deltaTime;
        }
        if(gm.selectedPlant != null)
            if (ObjectStage == gm.selectedPlant.ObjectStages.Length - 1)
            {
                waterTime = 0;
            }
 
        if (waterTime == 600)
        {
                Debug.Log("Watering");
                ShowStatus();
        }
        else if(waterTime > 600)
        {
            waterTime = 601;
        }
 
        //Status Touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
 
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
 
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("PlantStatus'"))
                {
                    Plant plant = hit.collider.GetComponent<Plant>();
                    if (plant._ownerPlantObjectPrefabs.name.Contains(this.name))
                    {
                       
                        //Debug.Log("Tapped on Object:" + this.name);
                        //Debug.Log("Is Water : " + isWater);
                        afterWatertime = 5;
                        //isWater = true;
                        isWater = true;
                        SFXPlaying.singleton.PlayWatering();
 
                        Lean.Pool.LeanPool.Despawn(hit.collider.gameObject);
 
                        //plantInventory.AddToInventory(hit.collider.gameObject.name);
                        waterTime = 0; // Reset grow time
                    } 
                }
            }
        }
 
        //Stage Update
        if (IsPlanted == true)
        {
            if ( afterWatertime <= 0 && isWater == true)
            {
                ObjectStage++; 

                if (ObjectStage >= gm.selectedPlant.ObjectStages.Length)
                {
                    ObjectStage = 1;
                }
                UpdatePlant();
            }
        }

        if (gm.inventory.autoSell.sellTime == 0)
        {
            if(_selfObjectInfo != null)
            gm.inventory.SellFromInventory(_selfObjectInfo.ObjectName, gm.inventory.GetPlantQuantity(_selfObjectInfo.ObjectName));
        }
    }
    void ShowStatus()
    {
        Plant newPlant = InstantiateObject(StatusPrefab).GetComponent<Plant>();
        newPlant.tag = "PlantStatus'";
        
        // Set the new plant's position
        newPlant.transform.position = statusPos;
        newPlant._ownerPlantObjectPrefabs = this;
        // Reset grow time for the new plant
        //waterTime = 10f;
    }
 
    // Single Game Object
    public override void UpdateInfo(InfoObject newPlant)
    {
        //Debug.Log("Yo");
        IsPlanted = true;
        _selfObjectInfo = newPlant;
        //Debug.Log("Planted");
        gm.selectedPlant = newPlant;
        ObjectStage = 0;
        ObjectName = newPlant.name;
        
        UpdatePlant();
        plant.gameObject.SetActive(true);
    }
 
    public override void Collected()
    {
        Debug.Log("Harvested");
        IsPlanted = false;
        gm.fm.isPlanting = false;
        ObjectStage = 0;
        isWater = false;
        plant.gameObject.SetActive(false);
        gm.inventory.AddToInventory(_selfObjectInfo.ObjectName);
    }
 
    // Single Game object
    void UpdatePlant()
    {
        if (ObjectStage >= _selfObjectInfo.ObjectStages.Length)
            ObjectStage = _selfObjectInfo.ObjectStages.Length;
 
        plant.sprite = _selfObjectInfo.ObjectStages[ObjectStage];
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.size.y / 2);
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

    public override void CheckIsLocked(int spacePrice)
    {
        _spacePrice = spacePrice;

        if(Inventory.singleton.coin < _spacePrice)
        {
            
            isLock = true;
            IsBought(isLock);
        }
        else if(Inventory.singleton.coin >= _spacePrice)
        {
            //Inventory.singleton.coin -= _spacePrice;
            isLock = false;
            
            IsBought(isLock);
        }

        /*if(isLock == true)
        {
            Inventory.singleton.coin -= 0;
        }
        else if(isLock == false)
        {
            Inventory.singleton.coin -= _spacePrice;
        }*/
    }

    public override void IsBought(bool b)
    {
        if (!b && _isBought == false)
        {
            Inventory.singleton.coin -= _spacePrice;
            _isBought = true;
        }
        else if (_isBought)
        {
            Debug.Log("Already bought");
        }
        else
        {
            Debug.Log("Not enough coin");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            //destroyTime += Time.deltaTime;
            //if(destroyTime >= 5)
            //{
                plant.gameObject.SetActive(false);
            //}
        }
    }
}
