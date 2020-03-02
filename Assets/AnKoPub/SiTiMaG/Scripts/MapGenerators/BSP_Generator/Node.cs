using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Node
{
    private int MinLeafSize = BSPGenerator.MinLeafSizeStatic;
    private int HallWidth = BSPGenerator.HallsWidthStatic;
    public bool RandomizeHallWidth = BSPGenerator.RandomHallWidth;

    Point Position;
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Node LeftChild { get; private set; }
    public Node RightChild { get; private set; }
    public Rect Room { get; private set; }
    public List<Rect> Halls { get; private set; }

    public Node(Point startPoint, int width, int height)
    {
        Position = startPoint;
        Width = width;
        Height = height;
    }

    public bool Split()
    {
        if (LeftChild != null || RightChild != null) return false;

        bool splitH = Random.Range(0f, 1f) > 0.5;

        if (Width > Height && Width / Height >= 1.25) splitH = false;
        else if (Height > Width && Height / Width >= 1.25) splitH = true;

        var max = (splitH ? Height : Width) - MinLeafSize;
        if (max <= MinLeafSize) return false;
        var split = Random.Range(MinLeafSize, max);

        if (splitH)
        {
            LeftChild = new Node(Position, Width, split);
            RightChild = new Node(new Point(Position.X, Position.Y + split), Width, Height - split);
        }
        else
        {
            LeftChild = new Node(Position, split, Height);
            RightChild = new Node(new Point(Position.X + split, Position.Y), Width - split, Height);
        }
        return true;
    }

    public void CreateRooms()
    {
        if (LeftChild != null || RightChild != null)
        {
            if (LeftChild != null) LeftChild.CreateRooms();

            if (RightChild != null) RightChild.CreateRooms();

            if (LeftChild != null && RightChild != null)
                CreateHall(LeftChild.GetRoom(), RightChild.GetRoom());
        }

        else
        {
            Point roomSize;
            Point roomPos;

            roomSize = new Point(Random.Range(3, Width - 2), Random.Range(3, Height - 2));
            roomPos = new Point(Random.Range(1, Width - roomSize.X - 1), Random.Range(1, Height - roomSize.Y - 1));
            Room = new Rect(Position.X + roomPos.X, Position.Y + roomPos.Y, roomSize.X, roomSize.Y);
        }
    }

    public Rect GetRoom()
    {
        if (Room != Rect.zero) return Room;
        else
        {
            var lRoom = Rect.zero;
            var rRoom = Rect.zero;

            if (LeftChild != null)
                lRoom = LeftChild.GetRoom();
            if (RightChild != null)
                rRoom = RightChild.GetRoom();
            if (lRoom == Rect.zero && rRoom == Rect.zero) return Rect.zero;
            else if (rRoom == Rect.zero) return lRoom;
            else if (lRoom == Rect.zero) return rRoom;
            else if (Random.Range(0f, 1f) > 0.5) return lRoom;
            else return rRoom;
        }
    }

    public void CreateHall(Rect l, Rect r)
    {
        Halls = new List<Rect>();

        var point1 = new Point((int)Random.Range(l.xMin + 1, l.xMax - 2), (int)Random.Range(l.yMin + 1, l.yMax - 2));
        var point2 = new Point((int)Random.Range(r.xMin + 1, r.xMax - 2), (int)Random.Range(r.yMin + 1, r.yMax - 2));

        var w = point2.X - point1.X;
        var h = point2.Y - point1.Y;

        if (w < 0)
        {
            if (h < 0)
            {
                if (Random.Range(0f, 1f) < 0.5)
                {
                    Halls.Add(new Rect(point2.X, point1.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
                    Halls.Add(new Rect(point2.X, point2.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
                }
                else
                {
                    Halls.Add(new Rect(point2.X, point2.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
                    Halls.Add(new Rect(point1.X, point2.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
                }
            }

            else if (h > 0)
            {
                if (Random.Range(0f, 1f) < 0.5)
                {
                    Halls.Add(new Rect(point2.X, point1.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
                    Halls.Add(new Rect(point2.X, point1.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
                }
                else
                {
                    Halls.Add(new Rect(point2.X, point2.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
                    Halls.Add(new Rect(point1.X, point1.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
                }
            }
            else Halls.Add(new Rect(point2.X, point2.Y, Mathf.Abs(w), 0));
        }

        else if (w > 0)
        {
            if (h < 0)
            {
                if (Random.Range(0f, 1f) < 0.5)
                {
                    Halls.Add(new Rect(point1.X, point2.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
                    Halls.Add(new Rect(point1.X, point2.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
                }
                else
                {
                    Halls.Add(new Rect(point1.X, point1.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
                    Halls.Add(new Rect(point2.X, point2.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
                }
            }

            else if (h > 0)
            {
                if (Random.Range(0f, 1f) < 0.5)
                {
                    Halls.Add(new Rect(point1.X, point1.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
                    Halls.Add(new Rect(point2.X, point1.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
                }
                else
                {
                    Halls.Add(new Rect(point1.X, point2.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
                    Halls.Add(new Rect(point1.X, point1.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
                }
            }
            else Halls.Add(new Rect(point1.X, point1.Y, Mathf.Abs(w), RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth));
        }
        else
        {
            if (h < 0) Halls.Add(new Rect(point2.X, point2.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
            else if (h > 0) Halls.Add(new Rect(point1.X, point1.Y, RandomizeHallWidth ? Random.Range(0, HallWidth) : HallWidth, Mathf.Abs(h)));
        }
    }
}