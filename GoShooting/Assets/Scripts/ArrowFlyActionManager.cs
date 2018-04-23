using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFlyActionManager : SSActionManager
{

    private ArrowFlyAction fly;                                //箭飞行的动作
    public FirstSceneController scene_controller;              //当前场景的场景控制器

    protected void Start()
    {
        scene_controller = (FirstSceneController)SSDirector.GetInstance().CurrentScenceController;
        scene_controller.action_manager = this;
    }
    //箭飞行
    public void ArrowFly(GameObject arrow,Vector3 wind)
    {
        fly = ArrowFlyAction.GetSSAction(wind);
        this.RunAction(arrow, fly, this);
    }
}
