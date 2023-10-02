using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour
{
    // Start is called before the first frame update
    private bool is_on = false;
    public TMP_Text text_pole;
    public string default_text;
    public string active_state_text;
    public void Change_state()
    {
        
        if (!is_on)
        {
            text_pole.text = active_state_text;
            is_on = true;
            
        }
        else
        {
            text_pole.text = default_text;
            is_on = false;
        }
    }
    void Start()
    {
         text_pole.text = default_text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
