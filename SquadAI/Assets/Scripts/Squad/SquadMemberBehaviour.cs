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
    }

    // Functionality for setting the attack state
    void SetAttackState()
    {
        if(targetedEnemy)
        {
            squadCombat.attackState = SquadMemberCombat.AttackState.Attacking;

            // IMPROVE THIS
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) // Cast out a ray to ensure the enemy can be hit, if it can't then the member will move to a spot where they can
            //{
            //    Debug.DrawRay(transform.position, transform.forward * 100);

            //    if (hit.transform.gameObject.CompareTag("Enemy"))
            //    {
            //        //navMeshAgent.isStopped = true;
            //    }

            //    if (!hit.transform.gameObject.CompareTag("Enemy"))
            //    {
            //        squadCombat.attackState = SquadMemberCombat.AttackState.Idle;
            //        //navMeshAgent.isStopped = false;
            //    }
            //}
        }

        if(targetedEnemy == null)
        {
            squadCombat.attackState = SquadMemberCombat.AttackState.Idle;
            navMeshAgent.isStopped = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, memberType.attackRange);
    }

    private void OnDestroy()
    {
        squadManager.allSquadMembers.Remove(this.gameObject);
    }
}
