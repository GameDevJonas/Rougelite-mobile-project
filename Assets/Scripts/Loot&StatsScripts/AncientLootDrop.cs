using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AncientLootDrop : MonoBehaviour
{
    [SerializeField] Item item;

    private bool IsInRange;
    private bool pickup = false;
    private int drop;
    private CharacterStat type;
    public GameObject player;
    public GameObject MenuManager;
    public GameObject LegendaryLoot;
    public PlayerStats PlayerStats;
    public LootFound LootFound;
    public AcceptLoot AcceptLoot;
    public List<int> AvailableLoot = new List<int>(new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 }); //list of database id's

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MenuManager = GameObject.FindGameObjectWithTag("MenuManager");
        PlayerStats = player.GetComponent<PlayerStats>();
        LootFound = MenuManager.GetComponent<LootFound>();
        AcceptLoot = MenuManager.GetComponent<AcceptLoot>();
    }
    private void Update()
    {

        if (PlayerStats.ShieldReflectsDmg.Value >= 1 || PlayerStats.ShieldArm.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(25)) || AcceptLoot.Loot.Any(des => des.id.Equals(25))) //if already found or debuff makes it useless, remove from table
        {
            AvailableLoot.Remove(25);
        }

        if (PlayerStats.DropGarantueed.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(26)) || AcceptLoot.Loot.Any(des => des.id.Equals(26)))
        {
            AvailableLoot.Remove(26);
        }

        if (PlayerStats.RueHPDmgOnHit.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(27)) || AcceptLoot.Loot.Any(des => des.id.Equals(27)))
        {
            AvailableLoot.Remove(27);
        }

        if (PlayerStats.IncreasedLifeOnHit.Value >= 1 || PlayerStats.LifeOnHit.Value < 1 
            || LootFound.Loot.Any(des => des.id.Equals(28)) || AcceptLoot.Loot.Any(des => des.id.Equals(28)))
        {
            AvailableLoot.Remove(28);
        }

        if (PlayerStats.FireArrows.Value >= 1 || PlayerStats.HasCrossbow.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(29)) || AcceptLoot.Loot.Any(des => des.id.Equals(29)))
        {
            AvailableLoot.Remove(29);
        }

        if (PlayerStats.SwordProjectile.Value >= 1 || PlayerStats.HasSword.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(30)) || AcceptLoot.Loot.Any(des => des.id.Equals(30)))
        {
            AvailableLoot.Remove(30);
        }

        if (PlayerStats.RapidFire.Value >= 1 || PlayerStats.HasCrossbow.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(31)) || AcceptLoot.Loot.Any(des => des.id.Equals(31)))
        {
            AvailableLoot.Remove(31);
        }

        if (PlayerStats.SwordExecute.Value >= 1 || PlayerStats.HasSword.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(32)) || AcceptLoot.Loot.Any(des => des.id.Equals(32)))
        {
            AvailableLoot.Remove(32);
        }

        if (PlayerStats.NoSacrifice.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(33)) || AcceptLoot.Loot.Any(des => des.id.Equals(33)))
        {
            AvailableLoot.Remove(33);
        }

        if (PlayerStats.PercentHpDmg.Value >= 1 
            || LootFound.Loot.Any(des => des.id.Equals(34)) || AcceptLoot.Loot.Any(des => des.id.Equals(34)))
        {
            AvailableLoot.Remove(34);
        }


        if (AvailableLoot.Count == 0) //if nothing available, spawn another item one tier lower.
        {
            Instantiate(LegendaryLoot, transform.position, Quaternion.identity);
            Destroy(gameObject, 0);
        }

        if (IsInRange == true && AvailableLoot.Count > 0)
        {
            drop = AvailableLoot[Random.Range(0, AvailableLoot.Count)]; //pull a random range based on the list items left.
            GiveItem(); //and give item.
        }
    }
    private void GiveItem()
    {
        MenuManager.GetComponent<LootFound>().GiveItem(drop); //send to loot found script.
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
