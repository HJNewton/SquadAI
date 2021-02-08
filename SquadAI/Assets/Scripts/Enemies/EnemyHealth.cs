using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health Setup")]
    public float maxHealth;
    public float currentHealth;
    public bool takingDOTS;

    private int dotsTaken = 0;

    [Header("Enemy Health Bar Setup")]
    public Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;

        StartCoroutine("TakeDOTS");
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (dotsTaken >= 5)
        {
            dotsTaken = 0;

            takingDOTS = false;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
    }

    public IEnumerator TakeDOTS()
    {
        while (true)
        {
            if (takingDOTS)
            {
                Debug.Log("Taking Damage");

                currentHealth -= Random.Range(3, 5);

                dotsTaken++;

                yield return new WaitForSeconds(1);

                Debug.Log("Damage Taken, Now at: " + dotsTaken);
            }
        }
    }
}
