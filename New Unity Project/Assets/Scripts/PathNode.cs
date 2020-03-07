using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public float g;
    public float h;
    public float f;
    public Vector2 position;
    public PathNode cameFromNode;
    public bool isTraversable;

    public PathNode(Vector2 Position)
    {
        this.position = Position;
    }
}