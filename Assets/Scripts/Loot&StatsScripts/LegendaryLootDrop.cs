using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LegendaryLootDrop : MonoBehaviour
{
    [SerializeField] Item item;

    private bool IsInRange;
    private int drop;
    private bool pickup = false;
    private CharacterStat type;
    public GameObject player;
    public GameObject MenuManager;
    public GameObject RareLoot;
    public PlayerStats PlayerStats;
    public List<int> AvailableLoot = new List<int>(new int[] { 13, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 });

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MenuManager = GameObject.FindGameObjectWithTag("MenuManager");
        PlayerStats = player.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (PlayerStats.EnemiesVisibleInsideLight.Value >= 1)
        {
            AvailableLoot.Remove(13);
        }
        if (PlayerStats.EnemiesVisibleOutsideLight.Value >= 1)
        {
            AvailableLoot.Remove(14);
        }
        if (PlayerStats.IgnoreUnitCollision.Value >= 1)
        {
            AvailableLoot.Remove(15);
        }
        if (PlayerStats.IgnoreKnockback.Value >= 1)
        {
            AvailableLoot.Remove(16);
        }
        if (PlayerStats.WalkAfterDodge.Value >= 1 || PlayerStats.CanDodge.Value >= 1)
        {
            AvailableLoot.Remove(17);
        }
        if (PlayerStats.ArrowKnockback.Value >= 1 || PlayerStats.HasCrossbow.Value >= 1)
        {
            AvailableLoot.Remove(18);
        }
        if (PlayerStats.SwordRangeIncreased.Value >= 1 || PlayerStats.HasSword.Value >= 1)
        {
            AvailableLoot.Remove(19);
        }
        if (PlayerStats.SwordArcIncreased.Value >= 1 || PlayerStats.HasSword.Value >= 1)
        {
            AvailableLoot.Remove(20);
        }
        if (PlayerStats.TripleArrow.Value >= 1 || PlayerStats.HasCrossbow.Value >= 1)
        {
            AvailableLoot.Remove(21);
        }
        if (PlayerStats.Power.Value >= 1)
        {
            AvailableLoot.Remove(22);
        }
        if (PlayerStats.PotsIncreaseStr.Value >= 1)
        {
            AvailableLoot.Remove(23);
        }
        if (PlayerStats.ExtraLife.Value >= 1)
        {
            AvailableLoot.Remove(24);
        }

        if (AvailableLoot.Count == 0)
        {
            Instantiate(RareLoot, transform.position, Quaternion.identity);
            Destroy(gameObject, 0);
        }

        if (IsInRange == true && AvailableLoot.Count > 0)
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
