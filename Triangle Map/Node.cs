﻿using System;
using System.Collections.Generic;
using DijkstraSpace;
using Point_Map;

namespace BaseNode
{
    abstract class Node : IComparable<Node>
    {
        public float value { get => Value(); }
        public bool visited { get; protected set; }
        public Node father { get; protected set; }
        public float distance { get; protected set; }

        /// <summary> This HeapNode is used by Heapify in Relax .</summary>
        public HeapNode heapNode { get; protected set; }

        public abstract float Value();
        public abstract List<Node> GetAdyacents();
        public abstract float Distance(Node node);

        public virtual void SetDistance(float value) { distance = value; }
        public virtual void SetFather(Node node) { father = node; }
        public virtual void SetVisited(bool value = true) { visited = value; }
        public virtual void SetHeapNode(HeapNode heapNode) { this.heapNode = heapNode; }
        public int CompareTo(Node other) { return this.value.CompareTo(other.value); }
    }
    class Map
    {
        internal List<Node> nodes { get; private set; }

        public Map()
        {
            nodes = new List<Node>();
        }
        public Map(List<PointNode> nodes)
        {
            this.nodes = new List<Node>();
        }
        public void AddNode(Node node)
        {
            nodes.Add(node);
        }
    }
    class Point
    {
        public float x { get; private set; }
        public float y { get; private set; }
        public float z { get; private set; }
        public Point(float x, float y, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public float Distance(Point point)
        {
            float temp = (point.x - this.x) * (point.x - this.x);
            temp += (point.y - this.y) * (point.y - this.y);
            temp += (point.z - this.z) * (point.z - this.z);
            return (float)Math.Sqrt(temp);
        }
        public static Point operator *(Point point, float a)
        {
            return new Point(a * point.x, a * point.y, point.z * a);
        }
        public static Point operator +(Point point1, Point point2)
        {
            return new Point(point1.x + point2.x, point1.y + point2.y, point1.z + point2.z);
        }
        public static Point operator -(Point point1, Point point2)
        {
            return new Point(point1.x - point2.x, point1.y - point2.y, point1.z - point2.z);
        }
        public static Point Max(Point point1, Point point2, string eye)
        {
            if (eye == "x")
            {
                if (point1.x > point2.x)
                    return point1;
                return point2;
            }
            if (eye == "y")
            {
                if (point1.y > point2.y)
                    return point1;
                return point2;
            }
            if (point1.z > point2.z)
                return point1;
            return point2;

        }
        public static Point Min(Point point1, Point point2, string eye)
        {
            if (eye == "x")
            {
                if (point1.x < point2.x)
                    return point1;
                return point2;
            }
            if (eye == "y")
            {
                if (point1.y < point2.y)
                    return point1;
                return point2;
            }
            if (point1.z < point2.z)
                return point1;
            return point2;

        }
        public override string ToString()
        {
            return "<" + x + "," + y + "," + z + ">";
        }

        public static float Distance_from_point_to_line(Point p, Point l1, Point l2)  // l1 and l2 points on the line
        {
            float a, b, c;
            if (l1.x == l2.x)
            {
                a = 1;
                b = 0;
            }
            else
            {
                a = (l2.y - l1.y)/(l1.x - l2.x);
                b = 1;
            }   
            c = - (a * l1.x + b * l1.y);

            return Math.Abs(a * p.x + b * p.y + c) / Math.Sqrt(Math.Pow(a,2) + Math.Pow(b,2));
        }
    }
}


