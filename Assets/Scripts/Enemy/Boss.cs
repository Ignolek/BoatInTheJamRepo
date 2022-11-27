using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform Player;
    public Animator animator;
    public Transform ShootPoint;
    public GameObject waveSpawner;

    //Params
    public float Health;
    public float CurrentHealth;
    public float timeBetweenAttacks;
    public float meleeAttackRange;
    public float distanceAttackRange;
    public GameObject projectile;
    public float projectileSpeed;

    bool alreadyAttacked;

    // Animation delays
    public float rangeAttackDelay;
    private float rangeAttackCurrentTime;

    public float meleeAttackDelay;
    private float meleeAttackCurrentTime;

    private void Awake()
    {
        CurrentHealth = Health;
        Player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("NO ANIMATOR ON BOSS!");
    }

    // Start is called before the first frame update
    void Start()
    {
        rangeAttackCurrentTime = rangeAttackDelay;
        meleeAttackCurrentTime = meleeAttackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (!waveSpawner.GetComponent<WaveSpawner>().inCombat)
        {
            // Idle animation
            animator.SetBool("Idle", true);
        }
        else
        {
            animator.SetBool("Movement", true);
            animator.SetBool("Idle", false);
            agent.destination = Player.transform.position;
            float distance = Vector3.Distance(agent.transform.position, Player.transform.position);

            if (distance <= distanceAttackRange && distance > meleeAttackRange)
            {
                rangeAttack();
            }
            else if (distance <= meleeAttackRange)
            {
                meleeAttack();
            }
        }
    }

    private void rangeAttack()
    {
        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            // Play animation here (probably)
            animator.SetBool("Movement", false);
            animator.SetTrigger("Shoot");
            //

            Rigidbody rb = Instantiate(projectile, ShootPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(ShootPoint.transform.forward * projectileSpeed, ForceMode.Impulse);
            rangeAttackCurrentTime = rangeAttackDelay;
            alreadyAttacked = true;
        }
    }
    private void meleeAttack()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            // Play animation here (probably)
            animator.SetBool("Movement", false);
            animator.SetTrigger("Damage");
            //
            alreadyAttacked = true;
        }
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
}
