﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding
{
    private List<PathNode> map;
    private Vector2 mapDimensions;
    private List<PathNode> openList;

    void start()
    {
        map = GameObject.FindObjectOfType<GenerateTilemapNodes>().nodes;
        mapDimensions = GameObject.FindObjectOfType<GenerateTilemapNodes>().tileMapDimensions;
    }



    List<PathNode> GetAdjacent(PathNode currentNode)
    {
        List<PathNode> neighborsList = new List<PathNode>();
        return neighborsList;
    }

    //private List<PathNode> FindPath(Vector2 startPos, Vector2 endPos)
    //{
    //    for(int i = 0; i < map.Count; i++)
    //    {
    //        if(map[i].position == startPos)
    //        {
    //            PathNode startNode = map[i];
    //            break;
    //        }
    //    }
    //}
}


