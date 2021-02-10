using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]Vector3 targetDestination;

    NavMeshAgent navMeshAgent;
    EnemyCombat enemyCombat;

    float speed;

    private void Awake()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        enemyCombat = this.GetComponent<EnemyCombat>();
        speed = navMeshAgent.speed;
        SetDestination();
    }

    private void Update()
    {
        if(enemyCombat.attackState == EnemyCombat.AttackState.Attacking)
        {
            //navMeshAgent.isStopped = true;
            navMeshAgent.speed = speed / 2;
        }

        else
        {
            navMeshAgent.speed = speed;
            navMeshAgent.isStopped = false;
        }
    }

    Vector3 GenerateNewDestination()
    {
        //if (!navMeshAgent.hasPath)
        //{
        //    NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        //    // Pick the first indice of a random triangle in the nav mesh
        //    int indice = Random.Range(0, navMeshData.indices.Length - 3);

        //    // Select a random point on it
        //    Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[indice]], navMeshData.vertices[navMeshData.indices[indice + 1]], Random.value);
        //    Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[indice + 2]], Random.value);

        //    targetDestination = point;
        //}

        targetDestination = GameObject.FindGameObjectWithTag("Portal").transform.position;

        return targetDestination;
    }

    void SetDestination()
    {
        navMeshAgent.SetDestination(GenerateNewDestination()); // Sets new agent destination
    }
}
