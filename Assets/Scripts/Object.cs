using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public AnimalStatus _ownerAnimalObjectPrefabs;
    public PlantStatus _ownerPlantObjectPrefabs;
    Inventory inventory;

    public enum InstanceMode
    {
        Instance,
        Pool
    }

    public InstanceMode instanceMode = InstanceMode.Pool;
}
