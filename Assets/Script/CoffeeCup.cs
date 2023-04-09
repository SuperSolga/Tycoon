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
    public void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.left * 1f * Time.deltaTime);
        if (transform.position.x < -10){
            Destroy(gameObject);
        }
    }
}
