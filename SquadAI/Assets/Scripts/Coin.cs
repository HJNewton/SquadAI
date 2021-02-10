using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinsToGive = 10;
    public Transform coin;
    public float rotationSpeed;

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
}
