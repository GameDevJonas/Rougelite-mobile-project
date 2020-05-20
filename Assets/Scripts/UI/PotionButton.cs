using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PotionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator anim;
    private Button button;
    private Player player;
    private TextMeshProUGUI potionCountText;

    void Awake()
    {
        anim = GetComponent<Animator>();
        button = GetComponent<Button>();
        player = GetComponentInParent<Player>();
        potionCountText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.potion <= 0)
        {
            button.interactable = false;
        }
        else if (player.canHeal)
        {
            button.interactable = true;
        }

        potionCountText.text = ("x" + player.potion);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button.interactable)
        {
            Debug.Log("Button is held down");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button.interactable)
        {
            Debug.Log("Button is released");
        }
    }
}
