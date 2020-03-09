using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
    private List<PathNode> map;
    private List<PathNode> openList;
    private int currentNodeIdx = 0;

    public PathNode GetCurrentNode(){ return map[currentNodeIdx]; }

    private GenerateTilemapNodes tileMapNodes;

    private void Start()
    {
        tileMapNodes = GenerateTilemapNodes.Instance;
        map = tileMapNodes.nodes;

        //GetNode();
    }

    List<int> GeneratePath(int start, int goal)
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

    List<int> GetAdjacent(int idx)
    {
        return new List<int>();
    }

    private int PositionToIndex(Vector2 pos)
    {
        for(int x = 0; x < pos.x; x++)
        {
            for (int y = 0; y < pos.x; y++)
            {

            }

        }
        return -1;
    }



    //private List<PathNode> FindPath(int startIdx, int endIdx)
    //{
    //    for (int i = 0; i < map.Count; i++)
    //    {
    //        if (map[i].position == startPos)
    //        {
    //            PathNode startNode = map[i];
    //            break;
    //        }
    //    }
    //}
}


