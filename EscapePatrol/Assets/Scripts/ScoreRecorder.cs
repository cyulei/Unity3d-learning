using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public FirstSceneController sceneController;
    public int score = 0;                            //分数
    public int crystal_number = 12;                  //水晶数量

    // Use this for initialization
    void Start()
    {
        sceneController = (FirstSceneController)SSDirector.GetInstance().CurrentScenceController;
        sceneController.recorder = this;
    }
    public int GetScore()
    {
        return score;
    }
    public void AddScore()
    {
        score++;
    }
    public int GetCrystalNumber()
    {
        return crystal_number;
    }
    public void ReduceCrystal()
    {
        crystal_number--;
    }
}

