using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Schema : MonoBehaviour
{
    // Start is called before the first frame update
    public Air30 device;
    public TMP_Text ampere_text_main_device;
    public TMP_Text ampere_text_conn_device;
    public Schema_Button connect_button;
    public void Change_Ampere(string ampere)
    {
        ampere_text_main_device.text = "I: " + ampere + " mA";
        if (connect_button.state == true)
        {
            ampere_text_conn_device.text = "I: " + ampere + " mA";
        }
    }
    public void Connect_button_Handle()
    {
        if (connect_button.state == false)
        {
            ampere_text_conn_device.text = "I: " + "0,000" + " mA";
        }
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
