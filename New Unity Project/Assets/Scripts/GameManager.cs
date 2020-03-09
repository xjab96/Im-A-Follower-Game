using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private PlayerActor player;
    private ProtagonistActor protagonist;
    private List<EnemyActor> enemies = new List<EnemyActor>();

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
    }
    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerActor>();
        protagonist = GameObject.FindObjectOfType<ProtagonistActor>();
        enemies.AddRange(GameObject.FindObjectsOfType<EnemyActor>());


    }

    private void Update()
    {
        Step();
    }

    public void Step()
    {
        if(MoveInput())
        {
            //move protagonist && all enemies
        }
    }

    bool MoveInput()
    {
        if (Input.GetKeyDown("up") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("down") || Input.GetKeyDown(KeyCode.S))
        {
            player.Move(new Vector2(0.0F, Input.GetAxisRaw("Vertical")));
            return true;
        }
        if (Input.GetKeyDown("left") || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown("right") || Input.GetKeyDown(KeyCode.D))
        {
            player.Move(new Vector2(Input.GetAxisRaw("Horizontal"), 0.0F));
            return true;
        }
        return false;
    }
}
