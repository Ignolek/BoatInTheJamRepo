using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy")
            return;

        if (GetComponent<BoxCollider>().enabled)
        {
            other.GetComponent<EnemyStateMachine>().TakeDamage(PlayerStats.Instance.lightAttackDamage);
        }
        if (GetComponent<SphereCollider>().enabled)
        {
            other.GetComponent<EnemyStateMachine>().TakeDamage(PlayerStats.Instance.heavyAttackDamage);
        }
    }
}
