using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointAndRespawn : MonoBehaviour
{
    // Spawnpoint and Respawn
    public Vector3 Spawnpoint = new Vector3(0, 0, 0);
    public bool DoRespawnAfterLoseMap = true;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DoRespawnAfterLoseMap)
            RespawnAfterLoseMap();
    }

    public void RespawnAfterLoseMap()
    {
        if (this.transform.position.y < -10)
        {
            GoToRespawn();
        }
    }
    
    public void GoToRespawn()
    {
            this.transform.position = Spawnpoint;
    }
}
