using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
enum screen_states
{
    pressure_showing,
    main_menu,
}
public class Menu_Params 
{
    // Start is called before the first frame update
    public int current_screen_state = 0;
    public TMP_Text screen_text ;
    public TMP_Text screen_unit_text ;
    public int number_of_dots = 2; 
    public float max_pressure = 1015f;      //10 атмосфер
    public float min_pressure = 0f;
    public float menu_timer = 0f;
    public int current_unit = 1;
    public int current_ampere_mode = 0;
    public string[] unit_variants = {"MPa","kPa","Pa","kgf/m2","kgf/cm2","mm"};
    public string[] ampere_variants = {"0-5","5-0","4-20","20-4"};
}
