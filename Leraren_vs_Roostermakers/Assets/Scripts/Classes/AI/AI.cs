using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 
public class AI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    
    [Header("Patrol")]
    public Transform[] points;
    private int destinationPoint = 0;

    [Header("LayerMask")]
    public LayerMask whatIsPlayer;

    [Header("Attack")]
    public float timeBetweenAttacks = 1f;
    bool bAlreadyAttacked;

    [Header("Ranges")]
    public float sightRange, attackRange;
    public bool bPlayerInSightRange, bPlayerInAttackRange;

    [Header("Player References")]
    public Target target; 
    public int damage = 1; 

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        InRange();
    }

    private void InRange()
    {
        bPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        bPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!bPlayerInAttackRange && !bPlayerInSightRange)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoTheNextDestinationPoint();
        }
        if (bPlayerInSightRange && !bPlayerInAttackRange) ChasePlayer();
        if (bPlayerInAttackRange && bPlayerInSightRange) AttackThePlayerInRange();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackThePlayerInRange()
    {
        agent.SetDestination(transform.position);

        if (!bAlreadyAttacked)
        {
            bAlreadyAttacked = true;
            target.TakeDamage(amount: damage);
            Invoke(nameof(ResetTheAttack), timeBetweenAttacks);
        }
    }

    private void ResetTheAttack()
    {
       
        bAlreadyAttacked = false;
    }
    
    
    private void GotoTheNextDestinationPoint()
    {
        if (points.Length == 0)
            return;
        agent.destination = points[destinationPoint].position;
        destinationPoint = (destinationPoint + 1) % points.Length;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
