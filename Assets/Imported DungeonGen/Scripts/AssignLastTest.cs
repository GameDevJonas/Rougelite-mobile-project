using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignLastTest : MonoBehaviour
{
    public GameObject bossDoor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBossDoor(GameObject room)
    {
        Debug.Log("Spawn bossdoor in " + room, room.gameObject);
        GameObject door = Instantiate(bossDoor, room.transform);
    }
}
