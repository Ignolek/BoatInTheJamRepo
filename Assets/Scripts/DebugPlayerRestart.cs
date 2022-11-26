using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerRestart : MonoBehaviour
{
    public Vector3 lastCheckpointPosition;
    public bool Restart;
    public float DebugCountdownToRestart = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DebugCountdownToRestart -= Time.deltaTime;
        if (DebugCountdownToRestart <= 0)
        {
            Restart = true;
            transform.position = lastCheckpointPosition;
            DebugCountdownToRestart = 20.0f;
        }    
    }

    public void RestartFromLastCheckpoint()
    {
        if (Restart)
        {
            Restart = false;
        }
    }
}
