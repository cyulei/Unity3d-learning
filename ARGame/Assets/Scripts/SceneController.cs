using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {
    public GameObject player;//玩家
    public GameObject tower; //塔
    public float distance = 15f;
    public float speed = 5f;
    Vector3 target;
    bool isMove = false;
    bool gameover = false;
	// Update is called once per frame
	void Update ()
    {
        if(player.GetComponent<PlayerController>().getPlayerLife() <= 0)
        {
            gameover = true;
            player.GetComponent<PlayerController>().Death();
            player.GetComponent<Animator>().SetBool("death", true);
        }
        if (tower.GetComponent<TowerController>().getTowerLife() <= 0)
        {
            gameover = true;
            tower.GetComponent<TowerController>().death();

        }
        if(isMove && !gameover)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, speed * Time.deltaTime);
            if(player.transform.position == target)
            {
                isMove = false;
            }
        }
    }
    //玩家攻击
    public void PlayerAttack()
    {
        //播放攻击动画
        player.GetComponent<Animator>().SetTrigger("shoot");
        //如果玩家在塔的范围内，塔掉血
        float dis = Vector3.Distance(tower.transform.position, player.transform.position);
        if (dis < distance)
        {
            tower.GetComponent<TowerController>().ReduceBlood();
        }
    }
    //玩家移动
    public void PlayerMove(string dir)
    {
        isMove = true;
        player.GetComponent<Animator>().SetTrigger("run");
        if(dir == "left" || dir == "right" || dir == "forword")
        {
            if (dir == "left")
            {
                player.transform.Rotate(new Vector3(0, -90, 0));
            }
            else if (dir == "right")
            {
                player.transform.Rotate(new Vector3(0, 90, 0));
            }
            if (player.transform.rotation == Quaternion.Euler(new Vector3(0, 180, 0)))
            {
                target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 2);
            }
            else if (player.transform.rotation == Quaternion.Euler(new Vector3(0, 270,0)))
            {
                target = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z);
            }
            else if (player.transform.rotation == Quaternion.Euler(new Vector3(0, 90, 0)))
            {
                target = new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z);
            }
            else if(player.transform.rotation == Quaternion.Euler(new Vector3(0, 0, 0)))
            {
                target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2);
            }
        }
        else
        {
            if (player.transform.rotation == Quaternion.Euler(new Vector3(0, 180, 0)))
            {
                target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2);
            }
            else if (player.transform.rotation == Quaternion.Euler(new Vector3(0, 270, 0)))
            {
                target = new Vector3(player.transform.position.x + 2, player.transform.position.y, player.transform.position.z);
            }
            else if (player.transform.rotation == Quaternion.Euler(new Vector3(0, 90, 0)))
            {
                target = new Vector3(player.transform.position.x - 2, player.transform.position.y, player.transform.position.z);
            }
            else if(player.transform.rotation == Quaternion.Euler(new Vector3(0, 0, 0)))
            {
                target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 2);
            }
        }
    }
    public bool GetGameOver()
    {
        return gameover;
    }
}
