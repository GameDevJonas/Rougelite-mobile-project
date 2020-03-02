using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Circle
{
    public Vector2 StartPoint { get; private set; }
    public int Radius { get; private set; }
    public Dictionary<string, Vector2> CircleBase { get; private set; }
    public List<Vector2> CircleData { get; private set; }

    public Circle(Vector2 start, int radius)
    {
        StartPoint = start;
        Radius = radius;
        CircleData = GenerateCircle();
        CircleBase = CircleBasePoints(CircleData);
    }

    List<Vector2> GenerateCircle()
    {
        var circleData = new List<Vector2>();
        var tempRad = Radius;
        while (tempRad >= 0)
        {
            for (int i = 0; i < 360; i++)
            {
                var rad = i * Mathf.PI / 180;
                var x = (int)(StartPoint.x + tempRad * Mathf.Cos(rad));
                var y = (int)(StartPoint.y + tempRad * Mathf.Sin(rad));
                var p = new Vector2(x, y);
                if (!circleData.Contains(p))
                {
                    circleData.Add(new Vector2(x, y));
                }
            }
            tempRad--;
        }
        return circleData;
    }

    Dictionary<string, Vector2> CircleBasePoints(List<Vector2> circle)
    {
        var left = circle.Aggregate((p1, p2) => p1.x < p2.x ? p1 : p2);
        var rigth = circle.Aggregate((p1, p2) => p1.x > p2.x ? p1 : p2);
        var up = circle.Aggregate((p1, p2) => p1.y < p2.y ? p1 : p2);
        var down = circle.Aggregate((p1, p2) => p1.y > p2.y ? p1 : p2);

        return new Dictionary<string, Vector2> { { "Left", left }, { "Rigth", rigth }, { "Up", up }, { "Down", down } };
    }

    public Vector2 RandomPointInCircle()
    {
        return CircleData[Random.Range(0, CircleData.Count)];
    }
}
