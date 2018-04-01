using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mygame;
public class Controllor : MonoBehaviour, ISceneController, IUserAction
{
    public LandModel start_land;            //开始陆地
    public LandModel end_land;              //结束陆地
    public BoatModel boat;                  //船
    private RoleModel[] roles;              //角色
    ViewUI view;

    void Start ()
    {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        view = gameObject.AddComponent<ViewUI>() as ViewUI;
        LoadResources();
    }
	
    public void LoadResources()              //创建水，陆地，角色，船
    {
        GameObject water = Instantiate(Resources.Load("Water", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
        water.name = "water";       
        start_land = new LandModel("start");
        end_land = new LandModel("end");
        boat = new BoatModel();
        roles = new RoleModel[6];

        for (int i = 0; i < 3; i++)
        {
            RoleModel role = new RoleModel("priest");
            role.SetName("priest" + i);
            role.SetPosition(start_land.GetEmptyPosition());
            role.GoLand(start_land);
            start_land.AddRole(role);
            roles[i] = role;
        }

        for (int i = 0; i < 3; i++)
        {
            RoleModel role = new RoleModel("devil");
            role.SetName("devil" + i);
            role.SetPosition(start_land.GetEmptyPosition());
            role.GoLand(start_land);
            start_land.AddRole(role);
            roles[i + 3] = role;
        }
    }

    public void MoveBoat()                  //移动船
    {
        if (boat.IsEmpty() || view.sign != 0) return;
        boat.BoatMove();
        view.sign = Check();
    }

    public void MoveRole(RoleModel role)    //移动角色
    {
        if (view.sign != 0) return;
        if (role.IsOnBoat())
        {
            LandModel land;
            if (boat.GetBoatSign() == -1)
                land = end_land;
            else
                land = start_land;
            boat.DeleteRoleByName(role.GetName());
            role.Move(land.GetEmptyPosition());
            role.GoLand(land);
            land.AddRole(role);
        }
        else
        {                                
            LandModel land = role.GetLandModel();
            if (boat.GetEmptyNumber() == -1 || land.GetLandSign() != boat.GetBoatSign()) return;   //船没有空位，也不是船停靠的陆地，就不上船

            land.DeleteRoleByName(role.GetName());
            role.Move(boat.GetEmptyPosition());
            role.GoBoat(boat);
            boat.AddRole(role);
        }
        view.sign = Check();
    }

    public void Restart()
    {
        start_land.Reset();
        end_land.Reset();
        boat.Reset();
        for (int i = 0; i < roles.Length; i++)
        {
            roles[i].Reset();
        }
    }

    int Check()
    {
        int start_priest = (start_land.GetRoleNum())[0];
        int start_devil = (start_land.GetRoleNum())[1];
        int end_priest = (end_land.GetRoleNum())[0];
        int end_devil = (end_land.GetRoleNum())[1];

        if (end_priest + end_devil == 6)     //获胜
            return 2;

        int[] boat_role_num = boat.GetRoleNumber();
        if (boat.GetBoatSign() == 1)         //在开始岸和船上的角色
        {
            start_priest += boat_role_num[0];
            start_devil += boat_role_num[1];
        }
        else                                  //在结束岸和船上的角色
        {
            end_priest += boat_role_num[0];
            end_devil += boat_role_num[1];
        }
        if (start_priest > 0 && start_priest < start_devil) //失败
        {      
            return 1;
        }
        if (end_priest > 0 && end_priest < end_devil)        //失败
        {
            return 1;
        }
        return 0;                                             //未完成
    }
}

