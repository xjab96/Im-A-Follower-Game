using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
    private List<PathNode> map;
    private List<PathNode> openList;
    private int currentNodeIdx = 0;
    private Vector2 tileSize;

    private GenerateTilemapNodes tileMapNodes;

    private void Start()
    {
        tileMapNodes = GenerateTilemapNodes.Instance;
        map = tileMapNodes.nodes;
        tileSize = tileMapNodes.TileSize;
        List<PathNode> test = GetAdjacent(map[10]);
        foreach(var i in test)
        {
            Debug.Log(i.position);
        }
        //GetNode();
    }

    List<int> GeneratePath(PathNode start, PathNode goal)
    {
        return new List<int>();
    }

    public PathNode GetNode(Vector2 nodePos)
    {
        IEnumerable<PathNode> nodeQuery = 
            from node in map
            where node.position == nodePos
            select node;
        return nodeQuery.First();
    }

    private List<PathNode> GetAdjacent(PathNode currentNode)
    {
        List<PathNode> validNeighbors = new List<PathNode>();

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
}


