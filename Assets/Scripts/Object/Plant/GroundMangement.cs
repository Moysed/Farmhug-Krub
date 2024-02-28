using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using DG.Tweening;
using System.Security.Cryptography;
using System.Threading.Tasks;

public class GroundMangement : MonoBehaviour
{
    [SerializeField] RectTransform storePanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;

    public static GroundMangement singleton;

    public GameObject storePanel;
    
    PlantStatus[] status;

    public Inventory inventory;

    public FarmManager fm;

    public InfoObject selectedPlant;

    BaseStatus _tempPlantStatus;

    public float timer;

     void Awake()
     {
        singleton = this;
     }

    void Start()
    {
        status = this.GetComponentsInChildren<PlantStatus>();
        inventory = FindObjectOfType<Inventory>();
        //if(_tempPlantStatus.isLock == true)
        //{
            storePanel.SetActive(false);
          //  Debug.Log(_tempPlantStatus.isLock);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(_tempPlantStatus == null)
        {
            fm.isPlanting = false;
        }
    }

    public void Isplanted(BaseStatus _objBase)
    {
        _tempPlantStatus = (BaseStatus)_objBase;

        if (!fm.isPlanting && _tempPlantStatus.isLock == false)
        {
            if (_tempPlantStatus.ObjectStage == 0)
            {
                Debug.Log(_tempPlantStatus.isLock);
                storePanel.SetActive(true);
                storePanelIntro();
            }
        }

        if (_tempPlantStatus.IsPlanted)
        {
           closePanel();
            if (_tempPlantStatus.ObjectStage >= 1)
            {
                _tempPlantStatus.Collected();
            }
        }
    }

    public async void closePanel()
    {
       await  storePanelOuttro();
        storePanel.SetActive(false);
    }

    public void tempPlanting()
    {
        Debug.Log(fm.selectPlant.plant);
        _tempPlantStatus.UpdateInfo(fm.selectPlant.plant);
        _tempPlantStatus = null;
    }

    void storePanelIntro()
    {
        storePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
    }

    async Task storePanelOuttro()
    {
        await storePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}