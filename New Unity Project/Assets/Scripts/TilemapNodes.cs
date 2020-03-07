using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapNodes : MonoBehaviour
{
    [SerializeField]
    private Tilemap floor;
    [SerializeField]
    private List<Tilemap> obstacles;

    //private List<Vector2> nodes = new List<Vector2>();

    private List<PathNode> nodes = new List<PathNode>();
    private PathNode[,] nodel;

    Vector2 startLocation;
    Vector2 endLocation;

    private void Start()
    {
        GenerateNodes();
    }
    private void GenerateNodes()
    {
        BoundsInt bounds = floor.cellBounds;
        foreach (Vector3Int pos in floor.cellBounds.allPositionsWithin)
        {
            Vector3Int localPos = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 worldPos = floor.CellToWorld(localPos);
            PathNode pathNode = new PathNode(worldPos);

            if (floor.HasTile(localPos))
                pathNode.isTraversable = true;
            else
                pathNode.isTraversable = false;

            for (int i = 0; i < obstacles.Count; i++)
            {
                foreach (Vector3Int obPos in obstacles[i].cellBounds.allPositionsWithin)
                {
                    localPos = new Vector3Int(obPos.x, obPos.y, obPos.z);
                    Vector3 obWorldPos = obstacles[i].CellToWorld(localPos);
                    if (obstacles[i].HasTile(localPos) && obWorldPos == worldPos)
                    {
                        pathNode.isTraversable = false;
                    } 
                }
            }
            nodes.Add(pathNode);
            nodel.Length = (bounds.x * bounds.y) - 1;
        }
    }
}
