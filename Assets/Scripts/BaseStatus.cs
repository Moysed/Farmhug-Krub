using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : MonoBehaviour
{
    public bool IsPlanted = false;

    public int plantStage = 0;
    public string plantName;
    public virtual void CallUpdate()
    {

    }

    public virtual void Collected()
    {

    }

    public virtual void UpdateInfo(InfoObject info)
    {

    }
}
