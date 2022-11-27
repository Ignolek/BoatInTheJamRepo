using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    //"Actors"
    private NavMeshAgent agent;
    public Transform Player;
    public Transform ShootPoint;
    public Animator animator;

    private Vector3 initialPosition;
    //public Collider arenaCollider;
    public bool isPlayerOnStage;

    //Params
    public float Health;
    public float CurrentHealth;
    public float DamagePerHit;
    public float timeBetweenAttacks;
    
    public bool isRangeType;
    public GameObject projectile;
    public float attackRange;
    public float projectileSpeed;
    public bool isDead;

    //Attack
    bool alreadyAttacked;
    bool charge;

    private void Awake()
    {
        CurrentHealth = Health;
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //TakeDamage(10f * Time.deltaTime);
        agent.destination = Player.transform.position;
        float distance = Vector3.Distance(agent.transform.position, Player.transform.position);

        if (distance <= attackRange)
            Attack();
        else
        {
            if (animator != null)
                animator.SetBool("Movement", true);
        }
    }

    private void Attack()
    {
        if (animator != null)
            animator.SetBool("Movement", false);

        // Make sure enemy doesn't move
        if (isRangeType)
            agent.SetDestination(transform.position);

        transform.LookAt(Player.transform.position + new Vector3(0, 1, 0));

        if (!alreadyAttacked)
        {
            if (isRangeType)
            {
                // Range attack code here:
                if (animator != null)
                    animator.SetTrigger("Shoot");
            }
            else
            {
                // Melee attack code here:
                agent.speed *= 50;
                transform.localScale *= 1.2f;

                charge = true;
            }
                
            alreadyAttacked = true;

            // Wait with next attack for timeBetweenAttacks
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void Shoot()
    {
        Rigidbody rb = Instantiate(projectile, ShootPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(ShootPoint.transform.forward * projectileSpeed, ForceMode.Impulse);
        rb.transform.rotation = transform.rotation;
    }

    private void ResetAttack()
    {
        if (!isRangeType)
        {
            // DEBUG
            agent.speed /= 50;
            transform.localScale /= 1.2f;
        }

        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (animator != null)
            animator?.SetTrigger("Damage");

        if (CurrentHealth <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(charge == true)
        {
            if (collision.other.CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>().TakeDamage();
            }
        }
    }
}
