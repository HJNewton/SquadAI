using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadMemberBehaviour : MonoBehaviour
{
    [Header("Squad Member Movement Setup")]
    public Camera mainCam;
    public NavMeshAgent navMeshAgent;
    //public bool squadLeader;
    //public Transform SL;
    //public Vector3 offset;
    //public float smoothSpeed;

    [Header("Squad Member Combat Setup")]
    public float combatRadius;
    public Transform targetedEnemy;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        GetNewLocation();
        SetTarget();

        if (targetedEnemy)
        {
            FaceTarget();
            AttackEnemy();
        }
    }    

    // Functionality for getting a new location to move to.
    void GetNewLocation()
    {
        if (Input.GetMouseButtonDown(0)) // On left click
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition); // Ray from the camera to the mouse position
            RaycastHit hit; // Hit data for our ray

            if (Physics.Raycast(ray, out hit)) // Checks if that ray has hit something
            {
                MoveToLocation(hit);            }
        }
    }

    // Functionality for moving to the new location based on hit location
    void MoveToLocation(RaycastHit hit)
    {
        navMeshAgent.SetDestination(hit.point); // Set's new agent destination
    }

    // Functionality to detect enemies in a certain radius
    void SetTarget()
    { 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, combatRadius); // Get all colliders in a certain radius

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
            if (Vector3.Distance(transform.position, targetedEnemy.position) > combatRadius)
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

    // Functionality for attacking targeted enemy
    void AttackEnemy()
    {
       // Attacking functionality
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, combatRadius);
    }
}
