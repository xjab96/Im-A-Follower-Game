using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Vector2 position;
    protected float gridSize = 1;

    float f;
    float g;
    float h;
    List<Vector2> openList = new List<Vector2>();
    List<Vector2> closedList = new List<Vector2>();



    protected virtual void Start()
    {

    }

    public virtual void Move(Vector2 direction)
    {
        position += direction * gridSize;
        this.transform.position = position;
    }

    protected virtual void AStar(Vector2 goal)
    {
        //openList.Add()
    }

}
