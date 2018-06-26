using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField userNameInput;           //用户名输入框
    public InputField passwordInput;           //密码输入框
    public Button loginButton;                 //登录按钮
    public Button registerButton;              //注册按钮
    public Text errorMessageText;              //错误消息文本

    void Awake()
    {
        loginButton.onClick.AddListener(Login);
        registerButton.onClick.AddListener(Register);
    }

    private void Login()
    {
        BlockInput();
        //发送登录用户的请求
        AuthenticationRequest request = new AuthenticationRequest();
        request.SetUserName(userNameInput.text);
        request.SetPassword(passwordInput.text);
        request.Send(OnLoginSuccess, OnLoginError);
    }

    private void OnLoginSuccess(AuthenticationResponse response)
    {
        //切换到游戏开始界面
        LoadingManager.Instance.LoadNextScene();
    }

    private void OnLoginError(AuthenticationResponse response)
    {
        UnblockInput();
        //将错误信息显示出来
        errorMessageText.text = response.Errors.JSON.ToString();
    }

    private void Register()
    {
        BlockInput();
        //发送注册用户的请求
        RegistrationRequest request = new RegistrationRequest();
        request.SetUserName(userNameInput.text);
        request.SetDisplayName(userNameInput.text);
        request.SetPassword(passwordInput.text);
        request.Send(OnRegistrationSuccess, OnRegistrationError);
    }

    private void OnRegistrationSuccess(RegistrationResponse response)
    {
        //注册成功则登录
        Login();
    }

    private void OnRegistrationError(RegistrationResponse response)
    {
        UnblockInput();
        errorMessageText.text = response.Errors.JSON.ToString();
    }
    //禁用输入
    private void BlockInput()
    {
        userNameInput.interactable = false;
        passwordInput.interactable = false;
        loginButton.interactable = false;
        registerButton.interactable = false;
    }
    //可以使用输入
    private void UnblockInput()
    {
        userNameInput.interactable = true;
        passwordInput.interactable = true;
        loginButton.interactable = true;
        registerButton.interactable = true;
    }
}
