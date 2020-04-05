using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestal : MonoBehaviour
{
    public Transform spawnPoint;

    public ItemPool pool;

    public static bool spawned;

    void Start()
    {
        spawned = false;
        pool = GameObject.FindGameObjectWithTag("ItemPool").GetComponent<ItemPool>();
        spawnPoint = GameObject.FindGameObjectWithTag("PedestalSpawn").transform;

        pool.ChooseItem();
        Instantiate(pool.itemToSpawn, spawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
