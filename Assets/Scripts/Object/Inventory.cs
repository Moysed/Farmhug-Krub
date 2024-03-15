using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //private scarecrowInfo scarecrow;
    public static Inventory singleton;
    //public float sellTime;
    public Dictionary<string, int> inventory = new Dictionary<string, int>(); // Use a dictionary to store product types and quantities

    private CoinManagement coinManager;
    public SellManager autoSell;

    private CoopManager cm;
    private FarmManager fm;
    public int coin = 15;

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
        //coin = 15;
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
        if (inventory.ContainsKey(productType))
        {
            if (inventory[productType] >= amount)
            {
                if(productType == "Mandrake" && amount != 0 && inventory[productType] != 0)
                {
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 5 * inventory[productType];
                    totalIncome += 5 * inventory[productType];
                    inventory[productType] = 0;
                    amount = 0;
                }
                if (productType == "Cow" && amount != 0 && inventory[productType] != 0)
                {
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 0 * inventory[productType];
                    totalIncome += 2 * inventory[productType];
                    inventory[productType] = 0;
                    amount = 0;

                }
                if (productType == "Pig" && amount != 0 && inventory[productType] != 0)
                {
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 8 * inventory[productType];
                    totalIncome += 8 * inventory[productType];
                    inventory[productType] = 0;
                    amount = 0;
                }
                if (productType == "Chicken" && amount != 0 && inventory[productType] != 0)
                {
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 2 * inventory[productType];
                    totalIncome += 2 * inventory[productType];
                    inventory[productType] = 0;
                    amount = 0;

                }
                if (productType == "Carrot" && amount != 0 && inventory[productType] != 0)
                {
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 4 * inventory[productType];
                    totalIncome += 4 * inventory[productType];
                    inventory[productType] = 0;
                    amount = 0;

                }
                if (productType == "Corn" && amount != 0 && inventory[productType] != 0)
                {
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 2 * inventory[productType];
                    totalIncome += 2 * inventory[productType];
                    inventory[productType] = 0;
                    amount = 0;

                }
                if (productType == "Wheat" && amount != 0 && inventory[productType] != 0)
                {
                    Debug.Log("Sold " + amount + " " + productType + "(s) from inventory. Remaining: " + inventory[productType]);
                    coin += 4 * inventory[productType];
                    totalIncome += 4 * inventory[productType];
                    inventory[productType] = 0;
                    amount = 0;

                }

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

    /*public void buyScarecrow()
    {
        if(coin >= scarecrow.price)
        {
            coin -= scarecrow.price;
        }
        else if(coin < scarecrow.price)
        {
            Debug.Log("Not Enough Money");
        }
    }*/
}
