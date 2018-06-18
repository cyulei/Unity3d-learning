using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerController : MonoBehaviour {
    public GameObject bullet;
    public GameObject player;
    public Slider towerSlider; //塔生命
    public float towerLife = 1;
    public float attackDistance = 15f;
    public float time = 0f;
    bool gameover = false;
    public Transform open;
    float speed = 10f;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(player != null && !gameover)
        {
            float dis = Vector3.Distance(transform.position, player.transform.position);
            if(dis < attackDistance)
            {
                if(player.GetComponent<PlayerController>().getPlayerLife()>0)
                {
                    transform.LookAt(player.transform);
                    time += Time.deltaTime;
                    if(time >= 2)
                    {
                        //发射子弹
                        shoot(player.transform.position);
                        time = 0;
                    }
                }
            }
            if (bullet.activeSelf)
                bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, player.transform.position, Time.deltaTime * speed);
            if (bullet.transform.position == player.transform.position)
            {
                 bullet.SetActive(false);
            }
        }
	}
    //得到生命值
    public float getTowerLife()
    {
        return towerLife;
    }
    //塔掉血
    public void ReduceBlood()
    {
        towerLife -= 0.1f;
        towerSlider.value = towerLife;
    }
    //发射子弹
    void shoot(Vector3 position)
    {
        bullet.transform.position = open.position;
        bullet.SetActive(true);
       // Debug.Log(bullet.transform.localPosition + " and " + position);     
    }
    //死亡
    public void death()
    {
        gameover = true;
    }
}
