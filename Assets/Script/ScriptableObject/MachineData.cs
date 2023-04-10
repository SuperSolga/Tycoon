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

    public void Spawn()
    {
        Instantiate(model, GameObject.FindGameObjectWithTag("Support").transform.position + position, Quaternion.Euler(0, 0, 0), GameObject.FindGameObjectWithTag("Support").transform);
    }
}
