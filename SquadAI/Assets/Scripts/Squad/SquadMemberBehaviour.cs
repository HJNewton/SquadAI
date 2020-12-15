using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadMemberBehaviour : MonoBehaviour
{
    [Header("Squad Member Movement Setup")]
    public Camera mainCam;
    public NavMeshAgent navMeshAgent;
    public Transform destinationTarget;

    [Header("Squad Member Combat Setup")]
    public SquadMemberType memberType;
    public Transform targetedEnemy;

    SquadManager squadManager;
    SquadMemberCombat squadCombat;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        squadManager = GameObject.FindGameObjectWithTag("Squad Manager").GetComponent<SquadManager>();

        squadCombat = this.GetComponent<SquadMemberCombat>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        SetTarget();
        MoveToDestination();

        SetAttackState();

        if (targetedEnemy)
        {
            FaceTarget();
        }
    }

    // Functionality for moving to target destination
    void MoveToDestination()
    {
        navMeshAgent.SetDestination(destinationTarget.transform.position); // Set's new agent destination
    }

    // Functionality to detect enemies in a certain radius
    void SetTarget()
    { 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, memberType.attackRange); // Get all colliders in a certain radius

        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.CompareTag("Enemy")) // Check if an enemy is in range
            {
                if(targetedEnemy)
                {
                    if(Vector3.Distance(transform.position, hitCollider.transform.position) < Vector3.Distance(transform.position, targetedEnemy.position))
                    {
                        targetedEnemy = hitCollider.gameObject.transform; // Set the enemy to the target if they are closer than previous target
                    }
                }

                if (!targetedEnemy)
                {
                    targetedEnemy = hitCollider.gameObject.transform; // Set the enemy to the target if the agent doesn't have any target already
                }
            }
        }

        if (targetedEnemy)
        {
            if (Vector3.Distance(transform.position, targetedEnemy.position) > memberType.attackRange)
            {
                targetedEnemy = null; // Sets target to null if agent goes out of range
            }
        }
    }

    // Functionality to face the targeted enemy
    void FaceTarget()
    {
        Vector3 targetDirection = targetedEnemy.position - transform.position; // Gets direction to face in
        float rotationSpeed = navMeshAgent.angularSpeed * Time.deltaTime; // Sets rotation speed

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed, 0.0f); // Generates a new direction to face 
        
        transform.rotation = Quaternion.LookRotation(newDirection); // Applies new rotation

        Debug.DrawRay(transform.position, newDirection * 100);
    }

    // Functionality for setting the attack state
    void SetAttackState()
    {
        if(targetedEnemy)
        {
            squadCombat.attackState = SquadMemberCombat.AttackState.Attacking;
        }

        if(targetedEnemy == null)
        {
            squadCombat.attackState = SquadMemberCombat.AttackState.Idle;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, memberType.attackRange);
    }
}
