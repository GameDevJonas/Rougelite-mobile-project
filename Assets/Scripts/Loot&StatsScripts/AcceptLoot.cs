﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AcceptLoot : MonoBehaviour
{
    public GameObject player;
    public ItemDatabase ItemDatabase;
    public PlayerStats PlayerStats;
    public List<Item> Loot = new List<Item>();
    public int LootAmount;

    public GameObject AcceptButton;

    public TextMeshProUGUI itemTier;
    public TextMeshProUGUI itemTitleAndAmount;
    public TextMeshProUGUI itemLore;
    public TextMeshProUGUI itemDescription;

    // Start is called before the first frame update
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                ItemDatabase = player.GetComponentInChildren<ItemDatabase>();
                PlayerStats = player.GetComponent<PlayerStats>();
            }
        }
    }

    // Update is called once per frame
    public void GiveItem(int id, int amount)
    {
        Item item = ItemDatabase.GetItem(id);
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
        while (Loot[0].amount > 1)
        {
            LootAmount = Loot[0].amount - 1;
            PlayerStats.GiveItem(Loot[0].id);
            Loot[0].amount = LootAmount;
        }
        if (Loot[0].amount <= 1)
        {
            PlayerStats.GiveItem(Loot[0].id);
            RemoveItem(Loot[0].id);
            AcceptButton.SetActive(false);
        }
    }
    public void RemoveItem(int id)
    {
        Item item = CheckforItems(id);
        if (item != null)
        {
            Loot.Remove(item);
        }
    }
}