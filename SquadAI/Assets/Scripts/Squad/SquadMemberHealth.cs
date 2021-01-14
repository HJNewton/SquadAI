using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadMemberHealth : MonoBehaviour
{
    [Header("Squad Member Health Setup")]
    public SquadMemberType memberType;
    public ParticleSystem healParticles;
    public float idleTimeToHeal;
    public float currentHealth;

    [SerializeField] private float timeIdle;

    [SerializeField] SquadMemberCombat squadMemberCombat;
    [SerializeField] SquadMemberBehaviour squadMemberBehaviour;

    private void Awake()
    {
        currentHealth = memberType.health; // Fetches the correct health value for this squad member and assigns to health value

        squadMemberCombat = this.GetComponent<SquadMemberCombat>(); // Get the combat script attached to this agent
        squadMemberBehaviour = this.GetComponent<SquadMemberBehaviour>(); // Get the combat script attached to this agent
    }

    private void Update()
    {
        if (squadMemberCombat.attackState == SquadMemberCombat.AttackState.Idle &&
            !squadMemberBehaviour.navMeshAgent.pathPending &&
            squadMemberBehaviour.navMeshAgent.remainingDistance < squadMemberBehaviour.navMeshAgent.stoppingDistance &&
            (!squadMemberBehaviour.navMeshAgent.hasPath || squadMemberBehaviour.navMeshAgent.velocity.sqrMagnitude == 0f))
        {
            timeIdle += Time.deltaTime;

            if (timeIdle > idleTimeToHeal) // If squad has been idle for the appropriate amount of time to start healing
            {
                Healing(); // Heal 
            }
        }

        else
        {
            timeIdle = 0;

            var hp = healParticles.emission;
            hp.enabled = false; // Set healing particles to inactive
        }
    }

    void Healing()
    {
        currentHealth += Time.deltaTime * memberType.healingPerSecond; // Heals the squad member every frame for a certain amount determined by the squad member type

        var hp = healParticles.emission;
        hp.enabled = true; // Set healing particles to active
    }

    void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake; // Take damage from an enemy
    }
}
