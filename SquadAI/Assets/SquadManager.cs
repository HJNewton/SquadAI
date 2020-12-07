using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    [Header("Squad Spawning")]
    public GameObject memberPrefab;
    public int numberOfMembers;
    public float spawnRange;
    public List<GameObject> allMembers = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < numberOfMembers; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange), 0.5f,
                                                Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange));

            allMembers.Add(Instantiate(memberPrefab, spawnPosition, Quaternion.identity));
        }
    }
}
