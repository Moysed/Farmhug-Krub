using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : MonoBehaviour
{
        
    public bool IsPeted = false;
    public bool IsPlanted = false;

    public int ObjectStage = 0;

    public bool collectCheck = false;
    //public int animalStage = 0;

    public string ObjectName;
    //public string plantName;


    [SerializeField]
    protected InfoObject _selfObjectInfo;

    public virtual void CallUpdate()
    {

    }

    public virtual void Collected()
    {

    }

    public virtual void UpdateInfo(InfoObject info)
    {

    }

    public virtual void OnSell() { }

    public virtual void CheckISLocked(bool Check)
    {

    }

    
}
