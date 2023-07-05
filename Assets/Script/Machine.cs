using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{
    #region Machine related variables
    public bool machineOn = false;
    private bool present = false;
    private bool deleted = false;

    [HideInInspector]
    public MachineData[] coffeeMachine, waffleMachine, iceMachine;
    [HideInInspector]
    public MachineData[] machine;
    public int machineLvl;

    GameObject model;
    #endregion

    #region Item related variables
    private CoffeeCup coffeeCup;
    public ItemData coffeeData;
    private int dosette;
    #endregion

    private Selection select;

    public float startTimer = 1.0f;
    public float timer = 10.0f;

    private int numberHierarchy = 0;

    #region Game related variables
    private GameManager gameManager;
    public float reSellFee;
    #endregion

    public int machineIndex;
    public string type;

    #region Menu related variables
    public Canvas upgrade;
    [HideInInspector]
    public UpgradeMenu upgradeMenu;
    #endregion

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        coffeeData.gameManager = gameManager;
        coffeeMachine = gameManager.coffeeMachineStock;

        if (type == "coffee")
        {
            machine = coffeeMachine;
        } else if (type == "waffle")
        {
            machine = waffleMachine;
        }

        upgrade.enabled = false;
        coffeeData = gameManager.coffeeTypeStock[gameManager.coffeeLvl];
        if (machine != null && machineOn)
        {
            SetMachine();
            coffeeCup = transform.GetChild(numberHierarchy).GetChild(0).GetComponent<CoffeeCup>();
            coffeeCup.coffee = coffeeData;
            present = true;

            model = coffeeData.model;

            upgradeMenu = upgrade.GetComponent<UpgradeMenu>();

            select = GameObject.FindGameObjectWithTag("Manager").GetComponent<Selection>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (model == null && coffeeData != null)
        {
            model = coffeeData.model;
        }

        if (machine != null && machineOn && !present)
        {
            SetMachine();
            upgradeMenu = upgrade.GetComponent<UpgradeMenu>();
            select = GameObject.FindGameObjectWithTag("Manager").GetComponent<Selection>();
            present = true;
            deleted = false;
            coffeeCup.coffee = coffeeData;
            transform.GetChild(numberHierarchy).GetChild(transform.GetChild(numberHierarchy).childCount - 1).SetSiblingIndex(0);

        }
        if (!machineOn && present && !deleted) 
        { 
            DeleteMachine();
            deleted = true;
            present = false;
        }
        if (machine != null && present)
        { 
            coffeeCup.index = machineIndex;
        }
    }

    void SetMachine()
    {
        machine[machineLvl].Spawn(machineIndex);
        InvokeRepeating("Spawn", startTimer, machine[machineLvl].timePerItem);
        coffeeCup = transform.GetChild(numberHierarchy).GetChild(0).GetComponent<CoffeeCup>();
    }

    void DeleteMachine()
    {
        for (int i = 0; i < transform.GetChild(numberHierarchy).childCount; i++)
        {
                Destroy(transform.GetChild(numberHierarchy).GetChild(i).gameObject);
                CancelInvoke();
        }
    }

    //Make a new item spawn, at the rate decided with "time per Item", and
    void Spawn()
    {
        if (dosette > 0)
        {
            coffeeCup.coffee.Spawn(machine[machineLvl], transform, coffeeCup.index, model);
            dosette--;
            upgradeMenu.slider.value = dosette;
        } else
        {
            Debug.Log("not enough coffee in the machine");
        }
    }

    //Try to upgrade the machine, check the money and the level and act as consequences of these checks
    public void UpgradeMachine()
    {
        try
        {
            if (machine[machineLvl + 1].upgradePrice <= gameManager.money)
            {

                DeleteMachine();
                machineLvl += 1;
                try
                {
                    SetMachine();
                    Debug.Log("upgraded");
                    try
                    {
                        upgradeMenu.GetSelected(machineIndex, machine[machineLvl].timePerItem, machine[machineLvl].numberItem, machine[machineLvl + 1].upgradePrice);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        upgradeMenu.GetSelected(machineIndex, machine[machineLvl].timePerItem, machine[machineLvl].numberItem, 0);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    machineLvl -= 1;
                    SetMachine();
                }
                gameManager.AddMoney(0 - machine[machineLvl].upgradePrice);
            }
            else
            {
                Debug.Log("not enough money");
            }
        }
        catch
        {
            Debug.Log("machine Lvl max");
        }
    }

    //Try to buy a new machine, if there is enough money, if not, return an error, if it can, a new machine spawn and money is deduced
    public void BuyMachine()
    {
        if(gameManager.money >= machine[0].upgradePrice)
        { 
            machineOn = true;
            gameManager.money  -= machine[0].upgradePrice;
        } else
        {
            Debug.Log("poor");
        }
    }

    //Sell the machine, delete it from the scene, and stop all coffee spawn
    public void SellMachine()
    {
        DeleteMachine();
        present = false;
        deleted = true;
        machineOn = false;
        for (int i = 0; i <= machineLvl; i++)
        {
            gameManager.money += reSellFee * machine[i].upgradePrice;
        }
        machineLvl = 0;
    }

    //Try to refill the machine at its maximum, if it can't, put the amount of dosette in the reserve and adapt the slider that represent the stock in the machine
    public void Refill()
    {
        if (gameManager.stock-machine[machineLvl].maxDosette - dosette >= 0)
        {
            gameManager.stock -= machine[machineLvl].maxDosette - dosette;
            dosette  += machine[machineLvl].maxDosette - dosette;
            upgradeMenu.slider.value = dosette;
        } else
        {
            dosette += gameManager.stock;
            gameManager.stock = 0;
        }
    }

    //Close the menu of machine upgrade
    public void CloseMenu()
    {
        upgrade.enabled= false;
    }

    //Select the new machine once it's upgrade, more simple to close the menu after that
    public void SelectNew()
    {
        select.highlight = transform.GetChild(0).GetChild(0);
        select.selection = transform.GetChild(0).GetChild(0);
    }

    //Try to change the type of coffee that the machine produces, return an error if this type is not unlocked
    public void ChangeCoffeeTypee(ItemData coffeeType)
    {
        if (gameManager.Search(coffeeType) <= gameManager.coffeeLvl)
        {
            Debug.Log("ok pour machine n°" + machineIndex);
            coffeeData = coffeeType;
        } else
        {
            Debug.Log("pas ok pour machine n°" + machineIndex);
        }
    }
}
