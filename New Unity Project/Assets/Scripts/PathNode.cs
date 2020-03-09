using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Vector2 position;

    public bool isTraversable = false;

    float zero = 0;

    public float g;
    public float h;
    public float f;

    public PathNode cameFromNode;

    public PathNode(Vector2 Position)
    {
        this.position = Position;
        g = 1 / zero;
        h = 1 / zero;
        f = 1 / zero;
    }
}