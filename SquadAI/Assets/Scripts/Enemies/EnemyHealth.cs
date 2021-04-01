using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health Setup")]
    public float maxHealth;
    public float currentHealth;
    public GameObject coinPrefab;

    [Header("Enemy Health Bar Setup")]
    public Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Instantiate(coinPrefab, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;
    }
}
