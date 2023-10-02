using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Slider_Script : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slide;
    public Air30 device;
    void Start()
    {
        slide = gameObject.GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Send_New_Value()
    {
        device.Change_Pressure(slide.value);
    }
    
}
