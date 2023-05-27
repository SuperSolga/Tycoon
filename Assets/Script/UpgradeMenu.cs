using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{

    private Text upText;
    private Text properties;

    private Text speed;
    private Text numberCoffee;

    // Start is called before the first frame update
    void Start()
    {
        upText = transform.GetChild(1).GetComponent<Text>();
        properties = transform.GetChild(2).GetComponent<Text>();

        speed = properties.transform.GetChild(0).GetComponent<Text>();
        numberCoffee = properties.transform.GetChild(1).GetComponent<Text>();
    }
    public void GetSelected(int machineIndex, float Speed, int NumberCoffee)
    {
        upText.text = "Upgrade menu \n of \n Machine n°" + (machineIndex + 1).ToString();
        speed.text = Speed.ToString();
        numberCoffee.text = NumberCoffee.ToString();
    }
}
