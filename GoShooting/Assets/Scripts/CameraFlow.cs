using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour
{
    public GameObject bow;               //跟随的物体
    public float smothing = 5f;          //相机跟随的速度
    Vector3 offset;                      //相机与物体相对偏移位置

    void Start()
    {
        offset = transform.position - bow.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 target = bow.transform.position + offset;
        //摄像机自身位置到目标位置平滑过渡
        transform.position = Vector3.Lerp(transform.position, target, smothing * Time.deltaTime);
    }
}