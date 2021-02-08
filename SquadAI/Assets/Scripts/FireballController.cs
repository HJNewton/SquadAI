using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    [Header("Projectile Setup")]
    public float speed;
    public float damage;
    public float damageRange;
    public float destroyTime;
    //public GameObject hitParticles;

    Rigidbody rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        Destroy(gameObject, destroyTime);
    }

    private void FixedUpdate()
    {
        Moving();
    }

    void Moving()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, damageRange); // Get all colliders in a certain radius

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                hitCollider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);

                hitCollider.gameObject.GetComponent<EnemyHealth>().takingDOTS = true;
            }
        }

        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //Instantiate(hitParticles, transform.position, transform.rotation);
    }
}
