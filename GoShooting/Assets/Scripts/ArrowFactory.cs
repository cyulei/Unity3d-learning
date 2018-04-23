using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFactory : MonoBehaviour {

    public GameObject arrow = null;                             //弓箭预制体
    private List<GameObject> used = new List<GameObject>();     //正在被使用的弓箭
    private Queue<GameObject> free = new Queue<GameObject>();   //空闲的弓箭队列
    public FirstSceneController sceneControler;                 //场景控制器
    
    public GameObject GetArrow()
    {
        if (free.Count == 0)
        {
            arrow = Instantiate(Resources.Load<GameObject>("Prefabs/arrow"));
        }
        else
        {
            arrow = free.Dequeue();
            //如果是曾经射出过的箭
            if(arrow.tag == "hit")
            {
                arrow.GetComponent<Rigidbody>().isKinematic = false;
                //箭头设置为可见
                arrow.transform.GetChild(0).gameObject.SetActive(true);
                arrow.tag = "arrow";
            }
            arrow.gameObject.SetActive(true);
        }

        sceneControler = (FirstSceneController)SSDirector.GetInstance().CurrentScenceController;
        Transform temp = sceneControler.bow.transform.GetChild(2);
        //设置新射出去的箭的位置在弓箭上
        arrow.transform.position = temp.transform.position;
        arrow.transform.parent = sceneControler.bow.transform;
        used.Add(arrow);
        return arrow;
    }

    //回收箭
    public void FreeArrow(GameObject arrow)
    {
        for (int i = 0; i < used.Count; i++)
        {
            if (arrow.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                used[i].gameObject.SetActive(false);
                free.Enqueue(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}
