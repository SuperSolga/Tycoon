using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{

    public float money = 0f;

    public void AddMoney(float amount)
    {
        money += amount;
        Debug.Log(money);
    }
}
