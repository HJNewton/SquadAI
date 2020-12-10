using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Setup")]
    public float speed;

    Rigidbody rb;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Moving();
    }

    void Moving()
    {
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }
}
