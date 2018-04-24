using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
   
    public Camera child_camera;                                      //副相机
    public Camera main_camera;                                       //主相机
    public ScoreRecorder recorder;                                   //记录员
    public ArrowFactory arrow_factory;                               //箭工厂
    public ArrowFlyActionManager action_manager;                     //动作管理
    private int[] targetscore = { 15, 30, 40, 50 };                  //每一回合的目标分数
    private int round = 0;                                           //回合
    public GameObject bow;                                          //弓
    private GameObject arrow;                                        //箭                      
    private GameObject target;                                       //靶子         
    private int arrow_num = 0;                                       //每一回合射出的弓箭数
    
    private List<GameObject> arrow_queue = new List<GameObject>();   //在场景中的箭队列

    private bool game_over = false;                                  //游戏结束
    private bool game_start = false;                                 //游戏开始
    private string wind = "";                                        //风的方向和等级
    private float wind_directX;                                      //风x轴方向的力大小         
    private float wind_directY;                                      //风y轴方向的力大小

    void Start ()
    {
        SSDirector director = SSDirector.GetInstance();
        arrow_factory = Singleton<ArrowFactory>.Instance;
        recorder = Singleton<ScoreRecorder>.Instance;
        director.CurrentScenceController = this;
        action_manager = gameObject.AddComponent<ArrowFlyActionManager>() as ArrowFlyActionManager;
        LoadResources();
        main_camera.GetComponent<CameraFlow>().bow = bow;
        //初始化风的方向
        wind_directX = Random.Range(-1, 1);
        wind_directY = Random.Range(-1, 1);
        //生成风
        CreateWind();
    }
	
	void Update ()
    {
        if(game_start)
        {
            for (int i = 0; i < arrow_queue.Count; i++)
            {
                GameObject temp = arrow_queue[i];
                //场景中超过5只箭或者超出边界则回收箭
                if (temp.transform.position.z > 30 || arrow_queue.Count > 5)
                {
                    arrow_factory.FreeArrow(arrow_queue[i]);
                    arrow_queue.Remove(arrow_queue[i]);
                }
            }
        }
    }
    public void LoadResources()
    {
        bow = Instantiate(Resources.Load("Prefabs/bow", typeof(GameObject))) as GameObject;
        bow.transform.rotation = Quaternion.Euler(90f, 0.0f, 90f);
        target = Instantiate(Resources.Load("Prefabs/target", typeof(GameObject))) as GameObject;
    }
    
    public void MoveBow(float offsetX, float offsetY)
    {
        //游戏未开始时候不允许移动弓
        if (game_over || !game_start)
        {
            return;
        }
        //弓是否超出限定的移动范围
        if (bow.transform.position.x > 5)
        {
            bow.transform.position = new Vector3(5, bow.transform.position.y, bow.transform.position.z);
            return;
        }
        else if(bow.transform.position.x < -5)
        {
            bow.transform.position = new Vector3(-5, bow.transform.position.y, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.y < -3)
        {
            bow.transform.position = new Vector3(bow.transform.position.x, -3, bow.transform.position.z);
            return;
        }
        else if (bow.transform.position.y > 5)
        {
            bow.transform.position = new Vector3(bow.transform.position.x, 5, bow.transform.position.z);
            return;
        }

        //弓箭移动
        offsetY *= Time.deltaTime;
        offsetX *= Time.deltaTime;
        bow.transform.Translate(0, -offsetX, 0);
        bow.transform.Translate(0, 0, -offsetY);
    }

    public void Shoot()
    {
        if((!game_over || game_start) && arrow_num <= 10)
        {
            arrow = arrow_factory.GetArrow();
            arrow_queue.Add(arrow);
            //风方向
            Vector3 wind = new Vector3(wind_directX, wind_directY, 0);
            //动作管理器实现箭飞行
            action_manager.ArrowFly(arrow, wind);
            //副相机开启
            child_camera.GetComponent<ChildCamera>().StartShow();
            //用户能射出的箭数量减少
            recorder.arrow_number--;
            //场景中箭数量增加
            arrow_num++;
        }
    }
    //获得分数
    public int GetScore()
    {
        return recorder.score;
    }
    //获得目标分数
    public int GetTargetScore()
    {
        return recorder.target_score;
    }
    //获得剩余箭数量
    public int GetResidueNum()
    {
        return recorder.arrow_number;
    }
    //得到游戏结束标志
    public bool GetGameover()
    {
        return game_over;
    }
    //得到风的字符串
    public string GetWind()
    {
        return wind;
    }
    //重新开始
    public void Restart()
    {
        game_over = false;
        recorder.arrow_number = 10;
        recorder.score = 0;
        recorder.target_score = 15;
        round = 0;
        arrow_num = 0;
        for (int i = 0; i < arrow_queue.Count; i++)
        {
            arrow_factory.FreeArrow(arrow_queue[i]);
        }
        arrow_queue.Clear();
    }

    public void CheckGamestatus()
    {
        
        if (recorder.arrow_number <= 0 && recorder.score < recorder.target_score)
        {
            game_over = true;
            return;
        }
        else if (recorder.arrow_number <= 0 && recorder.score >= recorder.target_score)
        {
            round++;
            arrow_num = 0;
            if (round == 4)
            {
                game_over = true;
            }
            //回收所有的箭
            for (int i = 0; i < arrow_queue.Count; i++)
            {
                arrow_factory.FreeArrow(arrow_queue[i]);
            }
            arrow_queue.Clear();
            recorder.arrow_number = 10;
            recorder.score = 0;
            recorder.target_score = targetscore[round];
        }
        //生成新的风向
        wind_directX = Random.Range(-(round + 1), (round + 1));
        wind_directY = Random.Range(-(round + 1), (round + 1));
        CreateWind();
    }
    //根据风的方向生成文本
    public void CreateWind()
    {
        string Horizontal = "", Vertical = "", level = "";
        if (wind_directX > 0)
        {
            Horizontal = "西";
        }
        else if (wind_directX <= 0)
        {
            Horizontal = "东";
        }
        if (wind_directY > 0)
        {
            Vertical = "南";
        }
        else if (wind_directY <= 0)
        {
            Vertical = "北";
        }
        if ((wind_directX + wind_directY) / 2 > -1 && (wind_directX + wind_directY) / 2 < 1)
        {
            level = "1 级";
        }
        else if ((wind_directX + wind_directY) / 2 > -2 && (wind_directX + wind_directY) / 2 < 2)
        {
            level = "2 级";
        }
        else if ((wind_directX + wind_directY) / 2 > -3 && (wind_directX + wind_directY) / 2 < 3)
        {
            level = "3 级";
        }
        else if ((wind_directX + wind_directY) / 2 > -5 && (wind_directX + wind_directY) / 2 < 5)
        {
            level = "4 级";
        }

        wind = Horizontal + Vertical + "风" + " " + level;
    }
    //开始游戏
    public void BeginGame()
    {
        game_start = true;
    }
}
