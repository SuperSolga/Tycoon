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

    public int coffeeLvl;

    public Canvas uiBase;
    public Canvas market;

    private int timer;
    public int goaledTimer;

    #endregion;

    #region Other Variable
    [Header("Coffee Management")]
    public CoffeeData[] coffeeTypeStock;
    [Header("Machine Management")]
    public MachineData[] machineType;
    [HideInInspector]
    public int[] coffeeCommand;

    [Header("Money Management")]
    public Text[] Textmoney;
    public Text TextCapsule;
    #endregion

    private void Start()
    {
        uiBase.enabled= true;
        market.enabled= true;
        uiBase.gameObject.SetActive(true);
        market.gameObject.SetActive(false);

        InvokeRepeating("TimerUpdate", 0, 1);
    }

    private void Update()
    {
        for (int i = 0;i < Textmoney.Length; i++)
        {
            Textmoney[i].text = "Money : " + money;
        }
        TextCapsule.text = "Capsule Stock : " + stock;
    }

    public void AddMoney(float amount)
    {
        if (money < -amount)
        {
            Debug.Log("not enough money");
        } else
        {
            money += amount;
        }
    }

    public void BuyCapsule(int capsule)
    {
        stock += capsule;
        float price = Mathf.Round(0.0001f * Mathf.Pow(capsule, 3) - 0.0089f * Mathf.Pow(capsule, 2) + 1.0283f * capsule - 0.0195f -0.1f);
        AddMoney(-price);
    }

    public void OpenMarket()
    {
        uiBase.gameObject.SetActive(false);
        market.gameObject.SetActive(true);
    }

    public void CloseMarket()
    {
        uiBase.gameObject.SetActive(true);
        market.gameObject.SetActive(false);
    }

    public void UpgradeCoffee(int lvl)
    {
        coffeeLvl = lvl;
    }

    public int Search(CoffeeData coffeeType)
    {
        int a = 0;
        for (int i = 0; i < coffeeTypeStock.Length; i++)
        {
            if (coffeeTypeStock[i] != coffeeType)
            {
                a++;
            } else
            {
                return a;
            }
        }
        return 45;
    }

    void TimerUpdate()
    {
        timer += 1;
        if (timer == goaledTimer)
        {
            print("c'est bon");
            AddMoney(100);
            timer = 0;
        }
    }
}
