using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LegendaryLootDrop : MonoBehaviour
{
    [SerializeField] Item item;

    private bool IsInRange;
    private int drop;
    private CharacterStat type;
    public GameObject player;
    public GameObject MenuManager;
    public GameObject RareLoot;
    public PlayerStats PlayerStats;
    public List<int> AvailableLoot = new List<int>(new int[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 });

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MenuManager = GameObject.FindGameObjectWithTag("MenuManager");
    }
    private void Update()
    {
        if (PlayerStats.EnemiesVisibleInsideLight.Value >= 1)
        {
            AvailableLoot.Remove(13);
            return;
        }
        if (PlayerStats.EnemiesVisibleOutsideLight.Value >= 1)
        {
            AvailableLoot.Remove(14);
            return;
        }
        if (PlayerStats.IgnoreUnitCollision.Value >= 1)
        {
            AvailableLoot.Remove(15);
            return;
        }
        if (PlayerStats.IgnoreKnockback.Value >= 1)
        {
            AvailableLoot.Remove(16);
            return;
        }
        if (PlayerStats.WalkAfterDodge.Value >= 1 || PlayerStats.CanDodge.Value >= 1)
        {
            AvailableLoot.Remove(17);
            return;
        }
        if (PlayerStats.ArrowKnockback.Value >= 1 || PlayerStats.HasCrossbow.Value >= 1)
        {
            AvailableLoot.Remove(18);
            return;
        }
        if (PlayerStats.SwordRangeIncreased.Value >= 1 || PlayerStats.HasSword.Value >= 1)
        {
            AvailableLoot.Remove(19);
            return;
        }
        if (PlayerStats.SwordArcIncreased.Value >= 1 || PlayerStats.HasSword.Value >= 1)
        {
            AvailableLoot.Remove(20);
            return;
        }
        if (PlayerStats.TripleArrow.Value >= 1 || PlayerStats.HasCrossbow.Value >= 1)
        {
            AvailableLoot.Remove(21);
            return;
        }
        if (PlayerStats.Power.Value >= 1)
        {
            AvailableLoot.Remove(22);
            return;
        }
        if (PlayerStats.PotsIncreaseStr.Value >= 1)
        {
            AvailableLoot.Remove(23);
            return;
        }
        if (PlayerStats.ExtraLife.Value >= 1)
        {
            AvailableLoot.Remove(24);
            return;
        }

        if (AvailableLoot.Count == 0)
        {
            Instantiate(RareLoot, transform.position, Quaternion.identity);
            Destroy(gameObject, 0);
        }

        if (IsInRange == true && AvailableLoot.Count > 0)
        {
            drop = Random.Range(12, AvailableLoot.Count);
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
