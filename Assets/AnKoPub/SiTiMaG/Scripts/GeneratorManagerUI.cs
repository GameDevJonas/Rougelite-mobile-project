using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
[CustomEditor(typeof(GeneratorManager))]
[CanEditMultipleObjects]
public class GeneratorManagerUI : Editor
{
    bool buildMap;
    bool clearMap;
    GeneratorManager manager;

    public override void OnInspectorGUI()
    {
        manager = (GeneratorManager)target;        
        manager.generatorIndex = EditorGUILayout.Popup("Generator type", manager.generatorIndex, manager.mapGenerators);
        manager.width = EditorGUILayout.IntField("Map width", manager.width);
        manager.height = EditorGUILayout.IntField("Map height", manager.height);
        manager.chanceOfDecorate = EditorGUILayout.FloatField("Chance of decorate", manager.chanceOfDecorate);
        manager.floor = (Tilemap)EditorGUILayout.ObjectField("Tilemap for floor", manager.floor, typeof(Tilemap), true);
        manager.wall = (Tilemap)EditorGUILayout.ObjectField("Tilemap for walls", manager.wall, typeof(Tilemap), true);
        manager.decoration = (Tilemap)EditorGUILayout.ObjectField("Tilemap for decoration", manager.decoration, typeof(Tilemap), true);
        manager.floorTile = (TileBase)EditorGUILayout.ObjectField("Tile for floor", manager.floorTile, typeof(TileBase), false);
        manager.wallTile = (TileBase)EditorGUILayout.ObjectField("Tile for walls", manager.wallTile, typeof(TileBase), false);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("decorations"), true);
        serializedObject.ApplyModifiedProperties();

        if (clearMap) manager.ClearAllMaps();

        switch (manager.generatorIndex)
        {
            case 0:
                TunnelingUI();
                break;
            case 1:
                CellularUI();
                break;
            case 2:
                BSPGenUI();
                break;
            case 3:
                MazeUI();
                break;
        }         
        
        GUILayout.BeginHorizontal();
        buildMap = GUILayout.Button("Generate new map");
        clearMap = GUILayout.Button("Clear map");
        GUILayout.EndHorizontal();
    }

    void TunnelingUI()
    {
        manager.maxTunnels = EditorGUILayout.IntField("Max tunnels count", manager.maxTunnels);
        manager.maxTunnelLength = EditorGUILayout.IntField("Max tunnel length", manager.maxTunnelLength);
        manager.minTunnelLength = EditorGUILayout.IntField("Min tunnel length", manager.minTunnelLength);
        manager.tunnelWidth = EditorGUILayout.IntField("Tunnel width", manager.tunnelWidth);
        manager.maxRRoomSize = EditorGUILayout.IntField("Max rectangle room size", manager.maxRRoomSize);
        manager.minRRoomSize = EditorGUILayout.IntField("Min rectangle room size", manager.minRRoomSize);
        manager.maxCRoomRadius = EditorGUILayout.IntField("Max circle room radius", manager.maxCRoomRadius);
        manager.minCRoomRadius = EditorGUILayout.IntField("Min circle room radius", manager.minCRoomRadius);
        manager.buildRectRoom = EditorGUILayout.Toggle("Build rectangle rooms", manager.buildRectRoom);
        manager.buildCircleRoom = EditorGUILayout.Toggle("Build circle rooms", manager.buildCircleRoom);
        manager.randomTunnelWidth = EditorGUILayout.Toggle("Randomize tunnel width", manager.randomTunnelWidth);

        if (buildMap)
        {
            manager.ClearAllMaps();
            manager.GenerateNewMap("Tunneling");
        }
    }

    void CellularUI()
    {
        manager.deathLimit = EditorGUILayout.IntField("Neighbour cells count to stay dead", manager.deathLimit);
        manager.birthLimit = EditorGUILayout.IntField("Neighbour cells count to stay alive", manager.birthLimit);
        manager.numberOfSteps = EditorGUILayout.IntField("Number of building steps", manager.numberOfSteps);
        manager.chanceToStartAlive = EditorGUILayout.FloatField("Cell chance to start alive", manager.chanceToStartAlive);

        if (buildMap)
        {
            manager.ClearAllMaps();
            manager.GenerateNewMap("CellularAutomata");
        }
    }

    void BSPGenUI()
    {
        manager.maxLeafSize = EditorGUILayout.IntField("Max room area size", manager.maxLeafSize);
        manager.minLeafSize = EditorGUILayout.IntField("Min room area size", manager.minLeafSize);
        manager.hallsWidth = EditorGUILayout.IntField("Halls width", manager.hallsWidth);
        manager.randomHallWidth = EditorGUILayout.Toggle("Randomize hall width", manager.randomHallWidth);
        if (buildMap)
        {
            manager.ClearAllMaps();
            manager.GenerateNewMap("BSP");
        }
    }

    void MazeUI()
    {
        manager.chanceOfEmptySpace = EditorGUILayout.FloatField("Chance of empty space", manager.chanceOfEmptySpace);
        if (buildMap)
        {
            manager.ClearAllMaps();
            manager.GenerateNewMap("Maze");            
        }
    }
}
