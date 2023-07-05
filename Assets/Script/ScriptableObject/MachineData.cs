using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="MachineData", menuName ="Tycoon/Machine")]
public class MachineData : ScriptableObject
{

    public string machineName;
    public int level;
    public GameObject model;
    public float timePerItem;
    public int numberItem;
    public Vector3 position;

    public int maxDosette;

    public float upgradePrice;

    public Vector3[] itemPositions;

    public void Spawn(int indexMachine)
    {
        Instantiate(model, GameObject.FindGameObjectsWithTag("Support")[indexMachine].transform.position + position, Quaternion.Euler(0, 0, 0), GameObject.FindGameObjectsWithTag("Support")[indexMachine].transform);
    }
}
