using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Squad Member Type")]
public class SquadMemberType : ScriptableObject
{
    [Header("Type")]
    public bool wizard;
    public bool ranger;
    public bool healer;

    [Header("Stats")]
    public float health;
    public float damage;
    public float attackRange;
    public float fireRate;
    public float healingPerSecond;

    [Header("Visuals")]
    public GameObject projectile;
    // public ParticleSystem attackParticles;
    // public ParticleSystem damagedParticles;
    // public ParticleSystem healParticles;
}
