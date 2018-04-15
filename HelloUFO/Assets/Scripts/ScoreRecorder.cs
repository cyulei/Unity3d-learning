using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public int score;                   //分数
    void Start ()
    {
        score = 0;
    }
    //记录分数
    public void Record(GameObject disk)
    {
        int temp = disk.GetComponent<DiskData>().score;
        score = temp + score;
        //Debug.Log(score);
    }
    //重置分数
    public void Reset()
    {
        score = 0;
    }
}
