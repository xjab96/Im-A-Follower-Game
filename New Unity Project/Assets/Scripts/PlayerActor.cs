using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : Actor
{
    private static PlayerActor _instance;
    public static PlayerActor GetInstance { get { return _instance; } }

    protected override void Start()
    {
        base.Start();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Update()
    {
    }
}
