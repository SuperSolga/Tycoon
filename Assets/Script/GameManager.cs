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

    public Canvas market;

    #endregion;

    public Text Textmoney;
    public Text TextCapsule;

    private void Start()
    {
        market.enabled = false;
    }

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
        float price = Mathf.Round(0.0001f * Mathf.Pow(capsule, 3) - 0.0089f * Mathf.Pow(capsule, 2) + 1.0283f * capsule - 0.0195f -0.1f);
        Debug.Log(price);
        AddMoney(-price);
    }

    public void OpenMarket()
    {
        market.enabled = true;
    }

    public void CloseMarket()
    {
        market.enabled = false;
    }
}
