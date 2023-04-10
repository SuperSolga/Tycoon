using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MachineData", menuName ="Tycoon/Machine")]
public class MachineData : ScriptableObject
{
    public string machineName;
    public int level;
    public Material color;
    public float timePerCoffee;
    public int numberCoffee;
}
