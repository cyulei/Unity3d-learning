using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public FirstSceneController scene_controller;         //场景控制器
    public ScoreRecorder recorder;                        //记录员

    void Start()
    {
        scene_controller = SSDirector.GetInstance().CurrentScenceController as FirstSceneController;
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    void OnTriggerEnter(Collider arrow_head)
    { 
        Transform arrow = arrow_head.gameObject.transform.parent;
        if (arrow == null)
        {
            return;
        }
        if(arrow.tag == "arrow")
        {
            arrow.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            arrow.GetComponent<Rigidbody>().isKinematic = true;
            recorder.Record(this.gameObject);
            arrow_head.gameObject.gameObject.SetActive(false); ;
            arrow.tag = "hit";
        }
    }
}
