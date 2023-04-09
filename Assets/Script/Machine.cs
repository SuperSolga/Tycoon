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

    public float timer = 10.0f;

    void Start()
    {
        InvokeRepeating("CoffeeSpawn", timer, timer);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void CoffeeSpawn()
    {
        Instantiate(coffeeCup, selfTransform);
    }
}
