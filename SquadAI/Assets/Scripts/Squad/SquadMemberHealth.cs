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

    private void Start()
    {
        if(healParticles.isPlaying)
        {
            healParticles.Stop();
        }
    }

    private void Update()
    {
        if (squadMemberCombat.attackState == SquadMemberCombat.AttackState.Idle &&
            !squadMemberBehaviour.navMeshAgent.pathPending &&
            squadMemberBehaviour.navMeshAgent.remainingDistance < squadMemberBehaviour.navMeshAgent.stoppingDistance &&
            (!squadMemberBehaviour.navMeshAgent.hasPath || squadMemberBehaviour.navMeshAgent.velocity.sqrMagnitude == 0f)) // Basically this absolute monstrosity tests if the member is both in the idle state and the navmesh agent is stopped
        {
            timeIdle += Time.deltaTime;

            if (timeIdle > idleTimeToHeal) // If squad has been idle for the appropriate amount of time to start healing
            {
                if (currentHealth <= memberType.health)
                {
                    Healing(); // Heal 
                }

                if (currentHealth >= memberType.health)
                {
                    if (healParticles.isPlaying)
                    {
                        healParticles.Stop();
                    }
                }
            }
        }

        else
        {
            timeIdle = 0;

            if (healParticles.isPlaying || !healParticles.isPlaying)
            {
                healParticles.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(5f);
        }
    }

    void Healing()
    {
        currentHealth += Time.deltaTime * memberType.healingPerSecond; // Heals the squad member every frame for a certain amount determined by the squad member type

        if (!healParticles.isPlaying)
        {
            healParticles.Play();
        }
    }

    void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake; // Take damage from an enemy
    }
}
