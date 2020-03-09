using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
    private List<PathNode> map;

    private Vector2 tileSize;

    private GenerateTilemapNodes tileMapNodes;

    private void Start()
    {
        tileMapNodes = GenerateTilemapNodes.Instance;
        map = tileMapNodes.nodes;
        tileSize = tileMapNodes.TileSize;
        PathNode test = GetNode(map[0].position);
        GeneratePath(map[10], map[24]);
    }


    public float FindH(PathNode node, PathNode goal)
    {
        return Mathf.Abs(node.position.x - goal.position.x) +
                    Mathf.Abs(node.position.y - goal.position.y);
    }

    public List<PathNode> GeneratePath(PathNode start, PathNode goal)
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
            foreach(var i in GetAdjacent(current))
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
            GameObject go = new GameObject();
            LineRenderer lr = go.AddComponent<LineRenderer>();
            lr.startColor = Color.red;
            lr.endColor = Color.red;
            lr.sortingOrder = 1000;
            lr.startWidth = 0.15f;
            lr.endWidth = 0.15f;

            Path.Add(current);
            lr.SetPosition(0, current.position);
            current = current.cameFromNode;
            lr.SetPosition(1, current.position);
        }
        return Path;
    }



    public List<PathNode> GetAdjacent(PathNode currentNode)
    {
        List<PathNode> validNeighbors = new List<PathNode>();

        //tilesize cannot exceed 10 or this function breaks
        Vector2 position = currentNode.position;
        Vector2 up = new Vector2(0, tileSize.y);
        Vector2 down = new Vector2(0, -tileSize.y);
        Vector2 left = new Vector2(-tileSize.x, 0);
        Vector2 right = new Vector2(tileSize.x, 0);

        PathNode neighborNode;

        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    neighborNode = GetNode(position + up);
                    break;

                case 1:
                    neighborNode = GetNode(position + down);
                    break;

                case 2:
                    neighborNode = GetNode(position + left);
                    break;

                case 3:
                    neighborNode = GetNode(position + right);
                    break;

                default:
                    neighborNode = currentNode;
                    break;
            }
            if (neighborNode.position != position && neighborNode.isTraversable)
                validNeighbors.Add(neighborNode);
        }
        return validNeighbors;
    }

    private PathNode GetNode(Vector2 nodePos)
    {
        IEnumerable<PathNode> nodeQuery =
            from node in map
            where node.position == nodePos
            select node;
        return nodeQuery.First();
    }

    public PathNode GetNearestNode(Vector2 pos)
    {
        float modX = pos.x % tileSize.x;
        float modY = pos.y % tileSize.y;
        pos.x -= modX;
        pos.y -= modY;

        PathNode nearest = map[0];
        foreach(var i in map)
        {
            if (Vector2.Distance(i.position, pos) < Vector2.Distance(nearest.position, pos))
            {
                nearest = i;
            }
        }
        return nearest;
    }

}


