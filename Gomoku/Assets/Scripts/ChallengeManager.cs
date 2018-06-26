using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ChallengeManager : Singleton<ChallengeManager>
{
    public UnityEvent ChallengeStarted;    //可注册的事件，当挑战开始
    public UnityEvent ChallengeTurnTaken;  //可注册的事件，切换回合
    public UnityEvent ChallengeWon;        //可注册的事件，胜利
    public UnityEvent ChallengeLost;       //可注册的事件，失败

    private string challengeID;         //挑战的ID，游戏ID
    public bool IsChallengeStart;       //挑战开始
    public string CurrentPlayerName;    //当前玩家名字
    public string HeartsPlayerName;     //心形棋子的玩家名字
    public string HeartsPlayerId;       //心形棋子的玩家ID
    public string SkullsPlayerName;     //骷髅棋子的玩家名字
    public string SkullsPlayerId;       //骷髅棋子的玩家ID
    public PieceType[] Fields;          //整个棋盘的数据

    void Start()
    {
        //注册监听方法
        ChallengeStartedMessage.Listener += OnChallengeStarted;
        ChallengeTurnTakenMessage.Listener += OnChallengeTurnTaken;
        ChallengeWonMessage.Listener += OnChallengeWon;
        ChallengeLostMessage.Listener += OnChallengeLost;
    } 

    private void OnChallengeStarted(ChallengeStartedMessage message)
    {
        challengeID = message.Challenge.ChallengeId;
        HeartsPlayerName = message.Challenge.Challenger.Name;
        HeartsPlayerId = message.Challenge.Challenger.Id;
        SkullsPlayerName = message.Challenge.Challenged.First().Name;
        SkullsPlayerId = message.Challenge.Challenged.First().Id;
        CurrentPlayerName = message.Challenge.NextPlayer == HeartsPlayerId ? HeartsPlayerName : SkullsPlayerName;
        IsChallengeStart = true;
        //将数据库中的棋盘数据拿到
        Fields = message.Challenge.ScriptData.GetIntList("fields").Cast<PieceType>().ToArray();
        ChallengeStarted.Invoke();
    }

    private void OnChallengeTurnTaken(ChallengeTurnTakenMessage message)
    {
        //切换当前玩家名字
        CurrentPlayerName = message.Challenge.NextPlayer == HeartsPlayerId ? HeartsPlayerName : SkullsPlayerName;
        //将数据库中的棋盘数据拿到
        Fields = message.Challenge.ScriptData.GetIntList("fields").Cast<PieceType>().ToArray();
        ChallengeTurnTaken.Invoke();
    }

    private void OnChallengeWon(ChallengeWonMessage message)
    {
        IsChallengeStart = false;
        ChallengeWon.Invoke();
    }

    private void OnChallengeLost(ChallengeLostMessage message)
    {
        IsChallengeStart = false;
        ChallengeLost.Invoke();
    }

    public void Move(int x, int y)
    {
        //发送落子的位置信息
        LogChallengeEventRequest request = new LogChallengeEventRequest();
        request.SetChallengeInstanceId(challengeID);
        request.SetEventKey("Move");
        request.SetEventAttribute("X", x);
        request.SetEventAttribute("Y", y);
        request.Send(OnMoveSuccess, OnMoveError);
    }

    private void OnMoveSuccess(LogChallengeEventResponse response)
    {
        print(response.JSONString);
    }

    private void OnMoveError(LogChallengeEventResponse response)
    {
        print(response.Errors.JSON.ToString());
    }
}
