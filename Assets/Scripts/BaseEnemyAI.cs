using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer;
    [SerializeField] private float health;
    [SerializeField] private GameObject spear;
    [SerializeField] private Animator anim;


    [Header("Patrolling")]
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    [Header("Attacking")]
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private bool alreadyAttacked;

    [Header("States")]
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            Patrolling();

        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();

        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();
    }

    private void Patrolling() 
    {
        if (!walkPointSet)
            SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint() 
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer() 
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer() 
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);

        if (!alreadyAttacked) 
        {

            alreadyAttacked = true;
            Invoke(nameof(ResetAttacks), timeBetweenAttacks);
        }
    }

    private void ResetAttacks() 
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;

        if (health <= 0)
        {
            anim.SetBool("hasDied", true);
            playerInAttackRange = false;
            playerInSightRange = false;
            agent.speed = 0;
        }
    }

    public void DestroyEnemy() 
    {
        spear.transform.parent = null;
        spear.GetComponent<Rigidbody>().isKinematic = false;
        spear.GetComponent <CapsuleCollider>().enabled = true;
        Destroy(gameObject);
    }
}
