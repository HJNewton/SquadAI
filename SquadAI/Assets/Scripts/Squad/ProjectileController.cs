using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Setup")]
    public float speed;
    public float destroyTime;
    public GameObject hitParticles;
    public SquadMemberType memberType;

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
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(memberType.damage); // Deals damage to hit enemies
        //    Instantiate(hitParticles, transform.position, transform.rotation);
        //    Destroy(gameObject);
        //}

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, memberType.damageRange); // Get all colliders in a certain radius

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Enemy"))
            {
                hitCollider.gameObject.GetComponent<EnemyHealth>().TakeDamage(memberType.damage);
                Instantiate(hitParticles, transform.position, transform.rotation);
            }
        }

        Destroy(gameObject);
    }
}
