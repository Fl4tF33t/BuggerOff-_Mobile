using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLogic : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private Animator animator;

    public int damage;
    Projectile projectileType;
    enum Projectile
    {
        Harpoon,
        Cannon,
        Kunai,
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (this.gameObject.name.Contains("Cannon"))
        {
            projectileType = Projectile.Cannon;
            animator = GetComponent<Animator>();
        } else if (this.gameObject.name.Contains("Harpoon")) 
        {
            projectileType = Projectile.Harpoon; 
        }
        else if (this.gameObject.name.Contains("Kunai"))
        {
            projectileType = Projectile.Kunai;
        }

    }
     
    private void Start()
    {       
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
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
                Destroy(gameObject);
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
                animator.SetTrigger("OnExplode");
                break;
            case Projectile.Kunai:
                if (collision.gameObject.TryGetComponent(out IBugTakeDamage kunaiDamage))
                {
                    kunaiDamage.BugTakeDamage(damage);
                }
                Destroy(gameObject);
                break;
        }
        // Destroy the projectile upon collision
        rb.isKinematic = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void EndOfAnim()
    {
        Destroy(gameObject);
    }
}
