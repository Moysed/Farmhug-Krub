using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePreferencesManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
    }

    void OnApplicationQuit()
    {
        SavePrefs();
    }

    void SavePrefs()
    {
        PlayerPrefs.SetInt("Coin", 15);
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
