using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AncientLootDrop : MonoBehaviour
{
    [SerializeField] Item item;

    private bool IsInRange;
    private int drop;
    private CharacterStat type;
    public GameObject player;
    public GameObject MenuManager;
    public GameObject LegendaryLoot;
    public PlayerStats PlayerStats;
    public List<int> AvailableLoot = new List<int>(new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 });

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MenuManager = GameObject.FindGameObjectWithTag("MenuManager");
    }
    private void Update()
    {
        if (PlayerStats.ShieldReflectsDmg.Value >= 1 || PlayerStats.ShieldArm.Value >= 1)
        {
            AvailableLoot.Remove(25);
            return;
        }

        if (PlayerStats.DropGarantueed.Value >= 1)
        {
            AvailableLoot.Remove(26);
            return;
        }

        if (PlayerStats.RueHPDmgOnHit.Value >= 1)
        {
            AvailableLoot.Remove(27);
            return;
        }

        if (PlayerStats.IncreasedLifeOnHit.Value >= 1)
        {
            AvailableLoot.Remove(28);
            return;
        }

        if (PlayerStats.FireArrows.Value >= 1 || PlayerStats.HasCrossbow.Value >= 1)
        {
            AvailableLoot.Remove(29);
            return;
        }

        if (PlayerStats.SwordProjectile.Value >= 1 || PlayerStats.HasSword.Value >= 1)
        {
            AvailableLoot.Remove(30);
            return;
        }

        if (PlayerStats.RapidFire.Value >= 1 || PlayerStats.HasCrossbow.Value >= 1)
        {
            AvailableLoot.Remove(31);
            return;
        }

        if (PlayerStats.SwordExecute.Value >= 1 || PlayerStats.HasSword.Value >= 1)
        {
            AvailableLoot.Remove(32);
            return;
        }

        if (PlayerStats.NoSacrifice.Value >= 1)
        {
            AvailableLoot.Remove(33);
            return;
        }

        if (PlayerStats.PercentHpDmg.Value >= 1)
        {
            AvailableLoot.Remove(34);
            return;
        }
        

        if (AvailableLoot.Count <= 0)
        {
            Instantiate(LegendaryLoot, transform.position, Quaternion.identity);
        }

        if (IsInRange == true && AvailableLoot.Count > 0)
        {
            drop = Random.Range(24, AvailableLoot.Count);
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
