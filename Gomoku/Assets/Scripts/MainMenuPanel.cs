using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public Button playButton;

    void Awake()
    {
        playButton.onClick.AddListener(Play);
        MatchNotFoundMessage.Listener += OnMatchNotFound;
        //监听挑战开始事件
        ChallengeStartedMessage.Listener += OnChallengeStarted;
    }

    //建立挑战成功，跳转到游戏界面
    private void OnChallengeStarted(ChallengeStartedMessage message)
    {
        LoadingManager.Instance.LoadNextScene();
    }

    private void Play()
    {
        BlockInput();
        //发送匹配玩家的请求
        MatchmakingRequest request = new MatchmakingRequest();
        request.SetMatchShortCode("DefaultMatch");
        request.SetSkill(0);
        request.Send(OnMatchmakingSuccess, OnMatchmakingError);
    }

    private void OnMatchmakingSuccess(MatchmakingResponse response) { }

    private void OnMatchmakingError(MatchmakingResponse response)
    {
        UnblockInput();
    }

    private void OnMatchNotFound(MatchNotFoundMessage message)
    {
        UnblockInput();
    }
    //保证只匹配一次
    private void BlockInput()
    {
        playButton.interactable = false;
    }

    private void UnblockInput()
    {
        playButton.interactable = true;
    }
}
