using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    
    public PlantStatus _ownerPlantObjectPrefabs;
    public Vector3 plantPos;
    float growTime;
    PlantInventory plantInventory;

    public enum InstanceMode
    {
        Instance,
        Pool
    }

    public InstanceMode instanceMode = InstanceMode.Pool;

 
}
