using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSelf : MonoBehaviour {
    public float speed = 2;                      //自转速度
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(this.transform.position, Vector3.up, speed);
    }
}
