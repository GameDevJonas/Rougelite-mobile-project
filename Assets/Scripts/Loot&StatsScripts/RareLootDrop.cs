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
    public GameObject MenuManager;
    public PlayerStats PlayerStats;
    public List<int> AvailableLoot = new List<int>(new int[] { 6, 7, 8, 9, 10, 11, 12 });

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MenuManager = GameObject.FindGameObjectWithTag("MenuManager");
    }
    private void Update()
    {
        if (PlayerStats.HasSword.Value >= 1)
        {
            AvailableLoot.Remove(8);
            return;
        }

        if (PlayerStats.HasCrossbow.Value >= 1)
        {
            AvailableLoot.Remove(9);
            return;
        }
       
        if (PlayerStats.MovementSpeed.Value >= 15)
        {
            AvailableLoot.Remove(10);
            return;
        }

        if (PlayerStats.CritChance.Value >= 100)
        {
            AvailableLoot.Remove(11);
            return;
        }


        if (IsInRange == true)
        {
            drop = Random.Range(5, AvailableLoot.Count);
            GiveItem();
        }
    }
    private void GiveItem()
    {
        MenuManager.GetComponent<LootFound>().GiveItem(drop);
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
