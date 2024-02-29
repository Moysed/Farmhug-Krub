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

    int[] sellPrice;

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
        autoSell = GetComponent<SellManager>();
        sellPrice = new int[5];
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
                if(productType == "Mandrake")
                {
                    inventory[productType] -= amount;
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 5;
                    totalIncome += 5;
                    
                }
                if (productType == "Cow")
                {
                    inventory[productType] -= amount;
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 2;
                    totalIncome += 2;
                    
                }
                if (productType == "Pig")
                {
                    inventory[productType] -= amount;
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 8;
                    totalIncome += 8;
                  
                }
                if (productType == "Chicken")
                {
                    inventory[productType] -= amount;
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 2;
                    totalIncome += 2;
                    
                }
                if (productType == "Berry")
                {
                    inventory[productType] -= amount;
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 4;
                    totalIncome += 4;

                }
                if (productType == "Corn")
                {
                    inventory[productType] -= amount;
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 2;
                    totalIncome += 2;

                }

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



                Debug.Log("Total income: " + totalIncome + ", Total coin: " + coin);
                totalIncome = 0;

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
