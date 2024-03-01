using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class PetManagement : MonoBehaviour
{

    [SerializeField] RectTransform storePanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;

    public static PetManagement singleton;

    public GameObject storePanel;

    AnimalStatus[] status;

    public Inventory inventory;

    public CoopManager cm;

    public InfoObject selectedAnimal;

    public BaseStatus _tempAnimalStatus;

    public float timer;
    

    void Awake()
    {
        singleton = this;
    }

    void Start()
    {
        status = this.GetComponentsInChildren<AnimalStatus>();
        inventory = FindObjectOfType<Inventory>();

        //if(_tempAnimalStatus.isLock == true)
        //{
            //Inventory.singleton.coin -= ;
            storePanel.SetActive(false);
        //    Debug.Log(_tempAnimalStatus.isLock);
        //}
        //storePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_tempAnimalStatus == null)
        {
            cm.isPeting = false;
        }
    }


    public void IsPeted(BaseStatus _objBase)
    {

        _tempAnimalStatus = (BaseStatus)_objBase;

        if(_tempAnimalStatus == null)
        {
            Debug.Log(_tempAnimalStatus);
            return;
        }

        Debug.Log(_tempAnimalStatus);
        if (!cm.isPeting && _tempAnimalStatus.isLock == false)
        {
            Debug.Log(_tempAnimalStatus.isLock);
            storePanel.SetActive(true);
            storePanelIntro();
        }

        /* if (!cm.isPeting && _tempAnimalStatus._isBought == true)
        {
            Debug.Log(_tempAnimalStatus._isBought);
            storePanel.SetActive(true);
            storePanelIntro();
        } */

        if (_tempAnimalStatus.IsPeted)
        {
            closePanel();
            if (_tempAnimalStatus.ObjectStage >= 1)
            {
                _tempAnimalStatus.Collected();
                _tempAnimalStatus.collectCheck = true;
            }
        }
    }
    public async void closePanel()
    {
        await storePanelOuttro();
        storePanel.SetActive(false);
    }

    public void tempAnimal()
    {
        _tempAnimalStatus.UpdateInfo(cm.selectAnimal.animal);
        _tempAnimalStatus = null;
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