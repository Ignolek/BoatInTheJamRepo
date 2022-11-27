using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Vector3 initialPosition;
    private Vector3 closePosition;
    public float floatToRiseGate;
    public GameObject waveSpawner;
    //public float lerpDuration;

    void Start()
    {
        initialPosition = transform.position;
        closePosition = new Vector3(initialPosition.x, initialPosition.y + floatToRiseGate, initialPosition.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (waveSpawner.GetComponent<WaveSpawner>().inCombat)
        {
            CloseTheGate();
        }
        else
            OpenTheGate();
        
    }

    void CloseTheGate()
    {
        transform.position = closePosition;
    }

    void OpenTheGate()
    {
        transform.position = initialPosition;
    }
}
