using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.rigidbody.name);
        if(collision.other.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>().TakeDamage();
        }
        else
        {
            Debug.Log("STH");
        }

        Destroy(gameObject);
    }
}
