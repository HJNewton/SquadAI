using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinsToGive = 10;
    public Transform coin;
    public float rotationSpeed;
    public GameObject squadMemberCollecting;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("Squad Manager").GetComponent<SquadManager>().coinsInScene.Add(this.gameObject);
    }

    private void Update()
    {
        coin.transform.Rotate(Vector3.one * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Squad Member"))
        {
            GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManagerScript>().coinsTotal += coinsToGive;

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (squadMemberCollecting != null)
        {
            squadMemberCollecting.GetComponent<SquadMemberBehaviour>().moveStates = SquadMemberBehaviour.MemberMovementStates.FollowingSquad;
            squadMemberCollecting.GetComponent<SquadMemberBehaviour>().destinationTarget = squadMemberCollecting.GetComponent<SquadMemberBehaviour>().squadTargetPoint;
        }

        if (squadMemberCollecting == null)
        {
            GameObject.FindGameObjectWithTag("Squad Manager").GetComponent<SquadManager>().coinsInScene.Remove(this.gameObject);
        }
    }
}
