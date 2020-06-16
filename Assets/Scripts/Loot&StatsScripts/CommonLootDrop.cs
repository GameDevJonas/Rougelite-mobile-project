using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CommonLootDrop : MonoBehaviour
{
    [SerializeField] Item item;
    private bool IsInRange;
    private bool pickup = false;
    private int drop;
    private CharacterStat type;
    public GameObject player;
    public GameObject MenuManager;
    public PlayerStats PlayerStats;

    public List<int> AvailableLoot = new List<int>(new int[] { 0, 1, 2, 3, 4 });

    public GameObject pickUpSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MenuManager = GameObject.FindGameObjectWithTag("MenuManager");
        PlayerStats = player.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (PlayerStats.PotionPotency.Value >= PlayerStats.Health.Value / 10)
        {
            AvailableLoot.Remove(4);
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
            IsInRange = false;
        }
    }
}
