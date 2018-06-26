using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public const int boardSize = 15;         //棋盘的大小
    public Field fieldPrefab;                //棋盘的棋子预制体
    public float fieldSpacing = 0.25f;       //棋盘棋子之间的间隔

    void Awake()
    {
        for (int x = 0; x < boardSize; x++)
        {
            for (int y = 0; y < boardSize; y++)
            {
                //算出每个棋子的位置
                float offset = -fieldSpacing * (boardSize - 1) / 2.0f;
                Vector3 position = new Vector3(x * fieldSpacing + offset, y * fieldSpacing + offset, 0.0f);
                Field field = Instantiate(fieldPrefab, position, Quaternion.identity, this.transform);
                field.Initialize(x, y);
            }
        }
    }
}
