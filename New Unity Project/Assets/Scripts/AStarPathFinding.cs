using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathFinding : MonoBehaviour
{
    private List<PathNode> map;
    private Vector2Int mapDimensions;
    private int mapMaxIdx;
    private List<PathNode> openList;
    private int currentNodeIdx = 0;

    public PathNode GetCurrentNode(){ return map[currentNodeIdx]; }

    private GenerateTilemapNodes tileMapNodes;

    private void Start()
    {
        tileMapNodes = GenerateTilemapNodes.Instance;
        map = tileMapNodes.nodes;
        mapDimensions = tileMapNodes.tileMapDimensions;
        mapMaxIdx = (mapDimensions.x * mapDimensions.y) - 1; //Minus 1 as it is an idx that starts from 0
    }

    List<int> GeneratePath(int start, int goal)
    {
        return new List<int>();
    }

    List<int> GetAdjacent(int idx)
    {
        List<int> validNeighborsIdx = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            switch(i)
            {
                case 0: //Up
                    if((idx - mapDimensions.x > 0) && map[(idx - mapDimensions.x)].isTraversable)
                    {
                        validNeighborsIdx.Add(idx - mapDimensions.x);
                        //Then left idx is also valid
                        if(map[idx - 1].isTraversable)
                        {
                            validNeighborsIdx.Add(idx - 1);
                            i++; //skip next
                        }
                    }
                    break;

                case 1: //Left
                    if ((idx - 1 > 0) && map[(idx - 1)].isTraversable)
                    {
                        validNeighborsIdx.Add(idx - 1);
                    }
                    break;

                case 2: //Down
                    if(idx + mapDimensions.x < mapMaxIdx && map[idx + mapDimensions.x].isTraversable)
                    {
                        validNeighborsIdx.Add(idx + mapDimensions.x);
                        //Then right idx is also valid
                        if(map[idx + 1].isTraversable)
                        {
                            validNeighborsIdx.Add(idx + 1);
                            i++; //skip next
                        }
                    }
                    break;

                case 3: //Right
                    if(idx + 1 < mapMaxIdx && map[idx + 1].isTraversable)
                    {
                        validNeighborsIdx.Add(idx + 1);
                    }
                    break;

                default:
                    break;
            }
        }
        return validNeighborsIdx;
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


