using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : Actor
{
    List<PathNode> currentPath;
    protected override void Start()
    {
        base.Start();
        currentPath = pathFinder.GeneratePath(currentMapNode, pathFinder.GetNearestNode(new Vector2(3, 1)));
    }

    public void Move()
    {
        if(currentPath.Count != 0)
        {
            currentMapNode = currentPath[currentPath.Count - 1];
            currentPath.RemoveAt(currentPath.Count - 1);
            rotateOnMove();
        }
    }

    public List<PathNode> CheckPointNodes = new List<PathNode>();

    public CheckPointTag checkPointTag = CheckPointTag.A;

    protected List<PathNode> FindCheckPointNodes()
    {

        return CheckPointNodes;
    }


}

