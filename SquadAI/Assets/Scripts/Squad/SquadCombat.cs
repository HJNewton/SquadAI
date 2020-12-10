using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadCombat : MonoBehaviour
{
    public enum AttackState
    {
        Idle,
        Attacking,
        Healing,
    }

    [Header("Current State")]
    public AttackState attackState; // Current state 

    [Header("Combat Setup")]
    public SquadMemberType memberType; // The squad member type scriptable object

    private void Start()
    {
        attackState = AttackState.Idle;
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        // Is attacking
        if(attackState == AttackState.Attacking)
        {
            Instantiate(memberType.projectile, transform.position, Quaternion.Euler(transform.forward));
        }
    }
}
