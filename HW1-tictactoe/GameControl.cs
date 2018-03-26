using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    private int[,] board = new int[3, 3];          //井字棋的棋盘
    private int click = 1;                         //1代表X的回合，-1代表O的回合
    private enum GameState {end ,mode1 ,mode2 };   //游戏状态有结束，模式1(玩家vs玩家)，模式2(玩家vs电脑)
    private bool isWin = false;                    //有玩家获胜
    private GameState gamestate = GameState.end;   //游戏状态
    // Use this for initialization
    void Start () {
        Reset();
	}
    void Reset()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = 0;                   //每一个小格子都没有棋子
    }
    void OnGUI()
    {     
        GUIStyle fontStyle = new GUIStyle()
        {
            fontSize = 25
        };
        fontStyle.normal.textColor = new Color(255, 255, 255);
        GUIStyle fontStyle1 = new GUIStyle()
        {
            fontSize = 30
        };
        fontStyle1.normal.textColor = new Color(255, 255, 255);
        GUI.Label(new Rect(413, 50, 100, 50), "井字游戏", fontStyle1);
        
        if(gamestate == GameState.end)
        {
            if (GUI.Button(new Rect(400, 200, 140, 50), "Player vs Player"))
            {
                gamestate = GameState.mode1;
                isWin = false;
            }
            if (GUI.Button(new Rect(400, 280, 140, 50), "Player vs Computer"))
            {
                gamestate = GameState.mode2;
                isWin = false;
            }
        }
        if(gamestate == GameState.mode1)
        {
            FixedUI(fontStyle);
            bool full = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 1)
                    {
                        GUI.Button(new Rect(350 + i * 80, 100 + j * 80, 80, 80), "X");
                    }
                    else if(board[i, j] == 2)
                    {
                        GUI.Button(new Rect(350 + i * 80, 100 + j * 80, 80, 80), "O");
                    }
                    else
                    {
                        full = false;
                    }
                    if (GUI.Button(new Rect(350 + i * 80, 100 + j * 80, 80, 80), ""))   //如果空白格子被点击
                    {
                        if(!isWin)
                        {
                            if (click == 1)                                       //X的回合
                            {
                                board[i, j] = 1;                                  //棋盘下X
                            }
                            if (click == -1)                                      //O的回合
                            {
                                board[i, j] = 2;                                  //棋盘下O
                            }
                            click = -click;
                        }
                    }
                }
            }
            if(full && Check() == 0)
            {
                GUI.Label(new Rect(430, 350, 100, 50), "dogfall!", fontStyle);
                isWin = true;
            }
        }
        if (gamestate == GameState.mode2)
        {
            FixedUI(fontStyle);
            bool full = true;

            if (click == 1 && !isWin)        //AI的回合
            {
                int a = 1, b = 2, c = 0;    //1代表检测X是否有取胜机会,2代表检测O是否有取胜机会
                if (!CheckGo(c, c, a))
                {
                    if (!CheckGo(c, c, b))
                    {
                        int pos_x, pos_y;
                        System.Random ran = new System.Random();
                        pos_x = ran.Next(0, 2);
                        pos_y = ran.Next(0, 2);
                        int count = 0;
                        while (board[pos_x, pos_y] != 0 && Check() == 0)
                        {
                            pos_x = ran.Next(0, 2);
                            pos_y = ran.Next(0, 2);
                            count++;
                            if(count == 5)
                            {
                                FindEmpty();
                                break;
                            }
                        }
                        if(count != 5)
                        {
                            board[pos_x, pos_y] = 1;
                            click = -click;
                        }
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (GUI.Button(new Rect(350 + i * 80, 100 + j * 80, 80, 80), ""))//如果空白格子被点击
                    {
                        if (!isWin)
                        {
                            if (click == -1 && board[i, j] == 0)
                            {
                                board[i, j] = 2;
                                click = -click;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 2)
                    {
                        GUI.Button(new Rect(350 + i * 80, 100 + j * 80, 80, 80), "O");
                    }
                    else if (board[i, j] == 1)
                    {
                        GUI.Button(new Rect(350 + i * 80, 100 + j * 80, 80, 80), "X");
                    }
                    else if (board[i, j] == 0)
                    {
                        full = false;
                    }
                }
            }
            if (full && Check() == 0)
            {
                GUI.Label(new Rect(430, 350, 100, 50), "dogfall!", fontStyle);
                isWin = true;
            }
        }
    }
    int Check()                //检查是否有人取胜
    {
        for (int i = 0; i < 3; i++)              
        {
            if (board[i, 0] != 0 && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return board[i, 0];
            }
        }
        for (int j = 0; j < 3; j++)              
        {
            if (board[0, j] != 0 && board[1, j] == board[0, j] && board[1, j] == board[2, j])
            {
                return board[0, j];
            }
        }
        if ((board[1, 1] != 0 && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) ||
            (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]))   //判断斜线是否连成三个
        {
            return board[1, 1];
        }
        return 0;
    }
    void FixedUI(GUIStyle fontStyle)            //固定不变的UI
    {
        int result = Check();
        if (result == 1)
        {
            GUI.Label(new Rect(430, 350, 100, 50), "X wins!", fontStyle);
            isWin = true;
        }
        else if (result == 2)
        {
            GUI.Label(new Rect(430, 350, 100, 50), "O wins!", fontStyle);
            isWin = true;
        }

        if (GUI.Button(new Rect(420, 390, 100, 50), "Reset"))
        {
            Reset();
            isWin = false;
        }
        if (GUI.Button(new Rect(420, 450, 100, 50), "Return"))
        {
            Reset();
            gamestate = GameState.end;
            isWin = false;
        }
        if (!isWin && click == 1)
        {
            GUI.Label(new Rect(430, 350, 100, 50), "X turn", fontStyle);
        }
        if (!isWin && click == -1)
        {
            GUI.Label(new Rect(430, 350, 100, 50), "O turn", fontStyle);
        }
    }
    bool CheckGo(int pos_x,int pos_y,int mod)                //检查AI和玩家是否有获胜机会
    {
        int piecesNum = 0;
        for (int i = 0; i < 3; i++)
        {
            piecesNum = 0;
            bool empty = false;
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == mod)
                {
                    piecesNum++;
                }
                else if (board[i, j] == 0)
                {
                    pos_x = i;
                    pos_y = j;
                    empty = true;
                }
            }
            if (piecesNum == 2 && empty && board[pos_x,pos_y] == 0)
            {
                board[pos_x, pos_y] = 1;
                click = -click;
                return true;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            piecesNum = 0;
            bool empty = false;
            for (int j = 0; j < 3; j++)
            {
                if (board[j, i] == mod)
                {
                    piecesNum++;
                }
                else if (board[j, i] == 0)
                {
                    pos_x = j;
                    pos_y = i;
                    empty = true;
                }
            }
            if (piecesNum == 2 && empty && board[pos_x, pos_y] == 0)
            {
                board[pos_x, pos_y] = 1;
                click = -click;
                return true;
            }
        }
        piecesNum = 0;
        bool empty1 = false;
        for (int i = 0; i < 3; i++)
        {     
            if (board[i, i] == mod)
            {
                piecesNum++;
            }
            else if (board[i, i] == 0)
            {
                pos_x = i;
                pos_y = i;
                empty1 = true;
            }
        }
        if (piecesNum == 2 &&empty1 && board[pos_x, pos_y] == 0)
        {
            board[pos_x, pos_y] = 1;
            click = -click;
            return true;
        }
        piecesNum = 0;
        empty1 = false;
        for (int i = 0; i < 3; i++)
        {
            int j = 2 - i;
            if (board[i, j] == mod)
            {
                piecesNum++;
            }
            else if (board[i, j] == 0)
            {
                pos_x = i;
                pos_y = j;
                empty1 = true;
            }
        }
        if (piecesNum == 2 && empty1 && board[pos_x, pos_y] == 0)
        {
            board[pos_x, pos_y] = 1;
            click = -click;
            return true;
        }
        return false;
    }
    void FindEmpty()                //找到空的格子
    {
        for(int i=0;i<3;i++)
            for(int j=0;j<3;j++)
            {
                if(board[i,j] == 0)
                {
                    board[i,j] = 1;
                    click = -click;
                    return;
                }
            }
    }
}