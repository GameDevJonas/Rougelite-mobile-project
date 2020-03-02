using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cellurar /*: MonoBehaviour*/ //Uncomment MonoBehaviour and make all fields public or serialize if you want to use this script out of generator
{
    bool[,] MapData;
    Tilemap WallMap;
    Tilemap FloorMap;
    int DeathLimit = 4;
    int BirthLimit = 5;
    int NumberOfSteps = 3;
    int Width = 100;
    int Height = 100;
    float ChanceToStartAlive = 0.45f;
    TileBase WallTile;
    TileBase FloorTile;

    public Cellurar(int mapWidth, int mapHeight, int deathLimit, int birthLimit,
                        int numberOfSteps, float chanceToStartAlive,
                        Tilemap floor, Tilemap walls, TileBase floorTile, TileBase wallTile)
    {
        Width = mapWidth;
        Height = mapHeight;
        DeathLimit = deathLimit;
        BirthLimit = birthLimit;
        NumberOfSteps = numberOfSteps;
        ChanceToStartAlive = chanceToStartAlive;
        FloorMap = floor;
        WallMap = walls;
        FloorTile = floorTile;
        WallTile = wallTile;
    }

    public void NewMap()
    {
        MapData = new bool[Width, Height];        
        MapData = InitialiseMap(MapData);
        for (int i = 0; i < NumberOfSteps; i++)
            MapData = DoSimulationStep(MapData);
        FillMap();
    }

    public bool[,] InitialiseMap(bool[,] map)
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                if (Random.Range(0f, 1f) < ChanceToStartAlive)
                    map[x, y] = true;
        //Uncomment or comment it if you want or dont want to fill map border with walls //from here
        for (int x = 0; x < Width; x++)
        {
            map[x, 0] = true;
            map[x, Width - 1] = true;
        }

        for (int y = 0; y < Height; y++)
        {
            map[0, y] = true;
            map[Height - 1, y] = true;
        }
        //to here
        return map;
       
    }

    bool[,] DoSimulationStep(bool[,] oldMap) //Do one pass through map
    {
        bool[,] newMap = new bool[Width, Height];

        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
            {
                int neigCount = CountAliveNeighbours(oldMap, x, y);
                if (oldMap[x, y])
                {
                    if (neigCount >= DeathLimit) newMap[x, y] = true;
                    else newMap[x, y] = false;
                }
                else
                {
                    if (neigCount >= BirthLimit) newMap[x, y] = true;
                    else newMap[x, y] = false;
                }
            }
        return newMap;
    }

    int CountAliveNeighbours(bool[,] map, int x, int y)
    {
        var count = 0;

        for (int i = -1; i < 2; i++)
            for (int j = -1; j < 2; j++)
            {
                int neighbourX = x + i;
                int neighbourY = y + j;
                if (i == 0 && j == 0) continue;
                else if (neighbourX < 0 || neighbourY < 0 || neighbourX >= Width || neighbourY >= Height) count += 1;
                else if (map[neighbourX, neighbourY]) count += 1;
            }

        return count;
    }

    void FillMap()
    {
        FloorMap.BoxFill(new Vector3Int(Width - 1, Height - 1, 0), FloorTile, 0, 0, Width, Height);
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                if (MapData[x, y])
                {
                    FloorMap.SetTile(new Vector3Int(x, y, 0), null);
                    WallMap.SetTile(new Vector3Int(x, y, 0), WallTile);
                }
    }

    void ClearMap()
    {
        var maps = GameObject.FindObjectsOfType(typeof(Tilemap));
        foreach (Tilemap m in maps)
            m.ClearAllTiles();
    }
}
