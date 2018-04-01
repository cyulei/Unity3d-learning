using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;
public class ViewUI : MonoBehaviour {

    private IUserAction action;
    public int sign = 0;

    bool isShow = false;
    void Start()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }
    void OnGUI()
    {
        GUIStyle text_style;
        GUIStyle button_style;
        text_style = new GUIStyle()
        {
            fontSize = 30
        };
        button_style = new GUIStyle("button")
        {
            fontSize = 15
        };
        if (GUI.Button(new Rect(10, 10, 60, 30), "Rule", button_style))
        {
            if (isShow)
                isShow = false;
            else
                isShow = true;
        }
        if(isShow)
        {
            GUI.Label(new Rect(Screen.width / 2 - 85, 10, 200, 50), "让全部牧师和恶魔都渡河");
            GUI.Label(new Rect(Screen.width / 2 - 120, 30, 250, 50), "每一边恶魔数量都不能多于牧师数量");
            GUI.Label(new Rect(Screen.width / 2 - 85, 50, 250, 50), "点击牧师、恶魔、船移动");
        }
        if (sign == 1)
        {
            GUI.Label(new Rect(Screen.width / 2-90, Screen.height / 2-120, 100, 50), "Gameover!", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 100, 50), "Restart", button_style))
            {
                action.Restart();
                sign = 0;
            }
        }
        else if (sign == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 120, 100, 50), "You Win!", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 100, 50), "Restart", button_style))
            {
                action.Restart();
                sign = 0;
            }
        }
    }
}
