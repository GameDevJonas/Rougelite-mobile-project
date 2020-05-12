using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour
{
    public Texture2D tex;
    [HideInInspector]
    public Vector2 gridPos;
    public int type; // 0: normal, 1: enter
    [HideInInspector]
    public bool doorTop, doorBot, doorLeft, doorRight;
    [SerializeField]
    public GameObject doorU, doorD, doorL, doorR, doorUpWall, doorOtherWall;
    [SerializeField]
    ColorToGameObject[] mappings;
    float tileSize = 16;
    Vector2 roomSizeInTiles = new Vector2(9, 17);

    public GameObject[] enemySpawns;

    public LevelGeneration theGen;

    private void Start()
    {
        theGen = FindObjectOfType<LevelGeneration>();
        theGen.roomList.Add(gameObject);
        enemySpawns = theGen.enemySpawns;
        //Instantiate(enemySpawns[Random.Range(0, enemySpawns.Length)], transform.position, Quaternion.identity, transform);
    }

    public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight)
    {
        tex = _tex;
        gridPos = _gridPos;
        type = _type;
        doorTop = _doorTop;
        doorBot = _doorBot;
        doorLeft = _doorLeft;
        doorRight = _doorRight;
        MakeDoors();
        GenerateRoomTiles();
    }
    void MakeDoors()
    {
        //top door, get position then spawn
        Vector3 spawnPos = transform.position + Vector3.up * (roomSizeInTiles.y / 4 * tileSize) - Vector3.up * (tileSize / 4);
        PlaceDoor(spawnPos, doorTop, doorU, "top");
        //bottom door
        spawnPos = transform.position + Vector3.down * (roomSizeInTiles.y / 4 * tileSize) - Vector3.down * (tileSize / 4);
        PlaceDoor(spawnPos, doorBot, doorD, "bot");
        //right door
        spawnPos = transform.position + Vector3.right * (roomSizeInTiles.x * tileSize) - Vector3.right * (tileSize);
        PlaceDoor(spawnPos, doorRight, doorR, "right");
        //left door
        spawnPos = transform.position + Vector3.left * (roomSizeInTiles.x * tileSize) - Vector3.left * (tileSize);
        PlaceDoor(spawnPos, doorLeft, doorL, "left");
    }
    void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn, string dir)
    {
        Debug.Log(dir + " " + door, gameObject);

        // check whether its a door or wall, then spawn
        if (door)
        {
            Instantiate(doorSpawn, spawnPos, Quaternion.identity).transform.parent = transform;
        }
        else
        {
            if(dir == "top")
            {
                Instantiate(doorUpWall, spawnPos, Quaternion.identity).transform.parent = transform;
            }
            else
            {
                Instantiate(doorOtherWall, spawnPos, Quaternion.identity).transform.parent = transform;
            }
            /*else if(dir == "bot")
            {
                //spawn bottom wall
            }
            else if(dir == "right")
            {
                //spawn right wall
            }
            else if(dir == "left")
            {
                //spawn left wall
            }*/

            //Instantiate(doorWall, spawnPos, Quaternion.identity).transform.parent = transform;
        }
    }
    void GenerateRoomTiles()
    {
        //loop through every pixel of the texture
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }
    void GenerateTile(int x, int y)
    {
        Color pixelColor = tex.GetPixel(x, y);
        //skip clear spaces in texture
        if (pixelColor.a == 0)
        {
            return;
        }
        //find the color to math the pixel
        foreach (ColorToGameObject mapping in mappings)
        {
            if (mapping.color.Equals(pixelColor))
            {
                Vector3 spawnPos = positionFromTileGrid(x, y);
                Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform;
            }
            else if (pixelColor.a != 0)
            {
                //forgot to remove the old print for the tutorial lol so I'll leave it here too
                //print(mapping.color + ", " + pixelColor);
            }
        }
    }
    Vector3 positionFromTileGrid(int x, int y)
    {
        Vector3 ret;
        //find difference between the corner of the texture and the center of this object
        Vector3 offset = new Vector3((-roomSizeInTiles.x + 1) * tileSize, (roomSizeInTiles.y / 4) * tileSize - (tileSize / 4), 0);
        //find scaled up position at the offset
        ret = new Vector3(tileSize * (float)x, -tileSize * (float)y, 0) + offset + transform.position;
        return ret;
    }
}
