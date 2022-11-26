using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<DebugPlayerRestart>().lastCheckpointPosition = transform.position;
        }
    }
}
