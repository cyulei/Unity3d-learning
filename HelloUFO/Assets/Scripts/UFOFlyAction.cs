using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFlyAction : SSAction
{
    public float gravity = -6;                                 //向下的加速度
    private Vector3 start_vector;                              //初速度向量
    private Vector3 gravity_vector = Vector3.zero;             //加速度的向量，初始时为0
    private float time;                                        //已经过去的时间
    private Vector3 current_angle = Vector3.zero;               //当前时间的欧拉角

    private UFOFlyAction() { }
    public static UFOFlyAction GetSSAction(Vector3 direction, float angle, float power)
    {
        //初始化物体将要运动的初速度向量
        UFOFlyAction action = CreateInstance<UFOFlyAction>();
        if (direction.x == -1)
        {
            action.start_vector = Quaternion.Euler(new Vector3(0, 0, -angle)) * Vector3.left * power;
        }
        else
        {
            action.start_vector = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * power;
        }
        return action;
    }

    public override void Update()
    {
        //计算物体的向下的速度,v=at
        time += Time.fixedDeltaTime;
        gravity_vector.y = gravity * time;

        //位移模拟
        transform.position += (start_vector + gravity_vector) * Time.fixedDeltaTime;
        current_angle.z = Mathf.Atan((start_vector.y + gravity_vector.y) / start_vector.x) * Mathf.Rad2Deg;
        transform.eulerAngles = current_angle;

        //如果物体y坐标小于-10，动作就做完了
        if (this.transform.position.y < -10)
        {
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }
    public override void FixedUpdate() { }
    public override void Start() { }
}