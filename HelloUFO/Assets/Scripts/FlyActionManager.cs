using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager
{

    public UFOFlyAction fly;                            //飞碟飞行的动作
    //public FirstController scene_controller;             //当前场景的场景控制器

    protected void Start()
    {

    }
    //飞碟飞行
    public void UFOFly(GameObject disk, float angle, float power)
    {
        fly = UFOFlyAction.GetSSAction(disk.GetComponent<DiskData>().direction, angle, power);
        this.RunAction(disk, fly, this);
    }
}