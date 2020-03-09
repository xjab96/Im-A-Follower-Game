using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Vector2 position;

    public bool isTraversable = false;

    public float g = 0;
    public float h = 0;
    public float f = 0;

    public PathNode cameFromNode;

    public PathNode(Vector2 Position)
    {
        this.position = Position;
    }
}