using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManagement : MonoBehaviour
{
    public Text coin;
    public static CoinManagement singleton;
    //Start is called before the first frame update 
    void Start()
    {
        //Make this active and only instance 
        singleton = this;
    }
    public void UpdateCoin(int _coin)
    { 
        coin.text = _coin.ToString();
    }
}
