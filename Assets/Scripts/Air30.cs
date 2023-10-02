using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using TMPro;





public class Air30 : MonoBehaviour
{
    // Start is called before the first frame update
    public Handle_Cap cap_front;
    public GameObject screen_text_object;
    public GameObject screen_unit_text_object;
    private float current_pressure= 0.0f;   //давление в kPa
    public float delta_pressure = 0f;
    private float input_pressure= 0.0f;
    private bool power_status = false;
    Menu_Handle Menu_Handler;
    
    public Schema schema;

    Menu_Params Parameters= new Menu_Params();
    private float pressure_update_timer = 0f;
    
    
    void Start()
    {
        Menu_Handler = new Menu_Handle(Parameters);
        screen_text_object.SetActive(false);
        screen_unit_text_object.SetActive(false);
        Parameters.screen_text = screen_text_object.GetComponent<TextMeshPro>();
        Parameters.screen_unit_text = screen_unit_text_object.GetComponent<TextMeshPro>();
        Parameters.screen_unit_text.text = Parameters.unit_variants[Parameters.current_unit];
    }

    // Update is called once per frame
    void Update()
    {
        if (power_status)
        {
            pressure_measuring();
        }
        if (Parameters.current_screen_state == (int)screen_states.main_menu)
        {
            Parameters.menu_timer += Time.deltaTime;
            if (Parameters.menu_timer > 5.0f)
            {
                Parameters.menu_timer = 0f;
                Menu_Handler.Quit_Menu();
            }
        }
    }
    private void pressure_measuring()
    {
        if (pressure_update_timer < 1f)
        {
            pressure_update_timer += Time.deltaTime;
            return;
        }
        else
        {
            pressure_update_timer = 0f;
        }

        float innacuracy = Random.Range(-0.03f,0.03f);
        float value = 0f;
        
        if (Parameters.current_screen_state == (int)screen_states.pressure_showing)
        {
            
            switch (Parameters.current_unit)
            {
                case 0:
                    value = current_pressure * 0.001f;
                    break;
                case 1:
                    value = current_pressure;
                    break;
                case 2:
                    value = current_pressure * 1000f;
                    break;
                case 3:
                    value = current_pressure * 101.97162f;
                    break;
                case 4:
                    value = current_pressure * 0.010197162f;
                    break;
                case 5:
                    value = current_pressure * 7.5006156f;
                    break;
            }
            value += innacuracy;
            //value = value < 0 ? 0 : value;
            if (value.ToString("f" + Parameters.number_of_dots.ToString()).Length < 11)
            {
                Parameters.screen_text.text = value.ToString("f" + Parameters.number_of_dots.ToString());
            }
            else
            {
                Parameters.screen_text.text = "ERR";
            }
        }
        schema.Change_Ampere(Get_Ampere(current_pressure + innacuracy).ToString("f3"));
        
        
    }
    private float Get_Ampere(float pressure)
    {
        float ampere_value = (pressure - Parameters.min_pressure)/(Parameters.max_pressure - Parameters.min_pressure);

        switch (Parameters.current_ampere_mode)
        {
            case 0:
                ampere_value = 5.0f * ampere_value; 
                if (ampere_value > 5.0f)
                {
                    ampere_value = 5.0f;
                }
                else if (ampere_value < 0.0f)
                {
                    ampere_value = 0.0f;
                }
                break;
            case 1:
                ampere_value = 5 - 5.0f * ampere_value;
                if (ampere_value > 5.0f)
                {
                    ampere_value = 5.0f;
                }
                else if (ampere_value < 0.0f)
                {
                    ampere_value = 0.0f;
                }
                break;
            case 2:
                ampere_value = 4 + (20 - 4)*ampere_value;
                if (ampere_value > 20.0f)
                {
                    ampere_value = 20.0f;
                }
                else if (ampere_value < 4.0f)
                {
                    ampere_value = 4.0f;
                }
                break;
            case 3:
                ampere_value = 24 - 5 - 5.0f * ampere_value;
                if (ampere_value > 20.0f)
                {
                    ampere_value = 20.0f;
                }
                else if (ampere_value < 4.0f)
                {
                    ampere_value = 4.0f;
                }
                break;
        }
        return ampere_value;
    }
    public void Change_Pressure(float pressure_in_atmosphere)       //подаём давление в атмосферах
    {
        pressure_in_atmosphere = 101.32499966284f * pressure_in_atmosphere;     //базовая ед. прибора - kPa
        input_pressure = pressure_in_atmosphere;
        current_pressure = pressure_in_atmosphere - delta_pressure;
    }

    public void Zero_setting()
    {
        
        delta_pressure = input_pressure;
        current_pressure = 0.0f;
        
    }
    public void Unit_Handle(GameObject child)       //функция обработки событий в родительском объекте
    {
        if (power_status == false && child.tag != "cap_front")
        {
            return;
        }
        switch (child.tag)
        {
            case "cap_front":
                cap_front.Cap_Change_State();
                break;
            case "menu_enter":
                Menu_Handler.Work((int)button_moves.enter);
                break;
            case "menu_left":
                Menu_Handler.Work((int)button_moves.left);
                break;
            case "menu_right":
                Menu_Handler.Work((int)button_moves.right);
                break;
            case "menu_zero":
                if (Parameters.current_screen_state == (int)screen_states.pressure_showing)
                {
                    Zero_setting();
                }
                break;
        }
       
    }

     
    public void Change_power_mode()
    {
        if (power_status == false)
        {
            power_status = true;
            screen_text_object.SetActive(true);
            screen_unit_text_object.SetActive(true);
        }
        else
        {
            power_status = false;
            screen_text_object.SetActive(false);
            screen_unit_text_object.SetActive(false);
            schema.Change_Ampere("0,000");
        }
    }
    
}
