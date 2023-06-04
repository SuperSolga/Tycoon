using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "CoffeeData", menuName = "Tycoon/Coffee")]

public class CoffeeData : ScriptableObject
{
    public float price;
    public int maxDosette;
    public GameObject model;

    public float size;

    public float conveyorSpeed = 1f;
    
    [HideInInspector]
    public bool isPresent = false;
    [HideInInspector]
    public GameManager gameManager;
    public List<GameObject> list;
    public bool[] tested = new bool[4];

    public float YSize;


    public void CoffeeSpawn(MachineData machine, Transform transform, int j, GameObject model)
    {
        for (int i = 0; i < machine.numberCoffee; i++)
        {
            //Vector3 a = transform.Find("MachineSupport").transform.position + new Vector3(machine.coffeePositions[i][0], YSize, machine.coffeePositions[i][2]);
            Instantiate(model, transform.Find("MachineSupport").transform.position
                + new Vector3(machine.coffeePositions[i][0], YSize, machine.coffeePositions[i][2]), Quaternion.Euler(0, 0, 0), transform.Find("MachineSupport"));
        }
        tested[j] = false;
    }
    public void Move(Transform trans, GameObject coffeeeCup, float speed)
    {
        trans.Translate(Vector3.left * Time.deltaTime * speed);

        if (trans.transform.position.x < -10)
            {
                Destroy(coffeeeCup);
                gameManager.AddMoney(price);
         }
     }
}
