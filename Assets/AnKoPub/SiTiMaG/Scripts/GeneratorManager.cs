using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratorManager : MonoBehaviour
{
    //Common generator options
    public string[] mapGenerators = { "Tunneling", "CellularAutomata", "BSP", "Maze" };
    public int generatorIndex = 0;
    public int width, height;
    public Tilemap floor, wall, decoration;
    public TileBase floorTile, wallTile;
    public float chanceOfDecorate;
    public List<TileBase> decorations;
    //Tunneling options
    public int maxTunnels, maxTunnelLength, minTunnelLength, tunnelWidth;
    public int maxRRoomSize, minRRoomSize, maxCRoomRadius, minCRoomRadius;
    public bool buildRectRoom, buildCircleRoom, randomTunnelWidth;
    //Cellular options
    public int deathLimit, birthLimit, numberOfSteps;
    public float chanceToStartAlive;
    //BSP options
    public int maxLeafSize, minLeafSize, hallsWidth;
    public bool randomHallWidth;
    //Maze options
    public float chanceOfEmptySpace;

    public void GenerateNewMap(string mapType)
    {
        switch (mapType)
        {
            case "Tunneling":
                var tunn = new Tunneling(width, height, maxTunnels, maxTunnelLength, minTunnelLength, tunnelWidth,
                                       maxRRoomSize, minRRoomSize, maxCRoomRadius, minCRoomRadius,
                                       buildRectRoom, buildCircleRoom, randomTunnelWidth,
                                       floor, wall, floorTile, wallTile);
                tunn.NewMap();
                if (decorations.Count > 0) PlaceDecorations();
                break;
            case "CellularAutomata":
                var cellular = new Cellurar(width, height, deathLimit, birthLimit, numberOfSteps,
                                           chanceToStartAlive, floor, wall, floorTile, wallTile);
                cellular.NewMap();
                if (decorations.Count > 0) PlaceDecorations();
                break;
            case "BSP":
                var bsp = new BSPGenerator(width, height, maxLeafSize, minLeafSize,
                                           hallsWidth, randomHallWidth, floor, wall, floorTile, wallTile);
                bsp.NewMap();
                if (decorations.Count > 0) PlaceDecorations();
                break;
            case "Maze":
                var maze = new MazeGenerator(width, height, chanceOfEmptySpace, floor, wall, floorTile, wallTile);
                maze.NewMap();
                if (decorations.Count > 0) PlaceDecorations();
                break;
        }
    }

    public void ClearAllMaps()
    {
        var maps = FindObjectsOfType(typeof(Tilemap));
        foreach (Tilemap m in maps)
            m.ClearAllTiles();
    }

    public void PlaceDecorations()
    {
        var decorateMap = decoration.GetComponent<Tilemap>();
        foreach (var point in GetAvailablePoints())
        {
            if (Random.Range(0f, 1f) < chanceOfDecorate) decorateMap.SetTile(point, decorations[Random.Range(0, decorations.Count)]);
        }
    }

    List<Vector3Int> GetAvailablePoints()
    {
        var availablePoints = new List<Vector3Int>();
        var floorMap = floor.GetComponent<Tilemap>();
        for (int i = floorMap.cellBounds.xMin; i <= floorMap.cellBounds.xMax; i++)
            for (int j = floorMap.cellBounds.yMin; j <= floorMap.cellBounds.yMax; j++)
            {
                Vector3Int place = new Vector3Int(i, j, (int)floorMap.transform.position.z);
                if (floorMap.HasTile(place)) availablePoints.Add(place);
            }
        return availablePoints;
    }
}
