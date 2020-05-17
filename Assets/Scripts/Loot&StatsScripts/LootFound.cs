using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootFound : MonoBehaviour
{
    public GameObject player;
    public List<Item> Loot = new List<Item>();
    public ItemDatabase ItemDatabase;
    public GameObject AcceptObject;
    public GameObject Button;
    public MenuManager MenuManager;
    public AcceptLoot AcceptLoot => AcceptObject.GetComponent<AcceptLoot>();
    

    public bool poppedUp = false;
    // Start is called before the first frame update
    void Start()
    {
        MenuManager = gameObject.GetComponent<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                ItemDatabase = player.GetComponentInChildren<ItemDatabase>();
            }
        }
        if (Loot.Count > 0 && MenuManager.isPaused && AcceptLoot.Loot.Count == 0)
        {
            AcceptLoot.GiveItem(Loot[0].id, Loot[0].amount);
            Button.SetActive(true);
            RemoveItem(Loot[0].id);
        }
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = ItemDatabase.GetItem(id);
        //itemToAdd.amount = 1;
        bool AlreadyinInventory = false;
        foreach (Item item in Loot)
        {
            if (item.id == itemToAdd.id)
            {
                item.amount += 1;
                AlreadyinInventory = true;

                Debug.Log("Got another " + itemToAdd.title + itemToAdd.description);

                Item itemCheck = CheckforItems(id);
            }
        }
        if (!AlreadyinInventory)
        {
            Loot.Add(itemToAdd);

            Debug.Log("Got " + itemToAdd.title + itemToAdd.description);

            Item itemCheck = CheckforItems(id);
        }
    }
    public Item CheckforItems(int id)
    {
        return Loot.Find(item => item.id == id);
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
