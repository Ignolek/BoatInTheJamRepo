using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform Player;
    public Animator animator;
    public Transform ShootPoint;
    public GameObject waveSpawner;
    public Image[] borowankoText;


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
            return;

        agent.destination = Player.transform.position;

        animator.SetBool("Movement", true);
        
        float distance = Vector3.Distance(agent.transform.position, Player.transform.position);

/*        if (distance <= distanceAttackRange && distance > meleeAttackRange)
            rangeAttack();*/
        if (distance <= meleeAttackRange)
            meleeAttack();
    }

    private void rangeAttack()
    {
        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            // Play animation here (probably)
            animator.SetBool("Movement", false);
            animator.SetTrigger("Fire");
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
            animator.SetTrigger("Attack");
            //
            alreadyAttacked = true;
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        var image = Instantiate(borowankoText[(int)Random.Range(0, 3)], transform.Find("Canvas"));
        StartCoroutine(MoveImageUp(image));

        if (CurrentHealth <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    IEnumerator MoveImageUp(Image image)
    {
        float timeToMoveUp = 1.5f;

        while (timeToMoveUp >= 0)
        {
            image.rectTransform.position = new Vector3(
                image.rectTransform.position.x,
                image.rectTransform.position.y + Time.deltaTime * 15f,
                image.rectTransform.position.z
            );

            image.rectTransform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 2f * Time.deltaTime);

            timeToMoveUp -= Time.deltaTime;
            yield return null;
        }

        Destroy(image.gameObject);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
