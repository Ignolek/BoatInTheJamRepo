using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform Player;
    public Transform ShootPoint;

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

    private void rangeAttack()
    {
        rangeAttackCurrentTime -= Time.deltaTime;
        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            // Play animation here (probably)

            //
            if (rangeAttackCurrentTime <= rangeAttackDelay)
            {
                Rigidbody rb = Instantiate(projectile, ShootPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(ShootPoint.transform.forward * projectileSpeed, ForceMode.Impulse);
                rangeAttackCurrentTime = rangeAttackDelay;
                alreadyAttacked = true;
            }
        }
    }
    private void meleeAttack()
    {
        meleeAttackCurrentTime -= Time.deltaTime;
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            // Play animation here (probably)

            //
        }
    }
}
