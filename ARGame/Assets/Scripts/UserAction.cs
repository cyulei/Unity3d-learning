using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAction : MonoBehaviour {
    public SceneController scene;

    private void OnGUI()
    {
        GUIStyle text_style = new GUIStyle();

        text_style.fontSize = 50;
        text_style.normal.textColor = Color.red;

        if (!scene.GetGameOver())
        {
            //攻击按钮按下
            if (GUI.Button(new Rect(Screen.width - 180, Screen.height - 100, 180, 90), "射击"))
            {
                scene.PlayerAttack();
            }
            //向左移动
            if (GUI.Button(new Rect(50, Screen.height - 100, 180, 90), "左转"))
            {
                scene.PlayerMove("left");
            }
            //向右移动
            if (GUI.Button(new Rect(560, Screen.height - 100, 180, 90), "右转"))
            {
                scene.PlayerMove("right");
            }
            //向下移动
            if (GUI.Button(new Rect(300, Screen.height - 100, 180, 90), "后退"))
            {
                scene.PlayerMove("");
            }
            //向上移动
            if (GUI.Button(new Rect(300, Screen.height - 300, 180, 90), "前进"))
            {
                scene.PlayerMove("forword");
            }
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 300, 150),"游戏结束", text_style);
        }
    }
}
