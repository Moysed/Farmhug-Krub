using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Animal", menuName = "Animal")]
public class AnimalObject : ScriptableObject
{
    public string animalName;
    public Sprite[] animalStages;
    public float timeBtwstage = 2f;
    public int price;
    public int sellPrice;
}
