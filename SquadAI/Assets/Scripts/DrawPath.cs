using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrawPath : MonoBehaviour
{
    public NavMeshAgent agentPathToDraw;
    private LineRenderer lr;

    private void Start()
    {
        lr = this.GetComponent<LineRenderer>();
        agentPathToDraw = this.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(agentPathToDraw.hasPath)
        {
            lr.positionCount = agentPathToDraw.path.corners.Length;
            lr.SetPositions(agentPathToDraw.path.corners);
            lr.enabled = true;
        }

        if (!agentPathToDraw.pathPending)
        {
            if (agentPathToDraw.remainingDistance <= agentPathToDraw.stoppingDistance)
            {
                if (!agentPathToDraw.hasPath || agentPathToDraw.velocity.sqrMagnitude == 0f)
                {
                    lr.enabled = false;
                }
            }
        }

    }
}
