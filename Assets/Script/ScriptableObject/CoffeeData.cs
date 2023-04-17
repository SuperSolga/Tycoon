using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CoffeeData", menuName = "Tycoon/Coffee")]

public class CoffeeData : ScriptableObject
{
    public float price;
    public GameObject model;
    
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
    public void Move()
    {
        int i = 0;
        Debug.Log(list[0]);
        Debug.Log(list.Count);
        foreach(GameObject movable in list)
        {
            movable.transform.Translate(Vector3.left * Time.deltaTime);
            Debug.Log(movable.transform.position.x);

            if (movable.transform.position.x < -10)
            {
                Debug.Log("coffee destroyed");
                list.RemoveAt(i);
                Destroy(movable);
                money.AddMoney(price);
            }
            i++;
        }
    }
}
