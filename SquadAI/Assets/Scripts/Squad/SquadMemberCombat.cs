﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMemberCombat : MonoBehaviour
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
            Instantiate(memberType.projectile, transform.position, transform.rotation);
            timeBetweenShots = memberType.fireRate; // Reset fire rate so you don't spam shots
        }
    }
}