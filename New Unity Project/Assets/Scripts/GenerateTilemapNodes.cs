using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTilemapNodes : MonoBehaviour
{
    [SerializeField]
    private Tilemap floor;
    [SerializeField]
    private Tilemap walls;
    [SerializeField]
    private List<Tilemap> obstacles;

    private List<PathNode> nodes = new List<PathNode>();
    private Vector2 tileMapDimensions;

    private void Start()
    {
        GenerateNodes();
    }
    private void GenerateNodes()
    {
        //Get width & height
        BoundsInt bounds = walls.cellBounds;
        tileMapDimensions = new Vector2(bounds.xMax, bounds.yMax);
        Debug.Log("tests");
        Debug.Log("tests");
        foreach (Vector3Int pos in walls.cellBounds.allPositionsWithin)
        {
            Debug.Log("test1");
            Vector3Int localPos = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 worldPos = walls.CellToWorld(localPos);

            PathNode pathNode = new PathNode(worldPos);


            foreach (Vector3Int floorPos in floor.cellBounds.allPositionsWithin)
            {
                localPos = new Vector3Int(floorPos.x, floorPos.y, floorPos.z);
                Vector3 floorWorldPos = floor.CellToWorld(localPos);
                if (floor.HasTile(localPos))
                    pathNode.isTraversable = true;
                else
                    pathNode.isTraversable = false;
            }

            for (int i = 0; i < obstacles.Count; i++)
            {
                foreach (Vector3Int obPos in obstacles[i].cellBounds.allPositionsWithin)
                {
                    localPos = new Vector3Int(obPos.x, obPos.y, obPos.z);
                    if (obstacles[i].HasTile(localPos))
                    {
                        pathNode.isTraversable = false;
                    }
                    Debug.Log(localPos);
                }
            }
            nodes.Add(pathNode);
            Debug.Log("tests");

        }
    }
}
