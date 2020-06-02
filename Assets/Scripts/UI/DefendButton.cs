using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefendButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        if (button.interactable && player.playerstats.ShieldArm.Value == 0)
        {
            //Debug.Log("Button is held down");
            player.shieldIsUp = true;
            player.StartCoroutine(player.ShieldUp());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button.interactable)
        {
            //Debug.Log("Button is released");
            player.shieldIsUp = false;
        }
    }
}
