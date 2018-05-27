using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct FrameDate
{
    //方向
    public Vector3 direction;
    //距离
    public float distance;
    public FrameDate(Vector3 dir, float dis)
    {
        direction = dir;
        distance = dis;
    }
}

public class ParticlePath : MonoBehaviour
{
    //粒子系统
    public ParticleSystem particle;
    //路径指定达到的点的坐标
    public List<Vector3> points;

    //每个坐标轴的速度变化曲线
    private AnimationCurve curveX = new AnimationCurve();
    private AnimationCurve curveY = new AnimationCurve();
    private AnimationCurve curveZ = new AnimationCurve();
    //关键帧所需要保存的信息
    private Queue<FrameDate> frames = new Queue<FrameDate>();

    void Start()
    {
        if (points.Count > 1)
        {
            //设置粒子发生点
            particle.transform.position = points[0];

            //路径的总长
            float totalDistance = 0;
            for (int i = 1; i < points.Count; i++)
            {
                //与下一个点距离
                float dis = Vector3.Distance(points[i], points[i - 1]);
                //向下一个点方向
                Vector3 dir = points[i] - points[i - 1];
                dir.Normalize();
                frames.Enqueue(new FrameDate(dir, dis));

                totalDistance += dis;
            }
            float time = 0;
            while (frames.Count > 0)
            {
                //关键帧的数据
                FrameDate data = frames.Dequeue();
                //将当前时刻和坐标轴值作为关键帧添加进曲线
                curveX.AddKey(new Keyframe(time, data.direction.x, float.PositiveInfinity, float.PositiveInfinity));
                curveY.AddKey(new Keyframe(time, data.direction.y, float.PositiveInfinity, float.PositiveInfinity));
                curveZ.AddKey(new Keyframe(time, data.direction.z, float.PositiveInfinity, float.PositiveInfinity));
                //设置下一时刻的关键帧
                time += (data.distance / totalDistance);
            }
            //设置粒子随着时间变化的速度
            var velocity = particle.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.Local;
            //单位时间需要移动的长度
            float distancePerTime = totalDistance / particle.startLifetime;
            velocity.x = new ParticleSystem.MinMaxCurve(distancePerTime, curveX);
            velocity.y = new ParticleSystem.MinMaxCurve(distancePerTime, curveY);
            velocity.z = new ParticleSystem.MinMaxCurve(distancePerTime, curveZ);
        }
    }
}



