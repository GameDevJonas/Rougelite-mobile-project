using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject[] objectsToPickFrom;
    public Transform parent;
    public int gridX;
    public int gridY;
    public float gridSpacingOffset;
    public Vector3 gridOrigin = Vector3.zero;

    void Start()
    {
        SpawnGrid();
    }

    public void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, y * gridSpacingOffset, 0) + gridOrigin;
                PickAndSpawn(spawnPosition, Quaternion.identity);
            }
        }
    }

    public void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        int randomIndex = Random.Range(0, objectsToPickFrom.Length);
        GameObject clone = Instantiate(objectsToPickFrom[randomIndex], positionToSpawn, rotationToSpawn, parent);
    }
}
