using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public Transform selfTransform;

    public bool machineOn = false;
    private bool present = false;
    private bool deleted = false;


    public MachineData machine;
    private CoffeeCup coffeeCup;

    public float startTimer = 1.0f;
    public float timer = 10.0f;

    public int machineIndex;

    void Start()
    { 
        if (machine != null && machineOn)
        {
            SetMachine();
            coffeeCup = transform.GetChild(2).GetChild(0).GetComponent<CoffeeCup>();
            present = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (machine != null && machineOn && !present)
        {
            SetMachine();
            present = true;
            deleted = false;
            transform.GetChild(2).GetChild(transform.GetChild(2).childCount - 1).SetSiblingIndex(0);
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
        machine.Spawn(machineIndex);
        InvokeRepeating("CoffeeSpawn", startTimer, machine.timePerCoffee);
        coffeeCup = transform.GetChild(2).GetChild(0).GetComponent<CoffeeCup>();
    }

    void DeleteMachine()
    {
        for (int i = 0; i < transform.GetChild(2).childCount; i++)
        {
            Destroy(transform.GetChild(2).GetChild(i).gameObject);
            CancelInvoke();
        }
    }

    void CoffeeSpawn()
    {
        coffeeCup.coffee.CoffeeSpawn(machine,selfTransform, coffeeCup.index);
    }
}
