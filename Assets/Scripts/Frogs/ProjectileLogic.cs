using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLogic : MonoBehaviour
{
    public float speed = 150f;
    Rigidbody rb;

    public int damage;
    Projectile projectileType;
    enum Projectile
    {
        Harpoon,
        Cannon,
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (this.gameObject.name.Contains("Harpoon"))
        {
            projectileType = Projectile.Harpoon;
        }else projectileType = Projectile.Cannon;
    }

    private void Update()
    {       
        rb.velocity = transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (projectileType)
        {
            case Projectile.Harpoon:
                if (collision.gameObject.TryGetComponent(out IBugTakeDamage bugDamage))
                {
                    bugDamage.BugTakeDamage(damage);
                }
                break;
            case Projectile.Cannon:
                Collider[] cols = Physics.OverlapSphere(transform.position, 0.6f, LayerMask.GetMask("Bug"));
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].gameObject.TryGetComponent(out IBugTakeDamage AOEDamage))
                    {
                        AOEDamage.BugTakeDamage(damage);
                    }
                }
                break;
        }
        // Destroy the projectile upon collision
        Destroy(gameObject);
    }
}
