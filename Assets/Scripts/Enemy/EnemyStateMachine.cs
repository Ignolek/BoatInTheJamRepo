using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{

    //"Actors"
    private NavMeshAgent agent;
    public Transform Player;

    // DEBUG:

    //Params
    public float Health;
    public float CurrentHealth;
    public float DamagePerHit;
    public float timeBetweenAttacks;
    
    public bool isRangeType;
    public GameObject projectile;
    public float attackRange;

    //Attack
    bool alreadyAttacked;

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        agent.destination = Player.transform.position;
        float distance = Vector3.Distance(agent.transform.position, Player.transform.position);
        
        if (distance <= attackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Make sure enemy doesn't move
        if (isRangeType)
            agent.SetDestination(transform.position);

        transform.LookAt(Player.transform.position);

        if (!alreadyAttacked)
        {
            if (isRangeType)
            {
                // Range attack code here:

                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);

                //
            }
            else
            {
                // Melee attack code here:

                agent.speed *= 20;
                transform.localScale *= 1.2f;

                //
            }
                
            alreadyAttacked = true;

            // Wait with next attack for timeBetweenAttacks
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        if (!isRangeType)
        {
            agent.speed /= 20;
            transform.localScale /= 1.2f;
        }

        alreadyAttacked = false;
    }
}
