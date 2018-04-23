using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFlyAction : SSAction
{
    public Vector3 force;                      //初始时候给箭的力
    public Vector3 wind;                       //风方向上的力
    private ArrowFlyAction() { }
    public static ArrowFlyAction GetSSAction(Vector3 wind)
    {
        ArrowFlyAction action = CreateInstance<ArrowFlyAction>();
        //给予箭z轴方向的力
        action.force = new Vector3(0, 0, 20);
        action.wind = wind;
        return action;
    }

    public override void Update(){}

    public override void FixedUpdate()
    {
        //风的力持续作用在箭身上
        this.gameobject.GetComponent<Rigidbody>().AddForce(wind, ForceMode.Force);

        //检测是否被击中或是超出边界
        if (this.transform.position.z > 30 || this.gameobject.tag == "hit")
        {
            this.destroy = true;
            this.callback.SSActionEvent(this,this.gameobject);
        }
    }
    public override void Start()
    {
        gameobject.transform.parent = null;
        gameobject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameobject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
}
