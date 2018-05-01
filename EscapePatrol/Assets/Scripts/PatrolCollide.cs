using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollide : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            //玩家进入侦察兵追捕范围
            this.gameObject.transform.parent.GetComponent<PatrolData>().follow_player = true;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = collider.gameObject;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.gameObject.transform.parent.GetComponent<PatrolData>().follow_player = false;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = null;
        }
    }
}
