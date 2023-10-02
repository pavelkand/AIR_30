using UnityEngine;
using Unity;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class Handle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    MeshRenderer render;
    Color default_color;
    public Air30 parent;
    private void Start() 
    {
        render = GetComponent<MeshRenderer>();
        default_color = render.material.color;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        parent.Unit_Handle(gameObject);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
            render.material.color = Color.red;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
            render.material.color = default_color;
    }
}
