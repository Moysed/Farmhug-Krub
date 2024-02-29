using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : MonoBehaviour
{
    //All Objects
    [SerializeField] protected InfoObject _selfObjectInfo;
    public int ObjectStage = 0;
    public bool collectCheck = false;
    public string ObjectName;
    public bool isLock = true;

    //Plant
    public bool IsPlanted = false;


    //Animal
    public bool IsPeted = false;

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

    public virtual void CheckIsLocked(int spacePrice)
    {
        
    }

    public virtual void IsBought(bool buy)
    {

    }
}
