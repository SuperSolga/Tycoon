using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="MachineData", menuName ="Tycoon/Machine")]
public class MachineData : ScriptableObject
{

    public string machineName;
    public int level;
    public Material color;
    public GameObject model;
    public float timePerCoffee;
    public int numberCoffee;
    public Vector3 position;

    public Vector3[] coffeePositions;


    public void Spawn(int indexMachine)
    {
        Instantiate(model, GameObject.FindGameObjectsWithTag("Support")[indexMachine].transform.position + position, Quaternion.Euler(0, 0, 0), GameObject.FindGameObjectsWithTag("Support")[indexMachine].transform);
    }
}
