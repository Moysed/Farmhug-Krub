using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    //public static SellManager singleton;
    CoinManagement CoinManagement;
    Inventory inventory;

    public float sellTime = 30;

    // Update is called once per frame
    public void AutoSell()
    {
        sellTime -= Time.deltaTime;
        if (sellTime <= -1)
        {
            sellTime = 30;
        }
    }
}
