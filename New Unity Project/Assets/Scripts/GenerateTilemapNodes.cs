﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTilemapNodes : MonoBehaviour
{
    private static GenerateTilemapNodes _instance;
    public static GenerateTilemapNodes Instance { get { return _instance; } }



    [SerializeField]
    private Tilemap floor;
    [SerializeField]
    private List<Tilemap> obstacles;

    public List<PathNode> nodes = new List<PathNode>();
    public Vector2Int tileMapDimensions;

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

    private void Start()
    {

    }
    private void GenerateNodes()
    {
        //Get width & height
        BoundsInt bounds = floor.cellBounds;
        tileMapDimensions = new Vector2Int(bounds.xMax, bounds.yMax);

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
            nodes.Add(pathNode);
        }
    }
}
