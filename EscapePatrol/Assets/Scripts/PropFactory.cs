using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropFactory : MonoBehaviour
{
    private GameObject patrol = null;                              //巡逻兵
    private List<GameObject> used = new List<GameObject>();        //正在被使用的巡逻兵
    private GameObject crystal = null;                             //水晶
    private List<GameObject> usedcrystal = new List<GameObject>();      //正在被使用的水晶
    private float range = 12;                                      //水晶生成的坐标范围
    private Vector3[] vec = new Vector3[9];                        //保存每个巡逻兵的初始位置

    public FirstSceneController sceneControler;                    //场景控制器

    public List<GameObject> GetPatrols()
    {
        int[] pos_x = { -6, 4, 13 };
        int[] pos_z = { -4, 6, -13 };
        int index = 0;
        //生成不同的巡逻兵初始位置
        for(int i=0;i < 3;i++)
        {
            for(int j=0;j < 3;j++)
            {
                vec[index] = new Vector3(pos_x[i], 0, pos_z[j]);
                index++;
            }
        }
        for(int i=0; i < 9; i++)
        {
            patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol"));
            patrol.transform.position = vec[i];
            patrol.GetComponent<PatrolData>().sign = i + 1;
            patrol.GetComponent<PatrolData>().start_position = vec[i];
            used.Add(patrol);
        }   
        return used;
    }


    public List<GameObject> GetCrystal()
    {
        for(int i=0;i<12;i++)
        {
            crystal = Instantiate(Resources.Load<GameObject>("Prefabs/Crystal"));
            float ranx = Random.Range(-range, range);
            float ranz = Random.Range(-range, range);
            crystal.transform.position = new Vector3(ranx, 0, ranz);
            usedcrystal.Add(crystal);
        }

        return usedcrystal;
    }
    public void StopPatrol()
    {
        //切换所有侦查兵的动画
        for (int i = 0; i < used.Count; i++)
        {
            used[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
