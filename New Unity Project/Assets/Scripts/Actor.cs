using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Vector2 position;
    protected float gridSize = 1;
    protected float speed = 10;

    protected PathNode currentMapNode;
    protected AStarPathFinding pathFinder;

    [SerializeField]
    protected SpriteRenderer sprite;

    protected Transform startMovementTransform;
    protected Quaternion startMovementRotation;

    protected virtual void Start()
    {
        pathFinder = GetComponent<AStarPathFinding>();
        currentMapNode = pathFinder.GetNearestNode(new Vector2(-1, 0));
        transform.position = currentMapNode.position;

        startMovementTransform = transform;
        startMovementRotation = sprite.transform.rotation;
    }

    protected virtual void Update()
    {
        //Lerp to new correct position & set sprite rotation back to zero
        transform.position = Vector2.Lerp(startMovementTransform.transform.position, currentMapNode.position, speed * Time.deltaTime);

        if((int)sprite.transform.rotation.eulerAngles.z != 0)
        {
            if (sprite.transform.rotation.eulerAngles.z < 180)
            {
                sprite.transform.Rotate(new Vector3(0, 0, -(speed * 4) * Time.deltaTime));
            }
            else if (sprite.transform.rotation.eulerAngles.z > 0)
            {
                sprite.transform.Rotate(new Vector3(0, 0, (speed * 4) * Time.deltaTime));
            }
        }
    }

    protected virtual void rotateOnMove()
    {
        int rotateZ = Random.Range(-25, 25);
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, rotateZ);

        startMovementTransform = transform;
        sprite.transform.rotation = rotation;
    }
}
