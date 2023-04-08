using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (transform.position.x < -5)
        {
            transform.position += new Vector3(transform.position.x + 0.2f, 0, 0);
        }
    }
}
