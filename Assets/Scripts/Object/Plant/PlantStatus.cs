using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlantStatus : BaseStatus
{
    //public int countUnlockGround = 0;
    public GameObject FloatingTextPrefab;
    EnemiesFollowing enemy;
    float plantAnimTimer;
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
 
    //public GameObject landPanel;
    
 
    public enum InstanceMode
    {
        Instance,
        Pool
    }
 
    public InstanceMode instanceMode = InstanceMode.Pool;

    void Start()
    {
        gm = GroundMangement.singleton;
        enemy = GetComponent<EnemiesFollowing>();
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
 
        if (waterTime == 600 && plant.gameObject.activeSelf)
        {
                Debug.Log("Watering");
                ShowStatus();
        }
        else if(waterTime > 600)
        {
            waterTime = 601;
        }
 
        if(plant.gameObject.activeSelf == false && IsPlanted == true)
        {
            IsPlanted = false;
            destroyStatus();
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
            plantAnimTimer -= Time.deltaTime;
            if ( afterWatertime <= 0 && isWater == true)
            {
                ObjectStage++;

                if (ObjectStage >= gm.selectedPlant.ObjectStages.Length)
                {
                    
                    ObjectStage = gm.selectedPlant.ObjectStages.Length - 1;
                    collectCheck = true;
                }
               
                if (plantAnimTimer <= 0)
                {
                    afterWatertime = 5;
                    UpdatePlant();
                    plantAnimTimer = _selfObjectInfo.timeBtwstage;
                }

                
            }
        }

        if (gm.inventory.autoSell.sellTime == 0)
        {
            if(_selfObjectInfo != null)
            gm.inventory.SellFromInventory(_selfObjectInfo.ObjectName, gm.inventory.GetPlantQuantity(_selfObjectInfo.ObjectName));
        }
    }
    Plant _selfPlantStatus;
    void ShowStatus()
    {
        _selfPlantStatus = InstantiateObject(StatusPrefab).GetComponent<Plant>();
        _selfPlantStatus.tag = "PlantStatus'";

        // Set the new plant's position
        _selfPlantStatus.transform.position = statusPos;
        _selfPlantStatus._ownerPlantObjectPrefabs = this;
        // Reset grow time for the new plant
        //waterTime = 10f;
    }

    void destroyStatus()
    {
        Lean.Pool.LeanPool.Despawn(_selfPlantStatus.gameObject);
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
       // if (instanceMode == InstanceMode.Instance)
       //     return Instantiate(obj);
       // else if (instanceMode == InstanceMode.Pool)
            return Lean.Pool.LeanPool.Spawn(obj);
 
       // return null;
    }

    public override void CallUpdate()
    {
        //waterCheck
        //base.CallUpdate();
    }

    public override void CheckIsLocked(int spacePrice)
    {
        _spacePrice = spacePrice;

        if(Inventory.singleton.coin < _spacePrice && !_isBought)
        {
            isLock = true;
            IsBought(isLock);
        }
        else if(Inventory.singleton.coin >= _spacePrice)
        {
            
            isLock = false;
            IsBought(isLock);

            
        }
        

        if(_isBought)
        {
            isLock = false;
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
            //GroundMangement.singleton.landPanel.gameObject.SetActive(true);

            Inventory.singleton.coin -= _spacePrice;
            _isBought = true;
            if (FloatingTextPrefab)
            {
                ShowFloatingText(" - "+_spacePrice);
            }
            GroundMangement.singleton.countUnlockGround += 1;
            Debug.Log("Count Unlock Ground : " + GroundMangement.singleton.countUnlockGround);
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

    void ShowFloatingText(string text)
    {
        
        var go = Instantiate(FloatingTextPrefab , transform.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = text;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            enemy = collision.GetComponent<EnemiesFollowing>();
            if(enemy.hp > 0)
            {
                if(enemy.plant.active = false)
                {
                    enemy.plant = default;
                }
                //Destroy(plant.gameObject, 1);
                Invoke("destroyPlantFromEnemy", 1);
            }
        }
    }

    void destroyPlantFromEnemy()
    {
        /*if(StatusPrefab.gameObject.active == true)
        {
            StatusPrefab.gameObject.SetActive(false);
        }*/
        plant.gameObject.SetActive(false);
        //Lean.Pool.LeanPool.Despawn(StatusPrefab.gameObject);
    }

    /*public void BuyLand()
    {
        Inventory.singleton.coin -= _spacePrice;
            _isBought = true;
            if (FloatingTextPrefab)
            {
                ShowFloatingText(" - "+_spacePrice);
            }
            GroundMangement.singleton.countUnlockGround += 1;
            Debug.Log("Count Unlock Ground : " + GroundMangement.singleton.countUnlockGround);
    }*/
}
