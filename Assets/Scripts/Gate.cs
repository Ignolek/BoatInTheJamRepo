using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 closePosition;
    public GameObject waveSpawner;

    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(waveSpawner.GetComponent<WaveSpawner>().inCombat)
        {
            //transform.position.y = Mathf.Lerp(initialPosition.y, closePosition.y, );
        }
    }
}
