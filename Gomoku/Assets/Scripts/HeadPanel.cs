using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadPanel : MonoBehaviour
{
    public PieceType PlayerType;          //玩家类型
    public Text text;
    public Image HeadImage;               //玩家代表的棋子图片
    private string PlayerName;             //玩家名字
   
    void Awake()
    {
        //根据棋子类型获取用户名
        PlayerName = (PlayerType == PieceType.Heart) ? ChallengeManager.Instance.HeartsPlayerName : ChallengeManager.Instance.SkullsPlayerName;
    }

    void Update()
    {
        //显示是哪一个用户的回合
        if(PlayerName == ChallengeManager.Instance.CurrentPlayerName)
        {
            HeadImage.rectTransform.localScale = new Vector3(1, 1, 1);
            text.text = PlayerName + " Turn";
        }
        else
        {
            HeadImage.rectTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }

    }
}
