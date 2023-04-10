using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeCup : MonoBehaviour
{
    public bool isPresent = true;
    Money money;
    void Start()
    {
        money = GameObject.FindGameObjectWithTag("Manager").GetComponent<Money>();
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.left * 1f * Time.deltaTime);
        if (transform.position.x < -10){
            Destroy(gameObject);
            isPresent = false;
            money.AddMoney(1.5f);
        }
    }
}