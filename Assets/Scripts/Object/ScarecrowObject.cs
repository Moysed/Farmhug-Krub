using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowObject : MonoBehaviour
{
    public GameObject[] scarecrow;
    int price = 200;
    public float MaxTime;
    float lifeTime1;
    float lifeTime2;
    float lifeTime3;
    float lifeTime4;

    void Start()
    {
        lifeTime1 = MaxTime;
        lifeTime2 = MaxTime;
        lifeTime3 = MaxTime;
        lifeTime4 = MaxTime;
    }
    void Update()
    {
        if(scarecrow[0].activeSelf)
        {
            lifeTime1 -= Time.deltaTime;
            if(lifeTime1 <= 0)
            {
                scarecrow[0].active = false;
                lifeTime1 = MaxTime;
            }
        }
        if(scarecrow[1].activeSelf)
        {
            lifeTime2 -= Time.deltaTime;
            if(lifeTime2 <= 0)
            {
                scarecrow[1].active = false;
                lifeTime2 = MaxTime;
            }
        }
        if(scarecrow[2].activeSelf)
        {
            lifeTime3 -= Time.deltaTime;
            if(lifeTime3 <= 0)
            {
                scarecrow[2].active = false;
                lifeTime3 = MaxTime;
            }
        }
        if(scarecrow[3].activeSelf)
        {
            lifeTime4 -= Time.deltaTime;
            if(lifeTime4 <= 0)
            {
                scarecrow[3].active = false;
                lifeTime4 = MaxTime;
            }
        }
    }
    public void placeScarecrow1()
    {
        if(Inventory.singleton.coin >= price)
        {
            scarecrow[0].active = true;
            Inventory.singleton.coin -= price;
        }
    }

    public void placeScarecrow2()
    {
         if(Inventory.singleton.coin >= price)
        {
            scarecrow[1].active = true;
            Inventory.singleton.coin -= price;
        }
    }

    public void placeScarecrow3()
    {
         if(Inventory.singleton.coin >= price)
        {
            scarecrow[2].active = true;
            Inventory.singleton.coin -= price;
        }
    }

    public void placeScarecrow4()
    {
         if(Inventory.singleton.coin >= price)
        {
            scarecrow[3].active = true;
            Inventory.singleton.coin -= price;
        }
    }
}
