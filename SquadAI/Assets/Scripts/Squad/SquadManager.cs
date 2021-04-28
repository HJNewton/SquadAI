using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquadManager : MonoBehaviour
{
    public enum Formation
    {
        Grid,
        Line,
        VShape,
    }

    [Header("Current Formation")]
    public Formation formation; // Current state 

    [Header("Squad Spawning")]
    public GameObject memberPrefab;
    public int numberOfMembers;
    public float spawnRange;
    public List<GameObject> allSquadMembers = new List<GameObject>();
    public GameObject[] destinationPoints;

    [Header("Movement Setup")]
    public Camera mainCam;
    public NavMeshAgent navMeshAgent;

    public List<GameObject> coinsInScene = new List<GameObject>();

    private void Awake()
    {
        formation = Formation.Grid;
    }

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        
        destinationPoints = GameObject.FindGameObjectsWithTag("Destination Point");

        numberOfMembers = destinationPoints.Length;

        for (int i = 0; i < numberOfMembers; i++)
        {
            allSquadMembers.Add(Instantiate(memberPrefab, destinationPoints[i].transform.position, Quaternion.identity));
            allSquadMembers[i].GetComponent<SquadMemberBehaviour>().destinationTarget = destinationPoints[i].gameObject.transform;
        }
    }

    private void Update()
    {
        GetNewLocation();

        if (EnemyWaveSpawner.instance.state == EnemyWaveSpawner.SpawnState.BetweenWaves &&
            coinsInScene.Count > 0)
        {
            foreach (GameObject squadMember in allSquadMembers)
            {
                if (squadMember.GetComponent<SquadMemberBehaviour>().moveStates == SquadMemberBehaviour.MemberMovementStates.FollowingSquad)
                {
                    coinsInScene[0].GetComponent<Coin>().squadMemberCollecting = squadMember;

                    squadMember.GetComponent<SquadMemberBehaviour>().moveStates = SquadMemberBehaviour.MemberMovementStates.CollectingCoins;
                    squadMember.GetComponent<SquadMemberBehaviour>().destinationTarget = coinsInScene[0].transform;

                    coinsInScene.RemoveAt(coinsInScene.Count - coinsInScene.Count);
                }
            }
        }

        if (allSquadMembers.Count <= 0)
        {
            GameManagerScript.instance.gameState = GameManagerScript.GameState.GameLost;
        }
    }

    // Functionality for getting a new location to move to.
    void GetNewLocation()
    {
        if (Input.GetMouseButtonDown(0)) // On left click
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition); // Ray from the camera to the mouse position
            RaycastHit hit; // Hit data for our ray

            if (Physics.Raycast(ray, out hit)) // Checks if that ray has hit something
            {
                MoveToLocation(hit);
            }
        }

        //for (int i = 0; i < numberOfMembers; i++)
        //{
        //    allSquadMembers[i].GetComponent<SquadMemberBehaviour>().squadTargetPoint = destinationPoints[i].gameObject.transform;
        //}
    }

    // Functionality for moving to the new location based on hit location
    void MoveToLocation(RaycastHit hit)
    {
        navMeshAgent.SetDestination(hit.point); // Sets new agent destination
    }
}
