using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
enum button_moves
{
    left,
    right,
    enter
}
public class Menu_Handle 
{
    // Start is called before the first frame update
    private string[] menu_positions = {"Unit","PreS","diAP","oUti","rEt"};
    
    private int current_menu_position_l0 = 0;
    private int current_menu_level = 0;
    private float diap_value;
    private int pres_value;
    private int ampere_value;
    private int unit_value;
    private bool diap_mode = false;
    private Menu_Params Parameters;
    
    public Menu_Handle(Menu_Params Parameters)
    {
        this.Parameters = Parameters;
    }
    public void Quit_Menu()
    {
        Parameters.current_screen_state = (int)screen_states.pressure_showing;
        current_menu_position_l0 = 0;
        current_menu_level = 0;
        diap_mode = false;
        Parameters.screen_unit_text.text = Parameters.unit_variants[Parameters.current_unit];
    }

    public void Work (int move)
    {
        Parameters.menu_timer = 0f;
        switch (current_menu_level)
        {
            case 0:
                Menu_Handle_L0(move);
                break;
            case 1:
                Menu_Handle_L1(move);
                break;
        }
    }
    
    private void Menu_Handle_L0(int move)
    {
        
        if (Parameters.current_screen_state == (int)screen_states.pressure_showing)
        {
            if (move == (int)button_moves.enter)
            {
                current_menu_position_l0 = 0;
                current_menu_level = 0;
                Parameters.current_screen_state = (int)screen_states.main_menu;
                Parameters.screen_text.text = menu_positions[current_menu_position_l0];
            }
        }
        else
        {
            if (move == (int)button_moves.left || move == (int)button_moves.right) 
            {
                if (move == (int)button_moves.right)
                {
                    current_menu_position_l0 = current_menu_position_l0 == menu_positions.Length -1 ? 0 : current_menu_position_l0 + 1; 
                }
                else if (move == (int)button_moves.left)
                {
                    current_menu_position_l0 = current_menu_position_l0 == 0 ? menu_positions.Length -1: current_menu_position_l0 - 1;
                }
                Parameters.screen_text.text = menu_positions[current_menu_position_l0];
            }
            else if (move == (int)button_moves.enter)
            {
                switch (current_menu_position_l0)
                {
                    case 0:     //unit
                        current_menu_level = 1;
                        Menu_Handle_L1(move,true);
                        break;
                    case 1:     //pres
                        current_menu_level = 1;
                        Menu_Handle_L1(move,true);
                        break;
                    case 2:     //diap
                        current_menu_level = 1;
                        Menu_Handle_L1(move,true);
                        break;
                    case 3:     //outi
                        current_menu_level = 1;
                        Menu_Handle_L1(move,true);
                        break;
                    case 4:     //ret
                        Parameters.current_screen_state = (int)screen_states.pressure_showing;
                        current_menu_position_l0 = 0;
                        break;                    
                }
            }
        }
        
    }

    private void Menu_Handle_L1(int move, bool select_flag = false)
    {
        switch (current_menu_position_l0)
        {
            case 0:     //unit
                unit(move,select_flag);
                break;
            case 1:     //pres
                pres(move,select_flag);
                break;
            case 2:     //diap
                diap(move, select_flag);
                break;
            case 3:     //outi
                outi(move, select_flag);
                break;
        }
    }
    private void diap(int move, bool select_flag)
    {
        if (select_flag == true)
        {
            diap_value = Parameters.min_pressure;
            Parameters.screen_text.text = diap_value.ToString("f1");
            Parameters.screen_unit_text.text = Parameters.unit_variants[1];
            return;
        }
        
        if(move == (int)button_moves.left)
        {
            diap_value -= 5.0f;
            if (diap_value < 0f)
            {
                diap_value = 0f;
            }
        }
        else if (move == (int)button_moves.right)
        {
            diap_value += 5.0f;
        }
        else if (move == (int)button_moves.enter)
        {
            if (diap_mode == false)
            {
                Parameters.min_pressure = diap_value;
                diap_value = Parameters.max_pressure;
                diap_mode = true;
            }
            else
            {
                if (Parameters.min_pressure < diap_value)
                {
                    Parameters.max_pressure = diap_value; 
                }
                diap_mode = false;
                current_menu_level = 0;
                Parameters.screen_text.text = menu_positions[current_menu_position_l0];
                Parameters.screen_unit_text.text = Parameters.unit_variants[Parameters.current_unit];
                return;
            }
           
        }
        Parameters.screen_text.text = diap_value.ToString("f1");
    }
    private void pres(int move, bool select_flag)
    {
        if (select_flag == true)
        {
            pres_value = Parameters.number_of_dots;
            Parameters.screen_text.text = pres_value.ToString();
            return;
        }

        if(move == (int)button_moves.left && pres_value > 0 )
        {
            pres_value -= 1;
        }
        else if (move == (int)button_moves.right && pres_value < 3)
        {
            pres_value += 1;
        }
        else if (move == (int)button_moves.enter)
        {
            Parameters.number_of_dots = pres_value;
            current_menu_level = 0;
            Parameters.screen_text.text = menu_positions[current_menu_position_l0];
            return;
        }
        Parameters.screen_text.text = pres_value.ToString();

    }

    private void unit(int move, bool select_flag)
    {
        if (select_flag == true)
        {
            Parameters.screen_text.text = Parameters.unit_variants[0];
            unit_value = 0;
            return;
        }

        if(move == (int)button_moves.left)
        {
            unit_value = unit_value != 0 ? unit_value - 1 : Parameters.unit_variants.Length - 1;
        }
        else if (move == (int)button_moves.right && pres_value < 3)
        {
            unit_value = unit_value != Parameters.unit_variants.Length - 1 ? unit_value + 1 : 0;
        }
        else if (move == (int)button_moves.enter)
        {
            Parameters.current_unit = unit_value;
            Parameters.screen_unit_text.text = Parameters.unit_variants[unit_value];
            current_menu_level = 0;
            Parameters.screen_text.text = menu_positions[current_menu_position_l0];
            return;
        }
        Parameters.screen_text.text = Parameters.unit_variants[unit_value];
    }

    private void outi(int move, bool select_flag)
    {
        if (select_flag == true)
        {
            Parameters.screen_text.text = Parameters.ampere_variants[0];
            ampere_value = 0;
            return;
        }

        if(move == (int)button_moves.left)
        {
            ampere_value = ampere_value != 0 ? ampere_value - 1 : Parameters.ampere_variants.Length - 1;
        }
        else if (move == (int)button_moves.right && pres_value < 3)
        {
            ampere_value = ampere_value != Parameters.ampere_variants.Length - 1 ? ampere_value + 1 : 0;
        }
        else if (move == (int)button_moves.enter)
        {
            Parameters.current_ampere_mode = ampere_value;
            current_menu_level = 0;
            Parameters.screen_text.text = menu_positions[current_menu_position_l0];
            return;
        }
        Parameters.screen_text.text = Parameters.ampere_variants[ampere_value];
    }
}
