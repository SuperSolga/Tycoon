using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CoffeeData", menuName = "Tycoon/Coffee")]

public class CoffeeData : ScriptableObject
{
    public float price;
    public GameObject model;

    public float conveyorSpeed = 1f;
    
    [HideInInspector]
    public bool isPresent = false;
    [HideInInspector]
    public Money money;
    public List<GameObject> list;
    public bool[] tested = new bool[4];


    public void CoffeeSpawn(MachineData machine, Transform transform, int j)
    {
        for (int i = 0; i < machine.numberCoffee; i++)
        {
            Instantiate(model, transform.Find("MachineSupport").transform.position
                + machine.coffeePositions[i], Quaternion.Euler(0, 0, 0), transform.Find("MachineSupport"));
        }
        tested[j] = false;
    }
    public void Move(Transform trans, GameObject coffeeeCup, float speed)
    {
        trans.Translate(Vector3.left * Time.deltaTime * speed);

        if (trans.transform.position.x < -10)
            {
                Destroy(coffeeeCup);
                money.AddMoney(price);
         }
     }
}
