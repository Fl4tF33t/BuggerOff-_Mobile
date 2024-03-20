using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ProjectileLogic : MonoBehaviour
{
    public float speed = 10f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {       
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Perform actions when colliding with other objects
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // Destroy the projectile upon collision
        Destroy(gameObject);
    }
}
