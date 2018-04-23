using UnityEngine;
using System.Collections;

public class ArrowTremble : SSAction
{
    float radian = 0;                             // 弧度  
    float per_radian = 3f;                        // 每次变化的弧度  
    float radius = 0.01f;                         // 半径  
    Vector3 old_pos;                              // 开始时候的坐标  
    public float left_time = 0.8f;                 //动作持续时间

    private ArrowTremble() { }

    public override void Start()
    {
        //将最初的位置保存  
        old_pos = transform.position;             
    }
    
    public static ArrowTremble GetSSAction()
    {
        ArrowTremble action = CreateInstance<ArrowTremble>();
        return action;
    }
    public override void Update()
    {
        left_time -= Time.deltaTime;
        if (left_time <= 0)
        {
            //颤抖后回到初始位置
            transform.position = old_pos;
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }

        // 弧度每次增加
        radian += per_radian;
        //y轴的位置变化,上下颤抖
        float dy = Mathf.Cos(radian) * radius; 
        transform.position = old_pos + new Vector3(0, dy, 0);
    }
    public override void FixedUpdate()
    {
    }
}