using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonSpawnPoint : MonoBehaviour
{
    public int openingDir;
    // 1 needs top door
    // 2 needs bottom door
    // 3 needs right door
    // 4 needs left door

    public bool spawned = false;

    private RoomManager templates;
    private GameObject daGrid;

    private int rand;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomManager>();
        daGrid = GameObject.FindGameObjectWithTag("RoomManager");
        Invoke("Spawn", 0);
    }

    // Update is called once per frame
    void Spawn()
    {
        if (!spawned)
        {
            if (openingDir == 1)
            {

                //need a room with top door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDir == 2)
            {
                //need a room with a bottom door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Tilemap.Instantiate(templates.bottomRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDir == 3)
            {
                //need a room with a right door
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, Quaternion.identity);
            }
            else if (openingDir == 4)
            {
                //need a room with a left door
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DungeonSpawnPoint spawner = collision.gameObject.GetComponent<DungeonSpawnPoint>();
        if (spawner != null)
        {
            if (collision.CompareTag("SpawnPoint"))
            {
                if (collision.GetComponent<DungeonSpawnPoint>().spawned == false && spawned == false)
                {
                    Instantiate(templates.closedRooms[0], transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                spawned = true;
            }
        }
    }
}
