using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RareLootDrop : MonoBehaviour
{
    [SerializeField] Item item;

    private bool IsInRange;
    private int drop;
    private bool pickup;
    private CharacterStat type;
    public GameObject player;
    public GameObject MenuManager;
    public PlayerStats PlayerStats;
    public List<int> AvailableLoot = new List<int>(new int[] { 5, 6, 7, 8, 9, 10, 11, 12 });

    public GameObject pickUpSound;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MenuManager = GameObject.FindGameObjectWithTag("MenuManager");
        PlayerStats = player.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (PlayerStats.HasSword.Value >= 1)
        {
            AvailableLoot.Remove(8);
        }

        if (PlayerStats.HasCrossbow.Value >= 1)
        {
            AvailableLoot.Remove(9);
        }
       
        if (PlayerStats.MovementSpeed.Value >= 15)
        {
            AvailableLoot.Remove(10);
        }

        if (PlayerStats.CritChance.Value >= 100)
        {
            AvailableLoot.Remove(11);
        }

        if (PlayerStats.CritDamage.Value >= (PlayerStats.CritChance.Value * 2.5f) + 200 && PlayerStats.CritChance.Value != 100)
        {
            AvailableLoot.Remove(12);
        }


        if (IsInRange == true)
        {
            drop = AvailableLoot[Random.Range(0, AvailableLoot.Count)];
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
        if (collider.gameObject.tag == "Player" && !pickup)
        {
            pickup = true;
            IsInRange = true;
            GameObject sfx = Instantiate(pickUpSound, transform.position, Quaternion.identity);
            Destroy(sfx, .5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            pickup = false;
            IsInRange = false;
        }
    }
}
