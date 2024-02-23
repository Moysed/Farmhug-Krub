using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory singleton;
    //public float sellTime;
    public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Use a dictionary to store product types and quantities

    private CoinManagement coinManager;
    public SellManager autoSell;

    private CoopManager cm;
    private FarmManager fm;
    public int coin;

    //private int[] amountSellPrice;

    int totalIncome;

    private void Awake()
    {
        singleton = this;
    }
    void Start()
    {
        cm = FindObjectOfType<CoopManager>();
        fm = FindObjectOfType<FarmManager>();
        autoSell = FindObjectOfType<SellManager>();
        
        coin = 15;
        //sellTime = 30;
    }

    void Update()
    {
        CoinManagement.singleton.UpdateCoin(coin);
        //sellTime -= Time.deltaTime;

        if (coin <= 0)
        {
            coin = 0;
        }

        autoSell.AutoSell();
    }

    // Add a plant to the inventory
    public void AddToInventory(string productType)
    {
        if (inventory.ContainsKey(productType))
        {
            inventory[productType]++;
        }
        else
        {
            inventory.Add(productType, 1);
        }

        Debug.Log("Added " + productType + " to inventory. Total: " + inventory[productType]);
    }

    // Sell a specific amount of a product type from the inventory
    public void SellFromInventory(string productType, int amount)
    {
        if (inventory.ContainsKey(productType) )
        {
            if (inventory[productType] >= amount)
            {
                //inventory[productType] -= amount;
                //Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);

                //for(int i = 0; i < 5; i++)
                /*if(productType == "Corn")
                {
                    if (inventory.ContainsKey(productType))
                    {
                        inventory[productType]++;
                    }
                    amountSellPrice[1] += inventory[productType] * amount;
                }
                else if(productType == "Berry")
                {
 
                }
                else if(productType == "Pig")
                {
                    
                }
                else if(productType == "Chicken")
                {
                    
                }
                else if(productType == "Cow")
                {
                    
                }
                totalIncome += cm.selectAnimal.animal.sellPrice * amount;
                coin += totalIncome;
            }
            else
            {
                Debug.Log("Not enough " + productType + " in inventory to sell.");
            }
        }
        else
        {
            Debug.Log("Plant type " + productType + " not found in inventory.");
        }*/

                if (inventory.ContainsKey(productType))
                {
                    if (inventory[productType] >= amount)
                    {
                        inventory[productType] -= amount;
                        Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);

                        int sellPrice = 0; // ราคารวมที่ขายได้จากสินค้าชนิดนี้
                        if (productType == "Corn")
                        {
                            // คำนวณราคารวมที่ขายได้จาก Corn
                            sellPrice += amount * cm.selectAnimal.animal.sellPrice;
                            Debug.Log(sellPrice);
                        }
                        else if (productType == "Berry")
                        {
                            // คำนวณราคารวมที่ขายได้จาก Berry
                            sellPrice += amount * cm.selectAnimal.animal.sellPrice;
                            Debug.Log(sellPrice);
                        }
                        else if (productType == "Pig")
                        {
                            // คำนวณราคารวมที่ขายได้จาก Pig
                            sellPrice += amount * cm.selectAnimal.animal.sellPrice;
                            Debug.Log(sellPrice);
                        }
                        else if (productType == "Chicken")
                        {
                            // คำนวณราคารวมที่ขายได้จาก Chicken
                            sellPrice += amount * cm.selectAnimal.animal.sellPrice;
                            Debug.Log(sellPrice);
                        }
                        else if (productType == "Cow")
                        {
                            // คำนวณราคารวมที่ขายได้จาก Cow
                            sellPrice += amount * cm.selectAnimal.animal.sellPrice;
                            Debug.Log(sellPrice);
                        }

                        totalIncome += sellPrice;
                        coin += sellPrice;

                        Debug.Log("Total income: " + totalIncome + ", Total coin: " + coin);
                    }
                    else
                    {
                        Debug.Log("Not enough " + productType + " in inventory to sell.");
                    }
                }
                else
                {
                    Debug.Log("Plant type " + productType + " not found in inventory.");
                }
            }
        }
    }

    // Get the quantity of a specific product type in the inventory
    public int GetPlantQuantity(string productType)
    {
        if (inventory.ContainsKey(productType))
        {
            return inventory[productType];
        }
        else
        {
            return 0;
        }
    }
}
