using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DragMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetPosition(eventData);
    }

    private void SetPosition(PointerEventData eventData)
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();

        Vector3 globalMousePos;
        //将屏幕空间点转换为位于给定RectTransform平面上的世界空间中的位置
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rectTransform.position = globalMousePos;
        }
    }
}
