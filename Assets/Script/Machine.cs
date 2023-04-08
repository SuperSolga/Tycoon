using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public GameObject coffeeCup;
    public Transform selfTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("Spawn", 1.0f, 1.0f);
    }

    //Spawn a coffee cup
    public void Spawn()
    {
        Instantiate(coffeeCup, selfTransform);
    }
}
