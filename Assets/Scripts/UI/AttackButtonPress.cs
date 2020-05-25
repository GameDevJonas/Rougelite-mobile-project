using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator anim;
    private Button button;

    void Awake()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button.interactable)
        {
            //Debug.Log("Button is held down");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button.interactable)
        {
            //Debug.Log("Button is released");
        }
    }
}
