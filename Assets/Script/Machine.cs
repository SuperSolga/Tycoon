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

    public MachineData[] machine;
    public int machineLvl;

    GameObject model;
    #endregion

    #region Coffee related variables
    private CoffeeCup coffeeCup;
    public CoffeeData coffeeData;
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

    #region Menu related variables
    public Canvas upgrade;
    [HideInInspector]
    public UpgradeMenu upgradeMenu;
    #endregion

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        coffeeData.gameManager = gameManager;
        upgrade.enabled = false;
        if (machine != null && machineOn)
        {
            SetMachine();
            coffeeCup = transform.GetChild(numberHierarchy).GetChild(0).GetComponent<CoffeeCup>();
            coffeeCup.coffee = coffeeData;
            dosette = coffeeData.maxDosette;
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
            Debug.Log("en spawn !");
            SetMachine();
            upgradeMenu = upgrade.GetComponent<UpgradeMenu>();
            select = GameObject.FindGameObjectWithTag("Manager").GetComponent<Selection>();
            present = true;
            deleted = false;
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
        InvokeRepeating("CoffeeSpawn", startTimer, machine[machineLvl].timePerCoffee);
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

    void CoffeeSpawn()
    {
        if (dosette > 0)
        {
            coffeeCup.coffee.CoffeeSpawn(machine[machineLvl], transform, coffeeCup.index, model);
            dosette--;
        } else
        {
            Debug.Log("not enough coffee in the machine");
        }
    }

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
                        upgradeMenu.GetSelected(machineIndex, machine[machineLvl].timePerCoffee, machine[machineLvl].numberCoffee, machine[machineLvl + 1].upgradePrice);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        upgradeMenu.GetSelected(machineIndex, machine[machineLvl].timePerCoffee, machine[machineLvl].numberCoffee, 0);
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

    public void SellMachine()
    {
        DeleteMachine();
        present = false;
        deleted = true;
        machineOn = false;
        for (int i = 0; i <= machineLvl; i++)
        {
            gameManager.money += reSellFee * (machine[i].upgradePrice);
        }
        machineLvl = 0;
    }

    public void Refill()
    {
        dosette = coffeeData.maxDosette;
    }

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
}
