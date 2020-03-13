using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
    public float FindH(PathNode node, PathNode goal)
    {
        return Mathf.Abs(node.position.x - goal.position.x) +
                    Mathf.Abs(node.position.y - goal.position.y);
    }

    public List<PathNode> GeneratePath(PathNode start, PathNode goal, TilemapNodes tileMapNodes)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();

        start.g = 0;
        start.h = FindH(start, goal);
        start.f = start.g + start.h;
        openList.Add(start);

        while(openList.Count != 0)
        {
            PathNode current = openList[0];
            foreach(var node in openList)
            {
                if (node.f < current.f)
                    current = node;
            }
            if (current == goal)
            {
                return ReconstructPath(start, current);
            }

            openList.Remove(current);
            closedList.Add(current);

            List<PathNode> neighbors = tileMapNodes.GetAdjacent(current);
            if(neighbors.Count == 0)
            {
                //The piece is trapped so cant path
                return neighbors;
            }
            foreach(var i in neighbors)
            {
                if(!closedList.Contains(i))
                {
                    i.g = current.g + 1;
                    i.h = FindH(i, goal);
                    i.f = i.g + i.h;

                    i.cameFromNode = current;
                    if (!openList.Contains(i))
                        openList.Add(i);
                }
            }

        }
        //Open set is empty but no path was found
        return new List<PathNode>();
    }

    private List<PathNode> ReconstructPath(PathNode start, PathNode current)
    {
        List<PathNode> Path = new List<PathNode>();
        while(current != start)
        {
            Path.Add(current);
            current = current.cameFromNode;
        }
        return Path;
    }
}


