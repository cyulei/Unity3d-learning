using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip gameoverClip;

    public void PlayMusic(AudioClip clip)
    {
        FirstSceneController scene = SSDirector.GetInstance().CurrentScenceController as FirstSceneController;
        //在一个玩家的位置播放音乐
        AudioSource.PlayClipAtPoint(clip, scene.player.transform.position);
    }
    void OnEnable()
    {
        GameEventManager.GameoverChange += Gameover;
    }
    void OnDisable()
    {
        GameEventManager.GameoverChange -= Gameover;
    }
    //播放游戏结束时音乐
    void Gameover()
    {
        PlayMusic(gameoverClip);
    }
}
