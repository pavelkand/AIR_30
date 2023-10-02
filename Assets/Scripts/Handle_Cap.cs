using UnityEngine;
using Unity;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class Handle_Cap : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    bool open_rotate = false;        //переменная, отвечающая за то, происходит ли в данный момент вращение крышки
    bool close_rotate = false;
    int is_opened = -1;     //хранит текущее состояние : -1: закрыта, 0 - в действии, 1 - открыта
    float timer;        //переменная, в которой будем хранить время на момент запуска процесса открытия крышки
    public float opening_time = 3f;       //время открытия (в секундах)
    public GameObject axis;     //поле, в которое передаём объект - ось
    public float speed = 100;       //скорость откручивания
    
    Color default_color;
    public Air30 parent;
    MeshRenderer render;
    
    private void Start() 
    {
        render = GetComponent<MeshRenderer>();
        default_color = render.material.color;
        
    }
    public int Get_state() 
    {
        return is_opened;
        
    }
    public void OnPointerClick(PointerEventData eventData)      //обработка клика
    {
        parent.Unit_Handle(gameObject);
    }
    public void OnPointerEnter(PointerEventData eventData)      //обработка наведения
    {
            render.material.color = Color.red;
    }
    public void OnPointerExit(PointerEventData eventData)      //обработка наведения
    {
            render.material.color = default_color;
    }
    private void Update()
    {
        Cap_Work();
    }
    public void Cap_Change_State()
    {
        if (is_opened == -1)
        {
            Cap_Open();
        }
        else if (is_opened == 1)
        {
            Cap_Close();
        }
    }
    public void Cap_Open()      //функция, запускающая откручивание крышки
    {
        if (is_opened == -1)
        {
            open_rotate = true;
            is_opened = 0;
            timer = Time.time;      //запись текущего времени, отправная точка для вычисения момента завершения откручивания
        }
    }
        
    public void Cap_Close()      //функция, запускающая закручивание крышки
    {
        if (is_opened == 1 )
        {
            close_rotate = true;
            is_opened = 0;
            timer = Time.time;      //запись текущего времени, отправная точка для вычисения момента завершения откручивания
            //gameObject.SetActive(true);
        }
       
    }
    private void Cap_Work()     //функция отрабатывающая откручивание крышки, если переменная rotate = true  
    {
        if (open_rotate)
        {
            if (Time.time < timer + opening_time)     //проверка таймера
            {
                gameObject.transform.RotateAround(axis.transform.position,new Vector3(0,0,1),speed * Time.deltaTime);       //вращение вокруг оси
                gameObject.transform.localPosition += new Vector3(0,0,-0.50f * Time.deltaTime * speed);      //смещение вперед 
            }
            else        //время вышло, откручивание останавливается
            {
                open_rotate = false;     //остановка откручивания
                is_opened = 1;
                timer = 0f;
                //gameObject.SetActive(false);        //после откручивания крышка исчезает
                
            }
        }
        else if (close_rotate)      //закручивание крышки 
        {
            if (Time.time < timer + opening_time)     //проверка таймера
            {
                gameObject.transform.RotateAround(axis.transform.position,new Vector3(0,0,-1),speed * Time.deltaTime);       //вращение вокруг оси
                gameObject.transform.localPosition += new Vector3(0,0,0.50f * Time.deltaTime * speed);      //смещение вперед
                  
            }
            else        //время вышло, откручивание останавливается
            {
                open_rotate = false;     //остановка откручивания
                is_opened = -1;
                timer = 0f;
                
            }
        }
    }
    
}
