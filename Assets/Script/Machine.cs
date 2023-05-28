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
    public Transform selfTransform;

    public bool machineOn = false;
    private bool present = false;
    private bool deleted = false;


    public MachineData[] machine;
    public int machineLvl;

    private CoffeeCup coffeeCup;
    public CoffeeData coffeeData;
    private float coffeeYdrift;

    private Selection select;

    GameObject model;

    public float startTimer = 1.0f;
    public float timer = 10.0f;

    private int numberHierarchy = 0;

    public int machineIndex;

    public Canvas upgrade;
    [HideInInspector]
    public UpgradeMenu upgradeMenu;

    private float a;

    void Start()
    {
        upgrade.enabled = false;
        if (machine != null && machineOn)
        {
            SetMachine();
            coffeeCup = transform.GetChild(numberHierarchy).GetChild(0).GetComponent<CoffeeCup>();
            coffeeCup.coffee = coffeeData;
            present = true;
            Debug.Log("spawned");

            a = machine[0].position[0];
            Debug.Log(a);

            model = coffeeData.model;

            Debug.Log("instance prete" + machine[machineLvl].coffeePositions[0]);

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
        Debug.Log(machine[machineLvl]);
        coffeeCup.coffee.CoffeeSpawn(machine[machineLvl], transform, coffeeCup.index, model);
    }

    public void UpgradeMachine()
    {
        DeleteMachine();
        Debug.Log("deleted");
        machineLvl += 1;
        try
        {
            SetMachine();
            Debug.Log("upgraded");
        }
        catch (IndexOutOfRangeException)
        {
            Debug.Log("machine Lvl max");
            machineLvl -= 1;
            SetMachine();
        }
    }

    public void CloseMenu()
    {
        upgrade.enabled= false;
    }

    //Select the new machine once it's upgrade, more simple to close the menu after that
    public void SelectNew()
    {
        Debug.Log(transform);
        Debug.Log(select);
        select.highlight = transform.GetChild(0).GetChild(0);
        select.selection = transform.GetChild(0).GetChild(0);
    }
}
