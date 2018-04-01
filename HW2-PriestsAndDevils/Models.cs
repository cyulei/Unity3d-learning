using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mygame
{

    public interface ISceneController                      //加载场景
    {
        void LoadResources();
    }
    public interface IUserAction                           //用户互动会发生的事件
    {
        void MoveBoat();                                   //移动船
        void Restart();                                    //重新开始
        void MoveRole(RoleModel role);                     //移动角色
    }

    public class SSDirector : System.Object
    {
        private static SSDirector _instance;
        public ISceneController CurrentScenceController { get; set; }
        public static SSDirector GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SSDirector();
            }
            return _instance;
        }
    }

    public class LandModel
    {
        //只能在初始化时候赋值
        GameObject land;                                //陆地对象
        Vector3[] positions;                            //保存每个角色放在陆地上的位置
        int land_sign;                                  //到达陆地标志为-1，开始陆地标志为1
        RoleModel[] roles = new RoleModel[6];           //陆地上有的角色

        public LandModel(string land_mark)
        {
            positions = new Vector3[] {new Vector3(46F,14.73F,-4), new Vector3(55,14.73F,-4), new Vector3(64F,14.73F,-4),
                new Vector3(73F,14.73F,-4), new Vector3(82F,14.73F,-4), new Vector3(91F,14.73F,-4)};
            if (land_mark == "start")
            {
                land = Object.Instantiate(Resources.Load("Land", typeof(GameObject)), new Vector3(70, 1, 0), Quaternion.identity, null) as GameObject;
                land_sign = 1;
            }
            else
            {
                land = Object.Instantiate(Resources.Load("Land", typeof(GameObject)), new Vector3(-70, 1, 0), Quaternion.identity, null) as GameObject;
                land_sign = -1;
            }
        }

        public int GetEmptyNumber()                      //得到陆地上哪一个位置是空的
        {
            for (int i = 0; i < roles.Length; i++)
            {
                if (roles[i] == null)
                    return i;
            }
            return -1;
        }

        public int GetLandSign() { return land_sign; }

        public Vector3 GetEmptyPosition()               //得到陆地上空位置
        {
            Vector3 pos = positions[GetEmptyNumber()];
            pos.x = land_sign * pos.x;                  //因为两个陆地是x坐标对称
            return pos;
        }

        public void AddRole(RoleModel role)             
        {
            roles[GetEmptyNumber()] = role;
        }

        public RoleModel DeleteRoleByName(string role_name)      //离开陆地
        { 
            for (int i = 0; i < roles.Length; i++)
            {
                if (roles[i] != null && roles[i].GetName() == role_name)
                {
                    RoleModel role = roles[i];
                    roles[i] = null;
                    return role;
                }
            }
            return null;
        }

        public int[] GetRoleNum()
        {
            int[] count = { 0, 0 };                    //count[0]是牧师数，count[1]是魔鬼数
            for (int i = 0; i < roles.Length; i++)
            {
                if (roles[i] != null)
                {
                    if (roles[i].GetSign() == 0)
                        count[0]++;
                    else
                        count[1]++;
                }
            }
            return count;
        }

        public void Reset()
        {
            roles = new RoleModel[6];
        }
    }

    public class BoatModel
    {
        GameObject boat;                                          
        Vector3[] start_empty_pos;                                    //船在开始陆地的空位位置
        Vector3[] end_empty_pos;                                      //船在结束陆地的空位位置
        Move move;
        Click click;
        int boat_sign = 1;                                                     //船在开始还是结束陆地
        RoleModel[] roles = new RoleModel[2];                                  //船上的角色

        public BoatModel()
        {
            boat = Object.Instantiate(Resources.Load("Boat", typeof(GameObject)), new Vector3(38, -3.9F, -2.9F), Quaternion.identity, null) as GameObject;
            boat.name = "boat";
            move = boat.AddComponent(typeof(Move)) as Move;
            click = boat.AddComponent(typeof(Click)) as Click;
            boat.AddComponent(typeof(Click));
            click.SetBoat(this);
            start_empty_pos = new Vector3[] { new Vector3(9.82F, -1.65F, -4), new Vector3(31, -3.4F, -4) };
            end_empty_pos = new Vector3[] { new Vector3(-29, -1.9F, -4), new Vector3(-4.7F, -3.4F, -4) };
        }

        public bool IsEmpty()
        {
            for (int i = 0; i < roles.Length; i++)
            {
                if (roles[i] != null)
                    return false;
            }
            return true;
        }

        public void BoatMove()
        {
            if (boat_sign == -1)
            {
                move.MovePosition(new Vector3(38, -3.9F, -2.9F));
                boat_sign = 1;
            }
            else
            {
                move.MovePosition(new Vector3(-1.4F, -4, -3));
                boat_sign = -1;
            }
        }

        public int GetBoatSign(){ return boat_sign;}

        public RoleModel DeleteRoleByName(string role_name)
        {
            for (int i = 0; i < roles.Length; i++)
            {
                if (roles[i] != null && roles[i].GetName() == role_name)
                {
                    RoleModel role = roles[i];
                    roles[i] = null;
                    return role;
                }
            }
            return null;
        }

        public int GetEmptyNumber()
        {
            for (int i = 0; i < roles.Length; i++)
            {
                if (roles[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public Vector3 GetEmptyPosition()
        {
            Vector3 pos;
            if (boat_sign == -1)
                pos = end_empty_pos[GetEmptyNumber()];
            else
                pos = start_empty_pos[GetEmptyNumber()];
            return pos;
        }

        public void AddRole(RoleModel role)
        {
            roles[GetEmptyNumber()] = role;
        }

        public GameObject GetBoat(){ return boat; }

        public void Reset()
        {
            if (boat_sign == -1)
                BoatMove();
            roles = new RoleModel[2];
        }

        public int[] GetRoleNumber()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < roles.Length; i++)
            {
                if (roles[i] == null)
                    continue;
                if (roles[i].GetSign() == 0)
                    count[0]++;
                else
                    count[1]++;
            }
            return count;
        }
    }

    public class RoleModel
    {
        GameObject role;
        int role_sign;             //0为牧师，1为恶魔
        Click click;
        bool on_boat;              //是否在船上       
        Move move;
        LandModel land_model = (SSDirector.GetInstance().CurrentScenceController as Controllor).start_land;

        public RoleModel(string role_name)
        {
            if (role_name == "priest")
            {
                role = Object.Instantiate(Resources.Load("Priest", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0), null) as GameObject;
                role_sign = 0;
            }
            else
            {
                role = Object.Instantiate(Resources.Load("Devil", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0), null) as GameObject;
                role_sign = 1;
            }
            move = role.AddComponent(typeof(Move)) as Move;
            click = role.AddComponent(typeof(Click)) as Click;
            click.SetRole(this);
        }

        public int GetSign() { return role_sign;}
        public LandModel GetLandModel(){return land_model;}
        public string GetName() { return role.name; }
        public bool IsOnBoat() { return on_boat; }
        public void SetName(string name) { role.name = name; }
        public void SetPosition(Vector3 pos) { role.transform.position = pos; }

        public void Move(Vector3 vec)
        {
            move.MovePosition(vec);
        }

        public void GoLand(LandModel land)
        {  
            role.transform.parent = null;
            land_model = land;
            on_boat = false;
        }

        public void GoBoat(BoatModel boat)
        {
            role.transform.parent = boat.GetBoat().transform;
            land_model = null;          
            on_boat = true;
        }

        public void Reset()
        {
            land_model = (SSDirector.GetInstance().CurrentScenceController as Controllor).start_land;
            GoLand(land_model);
            SetPosition(land_model.GetEmptyPosition());
            land_model.AddRole(this);
        }
    }

    public class Move : MonoBehaviour
    {
        float move_speed = 250;                   //移动速度
        int move_sign = 0;                        //0是不动，1水平移动，2竖直移动
        Vector3 end_pos;
        Vector3 middle_pos;

        void Update()
        {
            if (move_sign == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, middle_pos, move_speed * Time.deltaTime);
                if (transform.position == middle_pos)
                    move_sign = 2;
            }
            else if (move_sign == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, end_pos, move_speed * Time.deltaTime);
                if (transform.position == end_pos)
                    move_sign = 0;           
            }
        }
        public void MovePosition(Vector3 position)
        {
            end_pos = position;
            middle_pos = position;
            if (position.y == transform.position.y)         //船只会水平移动
            {  
                move_sign = 2;
            }
            else if (position.y < transform.position.y)      //角色从陆地到船
            { 
                middle_pos.y = transform.position.y;
            }
            else                                          //角色从船到陆地
            {                       
                middle_pos.x = transform.position.x;
            }
            move_sign = 1;
        }
    }

    public class Click : MonoBehaviour
    {
        IUserAction action;
        RoleModel role = null;
        BoatModel boat = null;
        public void SetRole(RoleModel role)
        {
            this.role = role;
        }
        public void SetBoat(BoatModel boat)
        {
            this.boat = boat;
        }
        void Start()
        {
            action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        }
        void OnMouseDown()
        {
            if (boat == null && role == null) return;
            if (boat != null)
                action.MoveBoat();
            else if(role != null)
                action.MoveRole(role);
        }
    }
}


