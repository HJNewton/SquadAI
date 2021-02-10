using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [Header("Drops Setup")]
    public GameObject coinPrefab;

    void OnDestroy()
    {
        Instantiate(coinPrefab, transform.position, transform.rotation);
    }
}
