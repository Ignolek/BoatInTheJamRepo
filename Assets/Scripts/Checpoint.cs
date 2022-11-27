using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checpoint : MonoBehaviour
{
    [Header("Checkpoint values")]
    public Transform transform;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform?.parent?.transform.name == "Player")
        {
            GameObject player = other.transform.parent.gameObject;

            transform = player.transform;

            HealthSystem health = FindObjectOfType<HealthSystem>();
            health.lastCheckpoint = transform.position;
        }
    }
}
