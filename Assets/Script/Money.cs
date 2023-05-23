using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{

    public float money = 0f;
    public Text Textmoney;

    private void Update()
    {
        Textmoney.text = "MONEY : " + money;
    }

    public void AddMoney(float amount)
    {
        money += amount;
        Debug.Log(money);
    }
}
