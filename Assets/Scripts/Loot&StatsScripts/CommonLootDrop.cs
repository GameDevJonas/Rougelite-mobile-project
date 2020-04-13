using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CommonLootDrop : MonoBehaviour
{
    [SerializeField] Item item;
    private bool IsInRange;
    private int drop;
    private CharacterStat type;
    private float value;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Inventory>();
    }
    private void Update()
    {
        if (IsInRange == true)
        {
            drop = Random.Range(0, 2);
            GiveItem();
        }
    }
    private void GiveItem()
    {
        player.GetComponent<Inventory>().GiveItem(drop);
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
