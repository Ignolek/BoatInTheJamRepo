using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombEnemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform Player;
    public float ExplosionDelay;

    // Params
    public float Health;
    public float CurrentHealth;
    public float attackRange;
    public float explosionRange;

    public bool alreadyAttacked;

    private float distance;

    private void Awake()
    {
        CurrentHealth = Health;
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alreadyAttacked)
        {
            agent.destination = Player.transform.position;
            ExplosionDelay -= Time.deltaTime;
            Debug.Log(ExplosionDelay);

            //agent.transform.localScale *= 0.1f * Time.deltaTime;
            if (ExplosionDelay <= 0 || CurrentHealth <= 0)
                DestroyEnemy();
        }

        distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance <= attackRange)
        {
            alreadyAttacked = true;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
            Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        // Here BUUUUM code
        Debug.Log("BUUUUUUUUM");

        // Play particles

        //

        if (distance <= explosionRange)
        {
            Player.GetComponent<HealthSystem>().TakeDamage();

        }
        //
        Destroy(gameObject);
    }
}
