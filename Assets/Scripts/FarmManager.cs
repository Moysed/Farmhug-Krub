using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPlant(PlantItem newPlant)
    {
        /*if (selectPlant == newPlant)
        {
            selectPlant = null;
            isPlanting = false;
        }*/
        {
            selectPlant = newPlant;
            isPlanting = true;
        }
    }
}
