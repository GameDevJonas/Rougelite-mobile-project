using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Button button;

    private Player player;

    void Awake()
    {
        button = GetComponent<Button>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!player.attacking)
        {
            //Debug.Log("Button is held down");
            player.attacking = true;
            player.StartCoroutine(player.DoAnAttack());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button.interactable)
        {
            player.attacking = false;
            //Debug.Log("Button is released");
        }
    }
}
