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
    private bool tested = false;
    private bool deleted = false;
    public int index;

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
            coffeeCup = GameObject.FindGameObjectWithTag("Machine").GetComponent<CoffeeCup>();
            coffeeCup.index = index;
            tested = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (machine != null && machineOn && !tested)
        {
            SetMachine();
            tested = true;
            deleted = false;
        }
        if (!machineOn && tested && !deleted) 
        { 
            DeleteMachine();
            deleted = true;
            tested = false;
        }

    }

    void SetMachine()
    {
        machine.Spawn(machineIndex);
        InvokeRepeating("CoffeeSpawn", startTimer, machine.timePerCoffee);
    }

    void DeleteMachine()
    {
        Destroy(transform.GetChild(2).GetChild(0).gameObject);
        CancelInvoke();
        Debug.Log("deleted");
    }

    void CoffeeSpawn()
    {
        coffeeCup.coffee.CoffeeSpawn(machine,selfTransform, coffeeCup.index);
    }
}
