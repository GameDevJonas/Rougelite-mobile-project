using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ItemPool : MonoBehaviour
{
    public GameObject itemToSpawn;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemRarity;

    public string nameItem;

    public float percentage;

    public int current;

    public ItemNo[] itemArray;

    //public List<string> poolName = new List<string>();
    public List<float> poolPercentage = new List<float>();
    public List<ItemNo> itemsInPool = new List<ItemNo>();
    void Start()
    {
        current = 0;
        foreach (ItemNo item in itemArray)
        {
            float percentageToAdd = item.myPercentage;
            current++;
            //poolName.Add(item.myName);
            poolPercentage.Add(percentageToAdd);
        }
        current = 0;
    }

    public void ChooseItem()
    {
        foreach (ItemNo item in itemArray)
        {
            if (!ItemPedestal.spawned)
            {
                current++;
                float rand = Random.Range(0, 101);
                if (rand >= item.myPercentage)
                {
                    ItemPedestal.spawned = true;
                    itemToSpawn = item.objectToSpawn;
                    itemName.text = item.myName;
                    itemRarity.text = item.rarity;
                }
            }
            if(current >= itemArray.Length)
            {
                current = 0;
                ChooseItem();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

