using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using UnityEngine;
using static CoffeeCup;

public class Machine : MonoBehaviour
{
    public GameObject coffeeCup;
    public Transform selfTransform;

    public bool machineOn = false;

    public MachineData machine;

    public float startTimer = 1.0f;
    public float timer = 10.0f;

    void Start()
    {
        if (machine != null && machineOn)
        {
            machine.Spawn();
            InvokeRepeating("CoffeeSpawn", startTimer, machine.timePerCoffee);
        } else
        {
            Debug.Log("no machine set");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CoffeeSpawn()
    {
        Instantiate(coffeeCup, selfTransform.Find("MachineSupport").transform);
    }
}
