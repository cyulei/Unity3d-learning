using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {

    public int score;                   //分数
    public int target_score;            //目标分数
    public int arrow_number;            //箭的数量
    void Start()
    {
        score = 0;
        target_score = 15;
        arrow_number = 10;
    }
    //记录分数
    public void Record(GameObject disk)
    {
        int temp = disk.GetComponent<RingData>().score;
        score = temp + score;
        //Debug.Log(score);
    }
}
