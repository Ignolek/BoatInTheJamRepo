using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public Vector3 offsetVec;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offsetVec;
    }
}
