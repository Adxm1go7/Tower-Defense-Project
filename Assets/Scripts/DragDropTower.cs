using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropTower : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin dragging tower");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging tower");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End dragging tower");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Tower Clicked");
    }

}
