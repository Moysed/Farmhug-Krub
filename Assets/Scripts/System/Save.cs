using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public Inventory Player;

    // Start is called before the first frame update
    void Start()
    {
        int _coin = PlayerPrefs.GetInt("Coin");

        Player.coin = _coin;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("Coin", Player.coin);
    }
}
