using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject target;       //объект, вокруг которого будет вращаться камера (цель)
    private Vector3 offset;         //вектор, задающий положение камеры относительно центра цели
    public float sensitivity = 3;   //чувствительность мыши
    private float zoom = 0.06f;      //шаг для приближения/отдаления
    private float zoom_max = 0.53f;     //максимальное значение приближения
    private float zoom_min = 0.3f;     //максимальное значение отдаления
    private float default_offset_z;     //изначальный параметр z вектора offset, потребуется для введения ограничения на zoom
    private float x,y;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(-0.017f,0.1f,-0.7f);       //инициализация вектора подобранными значениями (для оптимальной видимости)
        default_offset_z = offset.z;        //сохранение изначального параметра z (изначальный масштаб)
        transform.position = transform.localRotation * offset + target.transform.position;         //позиционирование камеры по вектору offset,
        //сонаправленному с вектором поворота камеры (transform.localRotation * offset), относительно центрального оъекта (target.transform.position)
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && offset.z < default_offset_z + zoom_max)       //увеличение масштаба колесиком мыши, проверка условия
        {
            offset.z += zoom;       
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && offset.z > default_offset_z - zoom_min)       //уменьшение масштаба колесиком мыши, проверка условия
        {
            offset.z -= zoom;
        }
        if (Input.GetMouseButton(1))        //нажатие правой клавиши мыши
        {
            x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;      //получение нового угла поворота бъекта по y (c уч. перемещения мыши по x)
            y = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity;      //получение нового угла поворота бъекта по x (c уч. перемещения мыши по y)
            transform.localEulerAngles = new Vector3(y, x, 0);      //поворот камеры по полученным углам
            
        }
        transform.position = transform.localRotation * offset + target.transform.position;      //позиционирование камеры по вектору offset,
        //сонаправленному с вектором поворота камеры (transform.localRotation * offset), относительно центрального оъекта (target.transform.position)

    }
}
