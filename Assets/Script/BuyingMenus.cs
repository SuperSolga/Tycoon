using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingMenus : MonoBehaviour
{
    [Header("Canvas")]
    #region Capsule Buy menu
    public Canvas capsuleMarket;
    #endregion
    
    #region Machine Buy menu
    public Canvas machineMarket;
    #endregion

    [Header("Button")]
    #region Choose Market
    public Button capsuleButton;
    public Button machineButton;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        capsuleMarket.enabled= true;
        machineMarket.enabled= false;
        capsuleButton.interactable = false;
        machineButton.interactable = true;
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
            capsuleButton.interactable = false;
            machineButton.interactable = true;
        }
        else if (type == "machine") 
        {
            capsuleMarket.enabled= false;
            machineMarket.enabled= true;
            capsuleButton.interactable = true;
            machineButton.interactable = false;
        }
    }
}
