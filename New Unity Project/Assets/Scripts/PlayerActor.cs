﻿using System.Collections;
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
        currentMapNode = tilemapNodes.GetNearestNode(new Vector2(-2, 0));
        transform.position = currentMapNode.position;
    }
    public bool Move(Vector2 direction)
    {
        foreach(var i in tilemapNodes.GetAdjacent(currentMapNode))
        {
            if(currentMapNode.position + direction == i.position)
            {
                currentMapNode.isTraversable = true;
                currentMapNode = i;
                currentMapNode.isTraversable = false;
                rotateOnMove();
                return true;
            }
        }
        return false;
    }
}
