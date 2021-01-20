using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [Header("Projectile Setup")]
    public float speed;
    public float destroyTime;
    public float damage;
    public GameObject hitParticles;

    Rigidbody rb;

    private void Start()
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
        if (collision.gameObject.CompareTag("Squad Member"))
        {
            collision.gameObject.GetComponent<SquadMemberHealth>().TakeDamage(damage);
            Instantiate(hitParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
