using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private IUserAction action;
    private GUIStyle score_style = new GUIStyle();
    private GUIStyle text_style = new GUIStyle();
    private GUIStyle over_style = new GUIStyle();
    public  int show_time = 8;                         //展示提示的时间长度
    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        text_style.normal.textColor = new Color(0, 0, 0, 1);
        text_style.fontSize = 16;
        score_style.normal.textColor = new Color(1,0.92f,0.016f,1);
        score_style.fontSize = 16;
        over_style.fontSize = 25;
        //展示提示
        StartCoroutine(ShowTip());
    }

    void Update()
    {
        //获取方向键的偏移量
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        //移动玩家
        action.MovePlayer(translationX, translationZ);
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
        GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), score_style);
        GUI.Label(new Rect(Screen.width - 170, 5, 50, 50), "剩余水晶数:", text_style);
        GUI.Label(new Rect(Screen.width - 80, 5, 50, 50), action.GetCrystalNumber().ToString(), score_style);
        if(action.GetGameover() && action.GetCrystalNumber() != 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "游戏结束", over_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }
        else if(action.GetCrystalNumber() == 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "恭喜胜利！", over_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }
        }
        if(show_time > 0)
        {
            GUI.Label(new Rect(Screen.width / 2-80 ,10, 100, 100), "按WSAD或方向键移动", text_style);
            GUI.Label(new Rect(Screen.width / 2 - 87, 30, 100, 100), "成功躲避巡逻兵追捕加1分", text_style);
            GUI.Label(new Rect(Screen.width / 2 - 90, 50, 100, 100), "采集完所有的水晶即可获胜", text_style);
        }
    }

    public IEnumerator ShowTip()
    {
        while (show_time >= 0)
        {
            yield return new WaitForSeconds(1);
            show_time--;
        }
    }
}
