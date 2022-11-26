using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<BoxCollider>().enabled)
            Debug.Log("Box collider");
        if (GetComponent<SphereCollider>().enabled)
            Debug.Log("Sphere collider");

        Destroy(other.gameObject);
    }
}
