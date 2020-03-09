using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Vector2 position;
    protected float gridSize = 1;

    protected PathNode currentMapNode;
    protected AStarPathFinding pathFinder;

    protected virtual void Start()
    {
        pathFinder = GetComponent<AStarPathFinding>();
        currentMapNode = pathFinder.GetNearestNode(new Vector2(-1, 0));
        transform.position = currentMapNode.position;
    }

    public virtual void Move(Vector2 direction)
    {
        position += direction * gridSize;
        this.transform.position = currentMapNode.position;
    }
}
