using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public PropFactory patrol_factory;                               //巡逻者工厂
    public ScoreRecorder recorder;                                   //记录员
    public PatrolActionManager action_manager;                       //运动管理器
    public int wall_sign = -1;                                       //当前玩家所处哪个格子
    public GameObject player;                                        //玩家
    public Camera main_camera;                                       //主相机
    public float player_speed = 5;                                   //玩家移动速度
    public float rotate_speed = 135f;                                //玩家旋转速度
    private List<GameObject> patrols;                                //场景中巡逻者列表
    private List<GameObject> crystals;                               //场景水晶列表
    private bool game_over = false;                                  //游戏结束

    void Update()
    {
        for (int i = 0; i < patrols.Count; i++)
        {
            patrols[i].gameObject.GetComponent<PatrolData>().wall_sign = wall_sign;
        }
        //水晶收集完毕
        if(recorder.GetCrystalNumber() == 0)
        {
            Gameover();
        }
    }
    void Start()
    {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        patrol_factory = Singleton<PropFactory>.Instance;
        action_manager = gameObject.AddComponent<PatrolActionManager>() as PatrolActionManager;
        LoadResources();
        main_camera.GetComponent<CameraFlow>().follow = player;
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    public void LoadResources()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 9, 0), Quaternion.identity) as GameObject;
        crystals = patrol_factory.GetCrystal();
        patrols = patrol_factory.GetPatrols();
        //所有侦察兵移动
        for (int i = 0; i < patrols.Count; i++)
        {
            action_manager.GoPatrol(patrols[i]);
        }
    }
    //玩家移动
    public void MovePlayer(float translationX, float translationZ)
    {
        if(!game_over)
        {
            if (translationX != 0 || translationZ != 0)
            {
                player.GetComponent<Animator>().SetBool("run", true);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("run", false);
            }
            //移动和旋转
            player.transform.Translate(0, 0, translationZ * player_speed * Time.deltaTime);
            player.transform.Rotate(0, translationX * rotate_speed * Time.deltaTime, 0);
            //防止碰撞带来的移动
            if (player.transform.localEulerAngles.x != 0 || player.transform.localEulerAngles.z != 0)
            {
                player.transform.localEulerAngles = new Vector3(0, player.transform.localEulerAngles.y, 0);
            }
            if (player.transform.position.y != 0)
            {
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }     
        }
    }

    public int GetScore()
    {
        return recorder.GetScore();
    }

    public int GetCrystalNumber()
    {
        return recorder.GetCrystalNumber();
    }
    public bool GetGameover()
    {
        return game_over;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Scenes/mySence");
    }

    void OnEnable()
    {
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameoverChange += Gameover;
        GameEventManager.CrystalChange += ReduceCrystalNumber;
    }
    void OnDisable()
    {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameoverChange -= Gameover;
        GameEventManager.CrystalChange -= ReduceCrystalNumber;
    }
    void ReduceCrystalNumber()
    {
        recorder.ReduceCrystal();
    }
    void AddScore()
    {
        recorder.AddScore();
    }
    void Gameover()
    {
        game_over = true;
        patrol_factory.StopPatrol();
        action_manager.DestroyAllAction();
    }
}
