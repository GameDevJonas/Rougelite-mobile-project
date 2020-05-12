using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RareLootDrop : MonoBehaviour
{
    [SerializeField] Item item;
    private bool IsInRange;
    private int drop;
    private CharacterStat type;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (IsInRange == true)
        {
            drop = Random.Range(5, 12);
            GiveItem();
        }
    }
    private void GiveItem()
    {
        player.GetComponent<PlayerStats>().GiveItem(drop);
        Destroy(gameObject, 0);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            IsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            IsInRange = false;
        }
    }
}
