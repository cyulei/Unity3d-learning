using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCamera : MonoBehaviour
{   
    public bool isShow = false;                   //是否显示副摄像机
    public float leftTime;                        //显示时间

    void Update()
    {
        if (isShow)
        {
            leftTime -= Time.deltaTime;
            if (leftTime <= 0)
            {
                this.gameObject.SetActive(false);
                isShow = false;
            }
        }
    }

    public void StartShow()
    {
        this.gameObject.SetActive(true);
        isShow = true;
        leftTime = 2f;
    }
}