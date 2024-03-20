using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalStatus : BaseStatus
{
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
    PolygonCollider2D animalCollider;

    //[SerializeField]
    //InfoObject _selfAnimalObjectInfo;

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
        pm = PetManagement.singleton;
        feedTime = 0; // Adjust the initial grow time as needed
        progressionbar = GetComponentInChildren<FloatingBar>();
    }

    void Update()
    {
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

            if (afterFeedtime <= 0 && isfeed == true)
            {
                if (ObjectStage == _selfObjectInfo.ObjectStages.Length - 1)
                {
                    collectCheck = true;
                }

                if (animalAnimTimer <= 0)
                {
                    if (ObjectStage < _selfObjectInfo.ObjectStages.Length - 1)
                    {
                        ObjectStage++;
                    }

                    UpdateAnimal();
                    progressionbar.slider.value += _selfObjectInfo.timeBtwstage;
                    animalAnimTimer = _selfObjectInfo.timeBtwstage;
                }
            }
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

        animalCollider.offset = new Vector2(0, animal.size.y / 5);
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

        progressionbar.slider.maxValue = _selfObjectInfo._growthTime;

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

            progressionbar.slider.value = 0;

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

