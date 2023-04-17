using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    public CoffeeData coffee;

    public int index;
    private int numberCoffee;

    void Start()
    {
        coffee.list.Clear();
        coffee.money = GameObject.FindGameObjectWithTag("Manager").GetComponent<Money>();
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
        Debug.Log(coffee.isPresent);
        if (coffee.isPresent)
        {
            Debug.Log(index);
            if (!coffee.tested[index])
            {
                for (int i = coffee.list.Count + 1; i <= numberCoffee; i++)
                {
                    Debug.Log(transform.parent.GetChild(i).gameObject);
                    coffee.list.Add(transform.parent.GetChild(i).gameObject);
                }
                coffee.tested[index] = true;
            }
            coffee.Move();
            Debug.Log(coffee.list.Count);
            Debug.Log(coffee.tested[index]);
        }
    }
}