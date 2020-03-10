using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActor : Actor
{
    public List<Vector2> checkPointPositons = new List<Vector2>();
    public List<PathNode> checkPointNodes = new List<PathNode>();

    private int currentCheckPoint = 0;

    public CheckPointTag checkPointTag = CheckPointTag.A;

    private List<PathNode> currentPath;
    protected override void Start()
    {
        base.Start();
        FindCheckPointNodes();
        currentPath = pathFinder.GeneratePath(currentMapNode, checkPointNodes[currentCheckPoint]);
    }

    protected override void Update()
    {
        base.Update();


    }


    public void Move()
    {
        if(currentPath.Count != 0)
        {
            if(currentPath[currentPath.Count - 1].isTraversable == true)
            {
                currentMapNode.isTraversable = true;
                currentMapNode = currentPath[currentPath.Count - 1];
                currentMapNode.isTraversable = false;
                currentPath.RemoveAt(currentPath.Count - 1);
                rotateOnMove();
            }
            else
            {
                currentPath = pathFinder.GeneratePath(currentMapNode, checkPointNodes[currentCheckPoint]);
            }

        }
        else
        {
            currentCheckPoint++;
            if(currentCheckPoint == checkPointNodes.Count)
            {
                currentCheckPoint = 0;
            }
            currentPath = pathFinder.GeneratePath(currentMapNode, checkPointNodes[currentCheckPoint]);
        }
    }





    protected List<PathNode> FindCheckPointNodes()
    {
        for(int i = 0; i < checkPointPositons.Count; i++)
        {
            checkPointNodes.Add(pathFinder.GetNearestNode(checkPointPositons[i]));
        }
        return checkPointNodes;
    }


}

