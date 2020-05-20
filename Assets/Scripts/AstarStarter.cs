using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AstarStarter : MonoBehaviour
{
    public AstarPath thePath;


    void Start()
    {
        thePath = GetComponent<AstarPath>();
        Invoke("DoTheScan", .1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoTheScan(GameObject newRoom)
    {
        Debug.Log("Scanned room");
        thePath.data.gridGraph.center = newRoom.transform.position;
        thePath.Scan();
    }
}
