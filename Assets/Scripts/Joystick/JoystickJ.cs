using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickJ : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector3 defaultPos;

    public RectTransform myTransform;

    public Canvas canvas;

    public Collider2D constraints;

    private void Awake()
    {
        myTransform = GetComponent<RectTransform>();
        defaultPos = myTransform.anchoredPosition;
    }

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
        if (constraints.bounds.Contains(transform.position))
        {
            myTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Ended Drag", gameObject);
        myTransform.anchoredPosition = defaultPos;
    }
}