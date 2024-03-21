using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalStatus : BaseStatus
{
    public GameObject bar;
    SFXManager sfx;
    public GameObject FloatingTextPrefab;
    public SpriteRenderer sign;
    public GameObject StatusPrefab;
    //public Vector3 statusPos;
    PetManagement pm;
    public int _spacePrice;
    public SpriteRenderer animal;
    FloatingBar progressionbar;

    [SerializeField]
    BoxCollider2D animalCollider;

    public enum InstanceMode
    {
        Instance,
        Pool
    }

    public InstanceMode instanceMode = InstanceMode.Pool;

    void Awake()
    {
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<SFXManager>();
    }

    void Start()
    {
        bar.gameObject.SetActive(false);

        pm = PetManagement.singleton;
        feedTime = 0; // Adjust the initial grow time as needed
        
    }

    void Update()
    {
        progressionbar = GetComponentInChildren<FloatingBar>();

        if (progressionbar != null)
        {
            if (animal.gameObject.active == false)
            {
                progressionbar.slider.value = 0;
            }
        }

        if (IsPeted == true)
        {
            afterFeedtime -= Time.deltaTime;
            animalAnimTimer -= Time.deltaTime;
            if (feedTime >= 0 && feedTime <= 1200)
            {
                if (afterFeedtime <= 0)
                {
                    feedTime++;
                }
            }

            if(isfeed == true)
            {
                bar.SetActive(true);

                if(progressionbar != null)
                {
                    progressionbar.slider.maxValue = _selfObjectInfo._growthTime;
                }

                if (afterFeedtime <= 0)
                {
                    if (animalAnimTimer <= 0)
                    {
                        if (ObjectStage >= _selfObjectInfo.ObjectStages.Length)
                        {
                            isSelected = false;
                            ObjectStage = _selfObjectInfo.ObjectStages.Length;
                        }

                        if (ObjectStage < _selfObjectInfo.ObjectStages.Length)
                        {
                            isSelected = false;
                            ObjectStage++;
                            UpdateAnimal();
                        }

                        if (ObjectStage > _selfObjectInfo.ObjectStages.Length - 2)
                        {
                            collectCheck = true;
                        }
                        afterFeedtime = 5;

                        Debug.Log(collectCheck);
                        Debug.Log(ObjectStage);
                        if (progressionbar != null)
                            progressionbar.slider.value += _selfObjectInfo.timeBtwstage;
                        animalAnimTimer = _selfObjectInfo.timeBtwstage;
                    }
                }
            }
        }

        if(ObjectStage == 2)
        {
            if (progressionbar != null)
                progressionbar.slider.value = 0;
            bar.SetActive(false);
        }

        if (pm.selectedAnimal != null)
        {
            if (ObjectStage == pm.selectedAnimal.ObjectStages.Length - 1)
            {
                feedTime = 0;
            }
        }

        if (feedTime == 1200 && !isfeed && isfeed == false)
        {
            Debug.Log("Feeding");
            ShowStatus();
        }
        else if (feedTime > 1200)
        {
            feedTime = 1201;
        }

        if (pm.inventory.autoSell.sellTime == 0)
        {
            OnSell();
        }
    }

    void ShowStatus()
    {
        StatusPrefab.SetActive(true);
    }

    // Single Game object
    void UpdateAnimal()
    {
        animal.sprite = _selfObjectInfo.ObjectStages[ObjectStage];
        if (ObjectStage >= _selfObjectInfo.ObjectStages.Length)
        {
            ObjectStage = _selfObjectInfo.ObjectStages.Length;
        }

        animalCollider.offset = new Vector2(0, animal.size.y / 10);
    }

    GameObject InstantiateObject(GameObject obj)
    {
        if (instanceMode == InstanceMode.Instance)
        {
            return Instantiate(obj);
        }
        else if (instanceMode == InstanceMode.Pool)
        {
            return Lean.Pool.LeanPool.Spawn(obj);
        }

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

    public override void OnSell()
    {
        Debug.Log(pm.inventory);
        if (_selfObjectInfo != null)
        {
            pm.inventory.SellFromInventory(_selfObjectInfo.ObjectName, pm.inventory.GetPlantQuantity(_selfObjectInfo.ObjectName));
        }
    }

    public override void Collected()
    {
        if (collectCheck)
        {
            Debug.Log("Harvested");
            IsPeted = false;
            pm.cm.isPeting = false;
            ObjectStage = 0;
            isfeed = false;
            animal.gameObject.SetActive(false);
            pm.inventory.AddToInventory(_selfObjectInfo.ObjectName);
            
            sfx.PlaySFX(sfx.Mandrake);
        }
    }

    public override void CheckIsLocked(int spacePrice)
    {
        _spacePrice = spacePrice;

        if (Inventory.singleton.coin < _spacePrice)
        {
            isLock = true;
            IsBought(isLock);
        }
        else if (Inventory.singleton.coin >= _spacePrice)
        {
            isLock = false;
            IsBought(isLock);
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
                ShowFloatingText(" - " + _spacePrice);
            }
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
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = text;
    }

    public override void isWatering()
    {
        afterFeedtime = 5;
        isfeed = true;
        sfx.PlaySFX(sfx.Watering);
        feedTime = 0; // Reset grow time
        StatusPrefab.SetActive(false);
    }
}