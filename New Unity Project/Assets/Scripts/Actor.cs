using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Vector2 position;
    protected float gridSize = 1;
    protected float speed = 10;

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

    protected virtual void Update()
    {
        if ((Vector2)transform.position != currentMapNode.position)
        {
            transform.position = Vector2.Lerp(transform.position, currentMapNode.position, speed * Time.deltaTime);
        }
    }
}
