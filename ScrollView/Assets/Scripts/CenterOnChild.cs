using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CenterOnChild : MonoBehaviour, IEndDragHandler, IDragHandler
{
    
    public float centerSpeed = 10f;                                  //将子物体拉到中心位置时的速度
    public GameObject panel;                                         //中心的选中框

    private ScrollRect scrollView;                                 
    private Transform container;                                     //可滚动部分的内容

    private List<float> childrenPos = new List<float>();             //保存每个子物体在中心时的位置
    private float targetPos;                                         //当前子物体应该在中心时的位置
    private bool centering = false;                             
    private bool drag = false;                                       
    private bool check = false;                                      //是否选择需要让子物体在中心位置

    void Awake()
    {
        scrollView = GetComponent<ScrollRect>();
        container = scrollView.content;
        GridLayoutGroup grid = container.GetComponent<GridLayoutGroup>();

        //计算第一个子物体位于中心时的位置
        float childPos = scrollView.GetComponent<RectTransform>().rect.width * 0.5f - grid.cellSize.x * 0.5f;
        childrenPos.Add(childPos);
        //缓存所有子物体位于中心时的位置
        for (int i = 0; i < container.childCount - 1; i++)
        {
            childPos -= grid.cellSize.x + grid.spacing.x;
            childrenPos.Add(childPos);
        }
    }

    void Update()
    {
        if (centering && Input.GetAxis("Mouse ScrollWheel")==0 && check)
        {
            //获得子物体当前位置
            Vector3 vec = container.localPosition;
            //将子物体的位置移到应该在中心时候的位置
            vec.x = Mathf.Lerp(container.localPosition.x, targetPos, centerSpeed * Time.deltaTime);
            container.localPosition = vec;
            //如果位置相等则不用再移动
            if (Mathf.Abs(container.localPosition.x - targetPos) < 0.01f)
            {
                centering = false;
            }
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //拖动结束，需要移到中心位置
        centering = true;
        drag = false;
        //找到最近的目标位置
        targetPos = FindClosestPos(container.localPosition.x);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //拖动中
        drag = true;
        centering = false;
    }

    public void ScrollbarDrag()
    {
        //不是直接拖动滑动框里面的内容
        if(!drag)
        {
            centering = true;
            targetPos = FindClosestPos(container.localPosition.x);
        }
    }

    private float FindClosestPos(float currentPos)
    {
        float closest = 0;
        float distance = Mathf.Infinity;

        for (int i = 0; i < childrenPos.Count; i++)
        {
            float pos = childrenPos[i];
            float dis = Mathf.Abs(pos - currentPos);
            //找寻距离当前位置最近的子物体中心位置
            if (dis < distance)
            {
                distance = dis;
                closest = pos;
            }
        }
        //返回需要移动到最近的位置
        return closest;
    }

    public void CheckToggle()
    {
        check = !check;
        if(check)
        {
            //当选中时需要将最近的一个子物体移到中心框里
            centering = true;
            targetPos = FindClosestPos(container.localPosition.x);
            //中心框显示
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
