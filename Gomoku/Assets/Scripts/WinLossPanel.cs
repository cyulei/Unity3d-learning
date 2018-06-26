using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLossPanel : MonoBehaviour
{
    public RectTransform content;
    public Text winText;
    public Text lossText;
    public Button backButton;      //返回按钮

    void Awake()
    {
        ChallengeManager.Instance.ChallengeWon.AddListener(OnChallengeWon);
        ChallengeManager.Instance.ChallengeLost.AddListener(OnChallengeLost);
        backButton.onClick.AddListener(OnBackButtonClick);
        //隐藏结束界面
        Hide();
    }

    private void Show()
    {
        content.gameObject.SetActive(true);
    }

    private void Hide()
    {
        content.gameObject.SetActive(false);
    }

    private void OnChallengeWon()
    {
        winText.enabled = true;
        lossText.enabled = false;
        Show();
    }

    private void OnChallengeLost()
    {
        winText.enabled = false;
        lossText.enabled = true;
        Show();
    }

    private void OnBackButtonClick()
    {
        //返回上一个场景
        LoadingManager.Instance.LoadPreviousScene();
    }
}