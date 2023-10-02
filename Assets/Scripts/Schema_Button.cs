using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Schema_Button : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Air30 device;
    public Schema schema;
    public bool state  = false;
    private Image button_image;
    
    void Start()
    {
        button_image = gameObject.GetComponent<Image>();
        button_image.color = Color.red;
    }

    // Update is called once per frame
    
    public void Change_State()
    {
        if (state)
        {
            state = false;
            button_image.color = Color.red;
            switch (gameObject.tag)
            {
                case "power_btn":
                    device.Change_power_mode();
                    break;
                case "connect_btn":
                    schema.Connect_button_Handle();
                    break;
            }
           
        }
        else
        {
            state = true;
            button_image.color = Color.green;
            switch (gameObject.tag)
            {
                case "power_btn":
                    device.Change_power_mode();
                    break;
                case "connect_btn":
                    schema.Connect_button_Handle();
                    break;
            }
        }
    }
}
