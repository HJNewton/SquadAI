using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyAfterThisTime;

    private void Start()
    {
        Destroy(gameObject, destroyAfterThisTime);
    }
}
