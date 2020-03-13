using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class TilemapNodes : MonoBehaviour
{
    private static TilemapNodes _instance;
    public static TilemapNodes Instance { get { return _instance; } }

    public Vector2 tileSize = new Vector2(1, 1);

    [SerializeField]
    private Tilemap floor;
    [SerializeField]
    private List<Tilemap> obstacles;
    [SerializeField]
    private List<Tilemap> doors;
    [SerializeField]
    private List<Tilemap> traps;

    public List<PathNode> nodes = new List<PathNode>();
    public Vector2 TileSize = new Vector2(1,1);

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        GenerateNodes();
    }


    private void GenerateNodes()
    {
        /* We only need to find out if the floor is walkable and if its not obstructed. Therefore we simply iterate and build a map
         * Using the floor boundaries. If the coordinates match that of an obstacle we set the path node to be obstructed */
        foreach (Vector3Int pos in floor.cellBounds.allPositionsWithin)
        {
            Vector3Int localPos = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 worldPos = floor.CellToWorld(localPos);
            PathNode pathNode = new PathNode(worldPos);

            if (floor.HasTile(localPos))
                pathNode.isTraversable = true;
            else
                pathNode.isTraversable = false;
            //Obstacles
            for (int i = 0; i < obstacles.Count; i++)
            {
                foreach (Vector3Int obPos in obstacles[i].cellBounds.allPositionsWithin)
                {
                    localPos = new Vector3Int(obPos.x, obPos.y, obPos.z);
                    Vector3 obWorldPos = obstacles[i].CellToWorld(localPos);

                    if (obstacles[i].HasTile(localPos) && worldPos == obWorldPos)
                    {
                        pathNode.isTraversable = false;
                        break;
                    }
                }
            }

            //find the trigger zones for the door instead
            for(int i = 0; i < doors.Count; i++)
            {
                foreach(Vector3Int doorPos in doors[i].cellBounds.allPositionsWithin)
                {
                    localPos = new Vector3Int(doorPos.x, doorPos.y, doorPos.z);
                    Vector3 doorWorldPos = doors[i].CellToWorld(localPos);

                    if(doors[i].HasTile(localPos) && worldPos == doorWorldPos)
                    {
                        pathNode.isTraversable = false;
                        break;
                    }
                }
            }

            //Just get the coordinates for the traps and add a tile based on those coords
            for (int i = 0; i < traps.Count; i++)
            {
                foreach (Vector3Int trapPos in traps[i].cellBounds.allPositionsWithin)
                {
                    localPos = new Vector3Int(trapPos.x, trapPos.y, trapPos.z);
                    Vector3 trapWorldPos = traps[i].CellToWorld(localPos);

                    if (traps[i].HasTile(localPos) && worldPos == trapWorldPos)
                    {
                        pathNode.isTraversable = true;
                        break;
                    }
                }
            }
            nodes.Add(pathNode);
        }
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

    public PathNode GetNode(Vector2 nodePos)
    {
        IEnumerable<PathNode> nodeQuery =
            from node in nodes
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

        PathNode nearest = nodes[0];
        foreach (var i in nodes)
        {
            if (Vector2.Distance(i.position, pos) < Vector2.Distance(nearest.position, pos))
            {
                nearest = i;
            }
        }
        return nearest;
    }


}
