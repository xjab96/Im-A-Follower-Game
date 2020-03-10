using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor
{
    private static PlayerActor _instance;
    public static PlayerActor GetInstance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    protected override void Start()
    {
        base.Start();
        currentMapNode = pathFinder.GetNearestNode(new Vector2(-2, 0));
        transform.position = currentMapNode.position;
    }
    public void Move(Vector2 direction)
    {
        foreach(var i in pathFinder.GetAdjacent(currentMapNode))
        {
            if(currentMapNode.position + direction == i.position)
            {
                currentMapNode = i;

                rotateOnMove();

            }
        }
    }
}
