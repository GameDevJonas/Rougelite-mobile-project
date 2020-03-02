using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tunneling /*: MonoBehaviour*/  //Uncomment MonoBehaviour and make all fields public or serialize if you want to use this script out of generator
{    
    int Width, Height, MaxTunnels, MaxTunnelLength, MinTunnelLength, TunnelWidth,
        MaxRRoomSize, MinRRoomSize, MaxCRoomRadius, MinCRoomRadius;
    bool BuildRectRoom, BuildCircleRoom, RandomTunnWidth;
    TileBase FloorTile, WallTile;
    Tilemap FloorMap, WallMap;
    Dictionary<Vector2, bool> MapData;
    List<Rect> RectRoomsAndTunnels;         //storage for rectangle rooms and tunnels data
    List<Circle> CircleRooms;               //storage for circle rooms data

    public Tunneling(int mapWidth, int mapHeight, int maxTunnels, int maxTunnelsLength, int minTunnelLength, int tunnelWidth,
                        int maxRectRoomSize, int minRecrRoomSize, int maxCircleRoomSize, int minCircleRoomSize,
                        bool buildRectRoom, bool buildCircleRoom, bool randomTunWidth,
                        Tilemap floor, Tilemap walls, TileBase floorTile, TileBase wallTile)
    {
        Width = mapWidth;
        Height = mapHeight;
        MaxTunnels = maxTunnels;
        MaxTunnelLength = maxTunnelsLength;
        MinTunnelLength = minTunnelLength;
        TunnelWidth = tunnelWidth;
        MaxRRoomSize = maxRectRoomSize;
        MinRRoomSize = minRecrRoomSize;
        MaxCRoomRadius = maxCircleRoomSize;
        MinCRoomRadius = minCircleRoomSize;
        BuildRectRoom = buildRectRoom;
        BuildCircleRoom = buildCircleRoom;
        RandomTunnWidth = randomTunWidth;
        FloorMap = floor;
        WallMap = walls;
        FloorTile = floorTile;
        WallTile = wallTile;
    }

    public void NewMap()
    {
        MapData = new Dictionary<Vector2, bool>();
        for (int x = 0; x <= Width; x++)
            for (int y = 0; y <= Height; y++)
                MapData.Add(new Vector2Int(x, y), true);

        RectRoomsAndTunnels = new List<Rect>();
        CircleRooms = new List<Circle>();        
        DigTunnel();
        BuildMap();
    }

    void DigTunnel()
    {
        var currentPoint = new Vector2(Random.Range(0, Width), Random.Range(0, Height));
        Vector2 randomDir = new Vector2();
        Vector2 lastDir = new Vector2();
        bool lastWasHall = false;
        for (int i = 0; i <= MaxTunnels;)
        {
            while (randomDir == lastDir || randomDir == -lastDir)
                randomDir = new List<Vector2> { Vector2.up, Vector2.down, Vector2.right, Vector2.left }[Random.Range(0, 4)];

            int nextStep = Random.Range(0, 3);

            if (nextStep == 0)
            {
                //GenerateTunnel(ref currentPoint, ref randomDir);
                currentPoint = GenerateTunnel_v2(currentPoint, ref randomDir);
                lastWasHall = true;
                i++;
                lastDir = randomDir;
            }
            else if (nextStep == 1 && lastWasHall && BuildRectRoom)
            {
                Vector2 nextStart = GenerateRectRoom(currentPoint, ref randomDir);
                currentPoint = nextStart;
                lastWasHall = false;
                lastDir = randomDir;
                i++;
            }
            else if (nextStep == 2 && lastWasHall && BuildCircleRoom)
            {
                Vector2 nextStart = GenerateCircleRoom(currentPoint, ref randomDir);
                currentPoint = nextStart;
                lastWasHall = false;
                lastDir = randomDir;
                i++;
            }
            else continue;
        }
        BuildMap();
    }

    void GenerateTunnel(ref Vector2 currentPoint, ref Vector2 randomDir) //standart version of tunnel generation cell by cell
    {
        var randomLength = Random.Range(MinTunnelLength, MaxTunnelLength);
        var tunnelLength = 0;

        while (tunnelLength <= randomLength)
        {
            if (currentPoint.x <= 0 && randomDir.x == -1 ||
               currentPoint.y <= 0 && randomDir.y == -1 ||
               currentPoint.x > Width && randomDir.x == 1 ||
               currentPoint.y > Height && randomDir.y == 1) break;
            else
            {
                MapData[currentPoint] = false;
                currentPoint = currentPoint + randomDir;
                tunnelLength++;
            }
        }
    }

    Vector2 GenerateTunnel_v2(Vector2 start, ref Vector2 direction) //version of tunnel generation using unity "Rect" class
    {
        var randomLength = Random.Range(MinTunnelLength, MaxTunnelLength);
        var tunnel = new Rect(start, new Vector2(direction.x == 0 ? (RandomTunnWidth ? Random.Range(0, TunnelWidth) : TunnelWidth) : randomLength * direction.x,
                                                 direction.y == 0 ? (RandomTunnWidth ? Random.Range(0, TunnelWidth) : TunnelWidth) : randomLength * direction.y));
        if (tunnel.xMax < 0) tunnel.xMax = 0;
        else if (tunnel.xMax > Width) tunnel.xMax = Width;
        if (tunnel.yMax < 0) tunnel.yMax = 0;
        else if (tunnel.yMax > Height) tunnel.yMax = Height;
        RectRoomsAndTunnels.Add(tunnel);
        return new Vector2(tunnel.xMax, tunnel.yMax);
    }

    Vector2 GenerateRectRoom(Vector2 start, ref Vector2 direction)
    {
        var heigth = Random.Range(MinRRoomSize, MaxRRoomSize);
        var width = Random.Range(MinRRoomSize, MaxRRoomSize);
        Vector2 offsetX = new Vector2(Random.Range(1, width), 0);
        Vector2 offsetY = new Vector2(0, Random.Range(1, heigth));
        Vector2 next = start;

        if (direction.y > 0)
        {
            var rect = new Rect((start - offsetX), new Vector2(width, heigth));
            RectRoomsAndTunnels.Add(rect);
            next = RandomPointInRect(rect);
            direction = new List<Vector2> { Vector2.up, Vector2.right, Vector2.left }[Random.Range(0, 3)];
        }
        else if (direction.y < 0)
        {
            var rect = new Rect((start - offsetX), new Vector2(width, -heigth));
            RectRoomsAndTunnels.Add(rect);
            next = RandomPointInRect(rect);
            direction = new List<Vector2> { Vector2.down, Vector2.right, Vector2.left }[Random.Range(0, 3)];
        }
        else if (direction.x > 0)
        {
            var rect = new Rect((start - offsetY), new Vector2(width, heigth));
            RectRoomsAndTunnels.Add(rect);
            next = RandomPointInRect(rect);
            direction = new List<Vector2> { Vector2.down, Vector2.right, Vector2.up }[Random.Range(0, 3)];
        }
        else if (direction.x < 0)
        {
            var rect = new Rect((start - offsetY), new Vector2(-width, heigth));
            RectRoomsAndTunnels.Add(rect);
            next = RandomPointInRect(rect);
            direction = new List<Vector2> { Vector2.down, Vector2.left, Vector2.up }[Random.Range(0, 3)];
        }
        return next;
    }

    Vector2 GenerateCircleRoom(Vector2 start, ref Vector2 direction)
    {
        var radius = Random.Range(MinCRoomRadius, MaxCRoomRadius);
        Vector2 next = start;

        if (direction.y > 0)
        {
            var circleData = new Circle(start - new Vector2(0, radius), radius);
            CircleRooms.Add(circleData);
            next = circleData.RandomPointInCircle();
            direction = new List<Vector2> { Vector2.up, Vector2.left, Vector2.right }[Random.Range(0, 3)];
        }
        else if (direction.y < 0)
        {
            var circleData = new Circle(start + new Vector2(0, radius), radius);
            CircleRooms.Add(circleData);
            next = circleData.RandomPointInCircle();
            direction = new List<Vector2> { Vector2.down, Vector2.left, Vector2.right }[Random.Range(0, 3)];
        }
        else if (direction.x > 0)
        {
            var circleData = new Circle(start - new Vector2(radius, 0), radius);
            CircleRooms.Add(circleData);
            next = circleData.RandomPointInCircle();
            direction = new List<Vector2> { Vector2.down, Vector2.up, Vector2.right }[Random.Range(0, 3)];
        }
        else if (direction.x < 0)
        {
            var circleData = new Circle(start + new Vector2(radius, 0), radius);
            CircleRooms.Add(circleData);
            next = circleData.RandomPointInCircle();
            direction = new List<Vector2> { Vector2.down, Vector2.up, Vector2.left }[Random.Range(0, 3)];
        }
        return next;
    }

    void BuildMap()
    {
        SetCircleRoomsData();
        SetRectRoomsData();

        foreach (var p in MapData)
        {
            FloorMap.SetTile(new Vector3Int((int)p.Key.x, (int)p.Key.y, 0), p.Value ? null : FloorTile);
            WallMap.SetTile(new Vector3Int((int)p.Key.x, (int)p.Key.y, 0), p.Value ? WallTile : null);
        }
        var borderCoords = MapBorder(MapData);

        for (int x = (int)borderCoords["MinPoint"].x - 2; x <= borderCoords["MaxPoint"].x + 2; x++)
            for (int y = (int)borderCoords["MinPoint"].y - 2; y <= borderCoords["MaxPoint"].y + 2; y++)
            {
                if (MapData.ContainsKey(new Vector2(x, y))) continue;
                else
                {
                    FloorMap.SetTile(new Vector3Int(x, y, 0), null);
                    WallMap.SetTile(new Vector3Int(x, y, 0), WallTile);
                }
            }
    }

    Dictionary<string, Vector2> MapBorder(Dictionary<Vector2, bool> data)
    {
        var minX = data.Min(x => x.Key.x);
        var maxX = data.Max(x => x.Key.x);
        var minY = data.Min(y => y.Key.y);
        var maxY = data.Max(y => y.Key.y);

        return new Dictionary<string, Vector2> { { "MinPoint", new Vector2(minX, minY) }, { "MaxPoint", new Vector2(maxX, maxY) } };
    }

    Vector2 RandomPointInRect(Rect rect)
    {
        return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
    }

    void SetRectRoomsData() //convert the data of each rectangle room and tunnel to map data
    {
        foreach (var room in RectRoomsAndTunnels)
            for (int x = (int)Mathf.Min(room.xMin, room.xMax); x <= (int)Mathf.Max(room.xMin, room.xMax); x++)
                for (int y = (int)Mathf.Min(room.yMin, room.yMax); y <= (int)Mathf.Max(room.yMin, room.yMax); y++)
                {
                    var point = new Vector2(x, y);
                    MapData[point] = false;
                }
    }

    void SetCircleRoomsData() //convert the data of each circle room to map data
    {
        foreach (var room in CircleRooms)
        {
            foreach (var point in room.CircleData)
                MapData[point] = false;
        }
    }

    public void ClearMap()
    {
        var maps = GameObject.FindObjectsOfType(typeof(Tilemap));
        foreach (Tilemap m in maps)
            m.ClearAllTiles();
    }
}