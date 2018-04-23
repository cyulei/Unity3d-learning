using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private IUserAction action;
    GUIStyle score_style = new GUIStyle();
    GUIStyle text_style = new GUIStyle();
    GUIStyle bold_style = new GUIStyle();
    GUIStyle over_style = new GUIStyle();
    private bool game_start = false;       //游戏开始

    // Use this for initialization
    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        text_style.normal.textColor = new Color(0, 0, 0, 1);
        text_style.fontSize = 16;
        score_style.normal.textColor = new Color(1, 0, 1, 1);
        score_style.fontSize = 16;
        bold_style.normal.textColor = new Color(1, 0, 0);
        bold_style.fontSize = 16;
        over_style.normal.textColor = new Color(1, 1, 1);
        over_style.fontSize = 25;
    }

    void Update()
    {
        if(game_start && !action.GetGameover())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                action.Shoot();
            }
            float translationY = Input.GetAxis("Vertical");
            float translationX = Input.GetAxis("Horizontal");
            //移动弓箭
            action.MoveBow(translationX, translationY);
        }
    }
    private void OnGUI()
    {
        if(game_start)
        {
            if (!action.GetGameover())
            {
                GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
                GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), score_style);

                GUI.Label(new Rect(Screen.width / 2 - 30, 8, 200, 50), "目标分数:", text_style);
                GUI.Label(new Rect(Screen.width / 2 + 50, 8, 200, 50), action.GetTargetScore().ToString(), score_style);

                GUI.Label(new Rect(Screen.width - 170, 5, 50, 50), "弓箭数:", text_style);
                for (int i = 0; i < action.GetResidueNum(); i++)
                {
                    GUI.Label(new Rect(Screen.width - 110 + 10 * i, 5, 50, 50), "I ", bold_style);
                }
                GUI.Label(new Rect(Screen.width - 170, 30, 200, 50), "风向: ", text_style);
                GUI.Label(new Rect(Screen.width - 110, 30, 200, 50), action.GetWind(), text_style);
            }

            if (action.GetGameover())
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "游戏结束", over_style);
                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
                {
                    action.Restart();
                    return;
                }
            }
        }
        else
        {
            GUI.Label(new Rect(Screen.width / 2 - 60, Screen.width / 2 - 320, 100, 100), "GoShooting!", over_style);
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.width / 2 - 220, 400, 100), "WSAD或者方向键控制弓箭移动,鼠标点击射箭", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "游戏开始"))
            {
                game_start = true;
                action.BeginGame();
            }
        }
    }
}
