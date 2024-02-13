using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour
{
    public PlantItem selectPlant;
    public bool isPlanting = false;

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
