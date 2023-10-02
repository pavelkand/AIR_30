using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Schema_view;
    private bool is_schema_active = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Switch_schema()
    {
        if (is_schema_active)
        {
            Schema_view.SetActive(false);
            is_schema_active = false;
        }
        else
        {
            Schema_view.SetActive(true);
            is_schema_active = true;
        }
        
    }
    
}
