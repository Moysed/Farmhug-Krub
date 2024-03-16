using System.Collections;
using System.Collections.Generic;
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
    public Vector3 statusPos;
    public int feedTime;
    public  bool isfeed = false;
    public bool isSell = false;
    PetManagement pm;
    float animalAnimTimer;
    public int _spacePrice;
    
    public bool _isBought = false;
    public SpriteRenderer animal;
    FloatingBar progressionbar;
 
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
//CheckIsLocked(_spacePrice);

        /*if(PetManagement.singleton.cm.isPeting == true && isLock == false)
        {
            Inventory.singleton.coin -= _spacePrice;
        }*/

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
 
        if (feedTime == 600 && !isfeed)
        {
                Debug.Log("Feeding");
                ShowStatus();
        }
        else if(feedTime > 600)
        {
            feedTime = 601;
        }

        //Status Touch
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
                            sfx.PlaySFX(sfx.Watering);

                            Lean.Pool.LeanPool.Despawn(hit.collider.gameObject);

                            feedTime = 0; // Reset grow time
                        }
                    }
                }
            }
        }
 
        if (IsPeted == true)
        {
            //pm.storePanel.SetActive(true);
            animalAnimTimer -= Time.deltaTime;
            if ( afterFeedtime <= 0 && isfeed == true )  
            {   
                
               
                if(ObjectStage == _selfObjectInfo.ObjectStages.Length - 1)
                {
                    collectCheck = true;
                }
               
               

                if (animalAnimTimer <= 0)
                {
                    if (ObjectStage < _selfObjectInfo.ObjectStages.Length -1)
                        ObjectStage++;
                    UpdateAnimal();
                    
                    progressionbar.slider.value += _selfObjectInfo.timeBtwstage;

                    animalAnimTimer = _selfObjectInfo.timeBtwstage;
                }

               
            }
        }
       
        if(pm.inventory.autoSell.sellTime == 0)
        {
            OnSell();    
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
  
    // Single Game object
    void UpdateAnimal()
    {
        animal.sprite = _selfObjectInfo.ObjectStages[ObjectStage];
        if (ObjectStage >= _selfObjectInfo.ObjectStages.Length)
            ObjectStage = _selfObjectInfo.ObjectStages.Length;
 
      
        
        animalCollider.offset = new Vector2(0, animal.size.y / 5);
        
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

        progressionbar.slider.maxValue = _selfObjectInfo._growthTime;

        UpdateAnimal();
        animal.gameObject.SetActive(true);
    }

    public override void OnSell()
    {
            Debug.Log(pm.inventory);
        if(_selfObjectInfo != null)
        {
            pm.inventory.SellFromInventory(_selfObjectInfo.ObjectName, pm.inventory.GetPlantQuantity(_selfObjectInfo.ObjectName));
            isSell = false;
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
        else
        {

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
            //Inventory.singleton.coin -= _spacePrice;
            isLock = false;
            IsBought(isLock);
        }
    }
        public override void IsBought(bool b)
    {
        if (!b && _isBought == false)
        {
            Inventory.singleton.coin -= _spacePrice;
            _isBought = true;

            sign.gameObject.SetActive(false);
            if (FloatingTextPrefab)
            {
                ShowFloatingText(" - " + _spacePrice);
            }
            //gm.seedPanel.SetActive(true);


            sfx.PlaySFX(sfx.BuyGround);
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

        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
        go.GetComponent<TextMeshPro>().text = text;
    }
}

