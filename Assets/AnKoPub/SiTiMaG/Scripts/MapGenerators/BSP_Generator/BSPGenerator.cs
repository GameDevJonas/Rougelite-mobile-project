using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BSPGenerator /*: MonoBehaviour*/ //Uncomment MonoBehaviour and make all fields public or serialize if you want to use this script out of generator
{
    TileBase WallTile;
    TileBase FloorTile;
    int Width = 50;
    int Heigth = 50;
    int MaxLeafSize = 20;
    int MinLeafSize = 6;
    int HallsWidth;
    public static bool RandomHallWidth;
    public static int MinLeafSizeStatic;
    public static int HallsWidthStatic;
    List<Node> leafsList;
    Tilemap WallMap;
    Tilemap FloorMap;

    public BSPGenerator(int mapWidth, int mapHeight, int maxLeafSize, int minLeafSize, int hallsWidth, bool randomHallWidth,
                        Tilemap floor, Tilemap walls, TileBase floorTile, TileBase wallTile)
    {
        Width = mapWidth;
        Heigth = mapHeight;
        MaxLeafSize = maxLeafSize;
        MinLeafSize = minLeafSize;
        HallsWidth = hallsWidth;
        RandomHallWidth = randomHallWidth;
        MinLeafSizeStatic = MinLeafSize;
        HallsWidthStatic = HallsWidth;
        FloorMap = floor;
        WallMap = walls;
        FloorTile = floorTile;
        WallTile = wallTile;
    }
        
    public void NewMap()
    {
        MinLeafSizeStatic = MinLeafSize;
        leafsList = new List<Node>();
        GenerateField(WallMap);
        GenerateNodes();
        BuildMap(leafsList);
    }

    void GenerateNodes()
    {
        var root = new Node(new Point(0, 0), Width, Heigth);
        leafsList.Add(root);

        bool didSplit = true;

        while (didSplit)
        {
            didSplit = false;
            for (int i = 0; i < leafsList.Count; i++)
            {
                if (leafsList[i].LeftChild == null && leafsList[i].RightChild == null)
                {
                    if (leafsList[i].Width > MaxLeafSize || leafsList[i].Height > MaxLeafSize || Random.Range(0f, 1f) > 0.25)
                    {
                        if (leafsList[i].Split())
                        {
                            leafsList.Add(leafsList[i].LeftChild);
                            leafsList.Add(leafsList[i].RightChild);
                            didSplit = true;
                        }
                    }
                }
            }
        }
        root.CreateRooms();
    }

    void BuildMap(List<Node> list)
    {
        foreach (var l in list)
        {
            for (int i = (int)l.Room.xMin; i <= l.Room.xMax; i++)
                for (int j = (int)l.Room.yMin; j <= l.Room.yMax; j++)
                {
                    if (i == 0 && j == 0) continue;
                    WallMap.SetTile(new Vector3Int(i, j, 0), null);
                    FloorMap.SetTile(new Vector3Int(i, j, 0), FloorTile);
                }

            if (l.Halls != null)
            {
                foreach (var hall in l.Halls)
                {
                    for (int i = (int)hall.xMin; i <= hall.xMax; i++)
                        for (int j = (int)hall.yMin; j <= hall.yMax; j++)
                        {
                            WallMap.SetTile(new Vector3Int(i, j, 0), null);
                            FloorMap.SetTile(new Vector3Int(i, j, 0), FloorTile);
                        }
                }
            }
        }
    }

    void GenerateField(Tilemap map) //generate a field filled with wall tiles
    {
        for (int i = 0; i <= Width; i++)
            for (int j = 0; j <= Heigth; j++)
                map.SetTile(new Vector3Int(i, j, 0), WallTile);
    }

    public void ClearMap()
    {
        var maps = GameObject.FindObjectsOfType(typeof(Tilemap));
        foreach (Tilemap m in maps)
            m.ClearAllTiles();
    }
}