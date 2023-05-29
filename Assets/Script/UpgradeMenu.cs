using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{

    private Text upText;
    private Text properties;
    private Button upgradeButton;

    private Text speed;
    private Text numberCoffee;

    private Text upgradeButtonText;

    // Start is called before the first frame update
    void Start()
    {
        upText = transform.GetChild(1).GetComponent<Text>();
        properties = transform.GetChild(2).GetComponent<Text>();
        upgradeButton = transform.GetChild(3).GetComponent<Button>();

        speed = properties.transform.GetChild(0).GetComponent<Text>();
        numberCoffee = properties.transform.GetChild(1).GetComponent<Text>();
        
        upgradeButtonText = upgradeButton.transform.GetChild(0).GetComponent<Text>();

    }
    public void GetSelected(int machineIndex, float Speed, int NumberCoffee, float upgradePrice)
    {
        upText.text = "Upgrade menu \n of \n Machine n°" + (machineIndex + 1).ToString();
        speed.text = Speed.ToString();
        numberCoffee.text = NumberCoffee.ToString();

        if (upgradePrice > 0)
        {
            upgradeButtonText.text = "UPGRADE : " + upgradePrice.ToString();
        } else
        {
            upgradeButtonText.text = "UPGRADE : Last upgrade";
        }
    }
}
