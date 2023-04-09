using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoffeeCup;

public class Machine : MonoBehaviour
{
    public GameObject coffeeCup;
    public Transform selfTransform;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newCup = Instantiate(coffeeCup, selfTransform, false);
        CoffeeCup script = newCup.GetComponent<CoffeeCup>();
        script.Update();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
