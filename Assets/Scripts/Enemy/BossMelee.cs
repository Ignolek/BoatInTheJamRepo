using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMelee : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform?.parent?.transform.name == "Player")
        {
            GameObject.Find("Health").GetComponent<HealthSystem>().TakeDamage();
            GameObject.Find("Health").GetComponent<HealthSystem>().TakeDamage();
        }
    }
}
