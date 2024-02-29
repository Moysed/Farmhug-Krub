using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{
    //public static SellManager singleton;
    CoinManagement CoinManagement;
    Inventory inventory;

    public float sellTime = 0;

    // Update is called once per frame
    public void AutoSell()
    {
        sellTime += Time.deltaTime;
        if (sellTime >= 30)
        {
            sellTime = 0;
        }
    }
}
