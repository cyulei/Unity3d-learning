using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("player");
	}
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.activeSelf)
             this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position,5f);
        if(transform.position == player.transform.position)
        {
            this.gameObject.SetActive(false);
        }
	}
}
