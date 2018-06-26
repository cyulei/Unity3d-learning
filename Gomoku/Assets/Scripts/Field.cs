using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    private Animator animator;        //棋子的动画，棋子的切换是由状态机控制的
    private int x;
    private int y;

    void Awake()
    {
        animator = GetComponent<Animator>();
        //监听回合切换事件
        ChallengeManager.Instance.ChallengeTurnTaken.AddListener(OnChallengeTurnTaken);
    }

    public void Initialize(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    void OnMouseDown()
    {
        //发送落子位置
        ChallengeManager.Instance.Move(x, y);
    }

    void OnMouseEnter()
    {
        animator.SetBool("IsHovered", true);
    }

    void OnMouseExit()
    {
        animator.SetBool("IsHovered", false);
    }

    private void OnChallengeTurnTaken()
    {
        //玩家落子后，获取落子位置的类型
        PieceType pieceType = ChallengeManager.Instance.Fields[x + y * ChessBoard.boardSize];
        //改变图形
        if (pieceType == PieceType.Heart)
        {
            animator.SetBool("IsHeart", true);
        }
        else if (pieceType == PieceType.Skull)
        {
            animator.SetBool("IsSkull", true);
        }
    }
}
