using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ItemData", menuName = "Tycoon/Item")]

public class ItemData : ScriptableObject
{
    public float price;
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


    public void Spawn(MachineData machine, Transform transform, int j, GameObject model)
    {
        for (int i = 0; i < machine.numberItem; i++)
        {
            Instantiate(model, transform.Find("MachineSupport").transform.position
                + new Vector3(machine.itemPositions[i][0], YSize, machine.itemPositions[i][2]), Quaternion.Euler(0, 0, 0), transform.Find("MachineSupport"));
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
