using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public Transform father;                //父节点的Transform
    public float speed = 15;                //速度
    float ry, rx;                           //rotation x和rotation y
    // Use this for initialization
    void Start () {
        rx = Random.Range(10, 300);         //按照多少度的角度旋转
        ry = Random.Range(10, 300);
    }	
	// Update is called once per frame
	void Update () {
        this.transform.RotateAround(father.position, new Vector3(rx, ry, 0), speed * Time.deltaTime);
    }
}
