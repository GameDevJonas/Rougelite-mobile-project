using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Demogen : MonoBehaviour
{
    public Tilemap tilemapFloor;
    public Tilemap tilemapWall;
    public TileBase floor;
    public TileBase wall;
    void Start()
    {
        if (tilemapFloor == null || tilemapWall == null) Debug.Log("No tilemaps!!!");
        else
        {
            var bsp = new BSPGenerator(40, 40, 15, 9, 1, false, tilemapFloor, tilemapWall, floor, wall); //Generate new BSP map. It's just for demo, you can set any map you want
            bsp.NewMap();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ClearMap();
            var bsp = new BSPGenerator(40, 40, 15, 9, 1, true, tilemapFloor, tilemapWall, floor, wall); 
            bsp.NewMap();
        }
    }

    void ClearMap()
    {
        var maps = GameObject.FindObjectsOfType(typeof(Tilemap));
        foreach (Tilemap m in maps)
            m.ClearAllTiles();
    }
}
