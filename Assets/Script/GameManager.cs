using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region GameGlobalVariables

    public int stock;
    public float money = 0f;

    #endregion;

    public Text Textmoney;
    public Text TextCapsule;

    private void Update()
    {
        Textmoney.text = "MONEY : " + money;
        TextCapsule.text = "Capsule Stock : " + stock;
    }

    public void AddMoney(float amount)
    {
        money += amount;
    }

    public void BuyCapsule(int capsule)
    {
        stock += capsule;
        float price = (Mathf.Pow((float)0.0001 * capsule, 3) - Mathf.Pow((float)0.8997 * capsule, 2) + (float)1.0283 * capsule - 0.0195f);
        Debug.Log(price);
        AddMoney(price);
    }
}
