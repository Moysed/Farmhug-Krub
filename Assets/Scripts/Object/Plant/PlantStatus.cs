using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlantStatus : BaseStatus
{
    public GameObject _player;
    SFXManager sfx;
 
    public SpriteRenderer sign;
    public SpriteRenderer wet;
    public GameObject FloatingTextPrefab;
    EnemiesFollowing enemy;
    public int _spacePrice;
    public GameObject StatusPrefab;
    //public Vector3 statusPos;
    GroundMangement gm;
    public SpriteRenderer plant;
 
    [SerializeField]
    BoxCollider2D plantCollider;
 
    FloatingBar progressionbar;


    Vector3 spawnplayerpos;
 
    public enum InstanceMode
    {
        Instance,
        Pool
    }
 
    void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>();
    }
 
    public InstanceMode instanceMode = InstanceMode.Pool;
 
    void Start()
    {
       spawnplayerpos = this.transform.position;
        progressionbar = GetComponentInChildren<FloatingBar>();
        gm = GroundMangement.singleton;
        enemy = GetComponent<EnemiesFollowing>();
        waterTime = 0; // Adjust the initial grow time as needed
        
    }
 
    void Update()
    {
        //water Time
        if(plant.gameObject.active == false)
        {
            progressionbar.slider.value = 0;
        }
 
        if(IsPlanted == false)
        {
            waterTime = 0;
        }

        if(IsPlanted == true)
        {
            afterWatertime -= Time.deltaTime;
            plantAnimTimer -= Time.deltaTime;
            if (waterTime >= 0 && waterTime <= 600)
            {
                if (afterWatertime <= 0)
                {
                    waterTime++;
                }
            }
 
            if (isWater == true)
            {
                wet.gameObject.SetActive(true);
                if(afterWatertime <= 0)
                {
                    if (ObjectStage == _selfObjectInfo.ObjectStages.Length - 1)
                    {
                        collectCheck = true;
                    }
 
                    if (plantAnimTimer <= 0)
                    {
                        if (ObjectStage < _selfObjectInfo.ObjectStages.Length - 1)
                        {
                            ObjectStage++;
                        }
 
                        afterWatertime = 5;
 
                        UpdatePlant();
                        progressionbar.slider.value += _selfObjectInfo.timeBtwstage;
                        plantAnimTimer = _selfObjectInfo.timeBtwstage;
                    }
                }
            }
 
            if (ObjectStage > 0)
            {
                wet.gameObject.SetActive(false);
            }
        }
 
        if(gm.selectedPlant != null)
        {
            if (ObjectStage == gm.selectedPlant.ObjectStages.Length - 1)
            {
                waterTime = 0;
            }
        }
 
        if (waterTime == 600 && plant.gameObject.activeSelf && isWater == false)
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
 
        if (gm.inventory.autoSell.sellTime == 0)
        {
            if(_selfObjectInfo != null)
            gm.inventory.SellFromInventory(_selfObjectInfo.ObjectName, gm.inventory.GetPlantQuantity(_selfObjectInfo.ObjectName));
        }
    }
    Plant _selfPlantStatus;
    void ShowStatus()
    {
        StatusPrefab.SetActive(true);
    }
 
    void destroyStatus()
    {
        StatusPrefab.SetActive(false);
    }
 
    // Single Game Object
    public override void UpdateInfo(InfoObject newPlant)
    {
        IsPlanted = true;
        _selfObjectInfo = newPlant;
        gm.selectedPlant = newPlant;
        ObjectStage = 0;
        ObjectName = newPlant.name;
        progressionbar.slider.maxValue = _selfObjectInfo._growthTime;
 
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
        gm.inventory.AddToInventory(_selfObjectInfo.ObjectName);
        progressionbar.slider.value = 0;
        sfx.PlaySFX(sfx.Harvest);
        _player.transform.localPosition = new Vector3(spawnplayerpos.x,spawnplayerpos.y + 1.7f);
        if (collectCheck)
        {
            if (PlayerScript.singleton.onAnimrun == true)
            {
                PlayerScript.singleton.ChangeAnimationstate("IsHarvestLeft", "Isidle");
            }
           
            Invoke("resetHavestAnim", 1.5f);
        }
    }
 
    void Harvest()
    {
        plant.gameObject.SetActive(false);
    }
 
    // Single Game object
    void UpdatePlant()
    {
        if (ObjectStage >= _selfObjectInfo.ObjectStages.Length)
        {
            ObjectStage = _selfObjectInfo.ObjectStages.Length;
        }
        
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
 
        if(Inventory.singleton.coin < _spacePrice && !isBought)
        {
            isLock = true;
            IsBought(isLock);
        }
        else if(Inventory.singleton.coin >= _spacePrice)
        {
            isLock = false;
            IsBought(isLock);
        }
 
        if(isBought == true)
        {
            isLock = false;
        }
    }
 
    public override void IsBought(bool b)
    {
        if (!b && isBought == false)
        {
            Inventory.singleton.coin -= _spacePrice;
            isBought = true;
 
            sign.gameObject.SetActive(false);
            if (FloatingTextPrefab)
            {
                ShowFloatingText(" - "+_spacePrice);
            }
            GroundMangement.singleton.countUnlockGround += 1;
            Debug.Log("Count Unlock Ground : " + GroundMangement.singleton.countUnlockGround);
            gm.seedPanel.SetActive(true);
            
            sfx.PlaySFX(sfx.BuyGround);
        }
        else if (isBought)
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
                    enemy.plant = default;
                    progressionbar.slider.value = 0;
                    IsPlanted = false;
                    gm.fm.isPlanting = false;
                    
                
                //Destroy(plant.gameObject, 1);
                destroyPlantFromEnemy();
            }
        }
    }
 
    void destroyPlantFromEnemy()
    {
        Debug.Log("Eated");
        IsPlanted = false;
        gm.fm.isPlanting = false;
        ObjectStage = 0;
        isWater = false;
        plant.gameObject.SetActive(false);
        progressionbar.slider.value = 0;
    }
 
    public override void isWatering()
    {
        afterWatertime = 5;
        isWater = true;
        sfx.PlaySFX(sfx.Watering);
        waterTime = 0; // Reset grow time
        StatusPrefab.SetActive(false);
        _player.transform.localPosition = new Vector3(spawnplayerpos.x,spawnplayerpos.y + 1.7f);
        
        if (PlayerScript.singleton.onAnimrun == true)
        {
            PlayerScript.singleton.ChangeAnimationstate("IsWateringLeft", "Isidle");
            Invoke("resetWateringAnim", 1.5f);
        }
    }
 
    void resetWateringAnim()
    {
        if (PlayerScript.singleton.onAnimrun == true)
        {
            PlayerScript.singleton.ChangeAnimationstate("Isidle", "IsWateringLeft");
            
            //PlayerScript.singleton.onLeft = false;
        }
        _player.transform.localPosition = PlayerScript.singleton.Spawnpos;
    }
 
    public void resetHavestAnim()
    {
        if (PlayerScript.singleton.onAnimrun == true)
        {
            PlayerScript.singleton.ChangeAnimationstate("Isidle", "IsHarvestLeft");
            //PlayerScript.singleton.onLeft = false;
        }
    
        plant.gameObject.SetActive(false);
        //PlayerScript.singleton.onAnimrun = false;
        //resetPlayerPos();
        _player.transform.localPosition = PlayerScript.singleton.Spawnpos;
    }

    /*void resetPlayerPos()
    {
        if(PlayerScript.singleton.onAnimrun == false)
        {
            _player.transform.localPosition = PlayerScript.singleton.Spawnpos;
            Debug.Log("on anim : " + PlayerScript.singleton.onAnimrun);
        }
    }*/
}