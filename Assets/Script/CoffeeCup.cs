using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Machine;

public class CoffeeCup : MonoBehaviour
{
    public bool isPresent = true;

    // Update is called once per frame
    public void Update()
    {
        Move();
    }

    public void Move()
    {
        ;
        transform.Translate(Vector3.left * 1f * Time.deltaTime);
        if (transform.position.x < -10){
            Destroy(gameObject);
            isPresent = false;
        }
    }
}