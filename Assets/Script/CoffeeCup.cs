using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    [HideInInspector]
    public CoffeeData coffee;

    public int index;
    private int numberCoffee;

    public float conveyorSpeed = 1.0f;

    void Start()
    {
        Debug.Log(coffee);
        Debug.Log(coffee.YSize);
        coffee.list.Clear();
        coffee.isPresent = false;

        for (int i = 0;i<4;i++)
        {
            coffee.tested[i] = false;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        numberCoffee = transform.parent.childCount - 1;
        if (numberCoffee <= 0)
        {
            coffee.isPresent = false;
        }
        else
        {
            coffee.isPresent = true;
        }
        if (coffee.isPresent)
        {
            for (int i = 1; i <= numberCoffee; i++)
            {
                    coffee.Move(transform.parent.GetChild(i).transform, transform.parent.GetChild(i).gameObject, conveyorSpeed);
            }
        }
    }
}