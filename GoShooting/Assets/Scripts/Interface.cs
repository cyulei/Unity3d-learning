using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController
{
    void LoadResources();
}

public interface IUserAction
{
    //获得键盘输入，移动弓
    void MoveBow(float offsetX, float offsetY);
    //射箭
    void Shoot();
    //获得分数
    int GetScore();
    //得到目标分数
    int GetTargetScore();
    //剩余的箭数量
    int GetResidueNum();
    //重新开始
    void Restart();
    //得到是否游戏结束
    bool GetGameover();
    //得到风文本
    string GetWind();
    //游戏开始
    void BeginGame();
}

public enum SSActionEventType : int { Started, Competeted }
public interface ISSActionCallback
{
    void SSActionEvent(SSAction source, GameObject arrow = null);
}