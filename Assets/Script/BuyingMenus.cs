using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingMenus : MonoBehaviour
{
    [Header("Canvas")]
    #region Canvas Buy menu
    public Canvas capsuleMarket;
    public Canvas machineMarket;
    public Canvas coffeeMarket;
    #endregion

    [Header("Button")]
    #region Choose Market
    public Button capsuleButton;
    public Button machineButton;
    public Button coffeeButton;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SwitchMenu("capsule");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SwitchMenu(string type)
    {
        if (type == "capsule")
        {
            capsuleMarket.enabled= true;
            machineMarket.enabled= false;
            coffeeMarket.enabled= false;
            capsuleButton.interactable = false;
            machineButton.interactable = true;
            coffeeButton.interactable = true;
        }
        else if (type == "machine") 
        {
            capsuleMarket.enabled= false;
            machineMarket.enabled= true;
            coffeeMarket.enabled= false;
            capsuleButton.interactable = true;
            machineButton.interactable = false;
            coffeeButton.interactable = true;
        }
        else if (type == "coffee")
        {
            capsuleMarket.enabled= false;
            machineMarket.enabled= false;
            coffeeMarket.enabled= true;
            capsuleButton.interactable = true;
            machineButton.interactable = true;
            coffeeButton.interactable = false;
        }
    }
}
