using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class AcceptLoot : MonoBehaviour
{
    public GameObject player;
    public ItemDatabase ItemDatabase;
    public PlayerStats PlayerStats;
    public List<Item> Loot = new List<Item>();
    public int LootAmount;
    public bool itemready = true;

    public GameObject AcceptButton;
    public AudioSource Legendary;
    public AudioSource Ancient;

    public TextMeshProUGUI itemTier;
    public TextMeshProUGUI itemTitleAndAmount;
    public TextMeshProUGUI itemLore;
    public TextMeshProUGUI itemDescription;

    // Start is called before the first frame update
    void Update()
    {
        if (player == null) //finds rue and scripts after load.
        {
            player = GameObject.FindGameObjectWithTag("Player");
            ItemDatabase = player.GetComponentInChildren<ItemDatabase>();
            PlayerStats = player.GetComponent<PlayerStats>();
        }
        if (Loot[0].tier == "Legendary Loot" && Legendary.isPlaying == false && itemready)
        {
            Legendary.GetComponent<AudioSource>().Play();
            AcceptButton.GetComponent<Button>().interactable = false;
            itemready = false;
        }
        
        if (Loot[0].tier == "Ancient Loot" && Ancient.isPlaying == false && itemready)
        {
            Ancient.GetComponent<AudioSource>().Play();
            AcceptButton.GetComponent<Button>().interactable = false;
            itemready = false;
        }
        if (Legendary.isPlaying == true || Ancient.isPlaying == true)
        {
            print("not ready");
            AcceptButton.GetComponent<Button>().interactable = false;
        }
        if (Legendary.isPlaying == false && Ancient.isPlaying == false)
        {
            print("ready");
            AcceptButton.GetComponent<Button>().interactable = true;
        }
    }

    public void GiveItem(int id, int amount)
    {
        Item item = ItemDatabase.GetItem(id); //gets item from lootfound and prints information on accept button.
        item.amount = amount;
        itemTier.text = item.tier;
        if (item.amount <= 1)
        {
            itemTitleAndAmount.text = item.title;
        }
        else
        {
            itemTitleAndAmount.text = item.title + " X " + item.amount;
        }
        itemLore.text = item.lore;
        itemDescription.text = item.description;

        Loot.Add(item);

        Item itemCheck = CheckforItems(id);  
    }
    public Item CheckforItems(int id)
    {
        return Loot.Find(item => item.id == id);
    }
    
    public void LootAccepted()
    {
        int differencial = Loot[0].amount / 2;
        int oldLoot = Loot[0].amount + differencial;
        int newloot = oldLoot;

        while (oldLoot > differencial) // I don't know how this works. I just brute forced it and slammed my head against it until it did.
        {
            newloot = oldLoot - 1;
            while (oldLoot > newloot)
            {
                PlayerStats.GiveItem(Loot[0].id);
                oldLoot = newloot;
                break;
            }
        }
        Loot[0].amount = 1; //resets loot amount, so that it doesn't carry over next time loot of the same time is picked up.
        RemoveItem(Loot[0].id);
        AcceptButton.SetActive(false);
        itemready = true;
    }
    public void RemoveItem(int id)
    {
        Item item = CheckforItems(id);
        if (item != null)
        {
            Loot.Remove(item); //if item in list, remove it.
        }
    }
}
