using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public enum AttackState
    {
        Idle,
        Attacking,
    }

    [Header("Current State")]
    public AttackState attackState; // Current state 

    [Header("Enemy Combat Settings")]
    public float awarenessRange;
    public float fireRate;
    public float rotationSpeed;
    public Transform firePosition;
    public Transform target;
    public GameObject projectile;

    [SerializeField ]float timeBetweenShots;
    Vector3 fireDirection;

    private void Update()
    {
        SetTarget();
        SetAttackState();

        Attack();

        if (target)
        {
            FaceTarget();
        }
    }

    void SetTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, awarenessRange); // Get all colliders in a certain radius

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Squad Member")) // Check if a squad member is in range
            {
                if (target)
                {
                    if (Vector3.Distance(transform.position, hitCollider.transform.position) < Vector3.Distance(transform.position, target.position))
                    {
                        target = hitCollider.gameObject.transform; // Set the new target if they are closer than previous target
                    }
                }

                if (!target)
                {
                    target = hitCollider.gameObject.transform; // Set the target if the enemy doesn't have a target already
                }

            }
        }

        if (target)
        {
            if (Vector3.Distance(transform.position, target.position) > awarenessRange)
            {
                target = null; // Sets target to null if target goes out of range
            }
        }
    }

    void FaceTarget()
    {
        Vector3 targetDirection = target.position - transform.position; // Gets direction to face in
        //float rotationSpeed = navMeshAgent.angularSpeed * Time.deltaTime; // Sets rotation speed
        float rotateSpeed = rotationSpeed * Time.deltaTime; // Sets rotation speed

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotateSpeed, 0.0f); // Generates a new direction to face 

        transform.rotation = Quaternion.LookRotation(newDirection); // Applies new rotation
    }

    void SetAttackState()
    {
        if (target)
        {
            attackState = AttackState.Attacking;
        }

        if (target == null)
        {
            attackState = AttackState.Idle;
        }
    }

    public void Attack()
    {
        // Is attacking
        if (attackState == AttackState.Attacking && timeBetweenShots <= 0)
        {
            fireDirection = target.position - firePosition.position; // Get direction to fire in

            Instantiate(projectile, firePosition.position, Quaternion.LookRotation(fireDirection)); // Spawn projectile
            timeBetweenShots = fireRate; // Reset fire rate so you don't spam shots
        }

        timeBetweenShots -= Time.deltaTime; // Reduce time until next available shot
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, awarenessRange);
    }
}
