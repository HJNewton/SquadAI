using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMemberCombat : MonoBehaviour
{
    public enum AttackState
    {
        Idle,
        Attacking,
    }

    [Header("Current State")]
    public AttackState attackState; // Current state 

    [Header("Combat Setup")]
    public SquadMemberType memberType; // The squad member type scriptable object
    public Transform firePosition;
    public Transform target;
    [SerializeField] Vector3 fireDirection;

    float timeBetweenShots;

    private void Start()
    {
        attackState = AttackState.Idle;
    }

    private void Update()
    {
        Attack();
        
        timeBetweenShots -= Time.deltaTime; // Reduce time until next available shot
    }

    public void Attack()
    {
        // Is attacking
        if(attackState == AttackState.Attacking && timeBetweenShots <= 0)
        {
            target = GetComponent<SquadMemberBehaviour>().targetedEnemy;
            fireDirection = target.position - firePosition.position;

            Instantiate(memberType.projectile, firePosition.position, Quaternion.LookRotation(fireDirection));
            timeBetweenShots = memberType.fireRate; // Reset fire rate so you don't spam shots
        }
    }
}
