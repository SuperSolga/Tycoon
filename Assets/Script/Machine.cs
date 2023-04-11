using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public GameObject coffeeCup;
    public Transform selfTransform;
    public Vector3 coffeePosition;

    public bool machineOn = false;

    public MachineData machine;

    public float startTimer = 1.0f;
    public float timer = 10.0f;

    public int machineIndex;

    void Start()
    {
        if (machine != null && machineOn)
        {
            machine.Spawn(machineIndex);
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
        Instantiate(coffeeCup, selfTransform.Find("MachineSupport").transform.position + coffeePosition, Quaternion.Euler(0, 0, 0), selfTransform.Find("MachineSupport"));
    }
}
