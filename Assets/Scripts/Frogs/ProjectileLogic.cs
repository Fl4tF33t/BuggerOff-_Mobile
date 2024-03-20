using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ProjectileLogic : MonoBehaviour
{
    public float speed = 10f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Perform actions when colliding with other objects
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // Destroy the projectile upon collision
        Destroy(gameObject);
    }
}
