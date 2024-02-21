using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatus : MonoBehaviour
{
    //Animal
    public bool IsPeted = false;
    public int animalStage = 0;
    public string animalName;
    public int feedTime;
    public bool isfeed = false;
    public float afterFeedtime = 0;

    //Plant
    public int waterTime;
    public  bool isWater = false;
    public float afterWatertime = 0;
    public bool IsPlanted = false;
    public int plantStage = 0;
    public string plantName;


    public virtual void CallUpdate() { }

    public virtual void Collected() { }

    public virtual void UpdateInfo(InfoObject info) { }

    public virtual void statusTap(Object obj) { }
}
