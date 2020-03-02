using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeGenerator /*: MonoBehaviour*/ //Uncomment MonoBehaviour and make all fields public or serialize if you want to use this script out of generator
{
    int Width;
    int Height;
    bool[,] MapData;
    float chanceOfEmptySpace;
    TileBase FloorTile;
    TileBase WallTile;
    Tilemap FloorMap;
    Tilemap WallMap;

    public MazeGenerator(int mapWidth, int mapHeight, float chanceOfEmpty, Tilemap floor, Tilemap walls, TileBase floorTile, TileBase wallTile)
    {
        Width = mapWidth;
        Height = mapHeight;
        MapData = new bool[Height, Width];
        chanceOfEmptySpace = chanceOfEmpty;
        FloorMap = floor;
        WallMap = walls;
        FloorTile = floorTile;
        WallTile = wallTile;
    }

    public void NewMap()
    {        
        GenerateMaze();
        BuildMaze();
    }

    public void GenerateMaze()    
    {   
        int rMax = MapData.GetUpperBound(0);
        int cMax = MapData.GetUpperBound(1);

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {                
                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    MapData[i, j] = true;
                }
                 
                else if (i % 2 == 0 && j % 2 == 0)
                {
                    if (Random.value > chanceOfEmptySpace)
                    {                        
                        MapData[i, j] = true;

                        int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                        int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                        MapData[i + a, j + b] = true;
                    }
                }
            }
        }        
    }

    void BuildMaze()
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

    public void ClearMap()
    {
        var maps = GameObject.FindObjectsOfType(typeof(Tilemap));
        foreach (Tilemap m in maps)
            m.ClearAllTiles();
    }
}