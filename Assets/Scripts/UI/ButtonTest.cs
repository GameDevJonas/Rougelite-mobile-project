using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTest : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("I am clicked", gameObject);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Started Drag", gameObject);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Doing Drag", gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Ended Drag");
    }
}