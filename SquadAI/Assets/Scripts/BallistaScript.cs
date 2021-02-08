using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaScript : MonoBehaviour
{
    [Header("Ballista Setup")]
    public GameObject boltPrefab;
    public GameObject rotator;
    public Transform firePoint;
    public Transform target;
    public float ballistaRange;
    public float rotationSpeed;
    public float fireRate;
    private float timeUntilNextShot;

    Vector3 lookRotation;

    private void Update()
    {
        timeUntilNextShot += Time.deltaTime;

        SelectTarget();

        if (target)
        {
            TrackTarget();
        }

        if (timeUntilNextShot >= fireRate && target)
        {
            Fire();

            timeUntilNextShot = 0;
        }
    }

    void SelectTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ballistaRange); // Get all colliders in a certain radius

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy")) // Check if a squad member is in range
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
            if (Vector3.Distance(transform.position, target.position) > ballistaRange)
            {
                target = null; // Sets target to null if target goes out of range
            }
        }
    }

    void TrackTarget()
    {
        Vector3 targetDirection = target.position - rotator.transform.position; // Gets direction to face in
        //float rotationSpeed = navMeshAgent.angularSpeed * Time.deltaTime; // Sets rotation speed
        float rotateSpeed = rotationSpeed * Time.deltaTime; // Sets rotation speed

        Vector3 newDirection = Vector3.RotateTowards(rotator.transform.forward, targetDirection, rotateSpeed, 0.0f); // Generates a new direction to face 

        rotator.transform.rotation = Quaternion.LookRotation(newDirection); // Applies new rotation
    }

    void Fire()
    {
        lookRotation = target.position - firePoint.position; // Get direction to fire in
        
        Instantiate(boltPrefab, firePoint.position, Quaternion.LookRotation(lookRotation));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, ballistaRange);
    }
}
