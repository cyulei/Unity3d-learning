using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public GameObject follow;            //跟随的物体
    public float smothing = 5f;          //相机跟随的速度
    Vector3 offset;                      //相机与物体相对偏移位置

    void Start()
    {
        offset = transform.position - follow.transform.position;
    }

    void FixedUpdate()
    {
        Vector3 target = follow.transform.position + offset;
        //摄像机自身位置到目标位置平滑过渡
        transform.position = Vector3.Lerp(transform.position, target, smothing * Time.deltaTime);
    }
}
