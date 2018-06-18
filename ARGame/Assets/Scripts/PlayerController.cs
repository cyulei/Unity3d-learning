using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public Slider playerSlider;//玩家生命
    public float playerLife = 1;
    bool gameover = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet" && !gameover)
        {
            reduceBoold();
        }
    }
    //得到生命值
    public float getPlayerLife()
    {
        return playerLife;
    }
    //死亡
    public void Death()
    {
        gameover = true;
    }
    //减血
    public void reduceBoold()
    {
        playerLife -= 0.05f;
        playerSlider.value = playerLife;
    }
}
