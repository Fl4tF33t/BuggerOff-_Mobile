using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLogic : MonoBehaviour
{
    private float speed = 5f;
    private Rigidbody rb;
    private Animator animator;

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
        if (this.gameObject.name.Contains("Cannon"))
        {
            projectileType = Projectile.Cannon;
            animator = GetComponent<Animator>();
        }else projectileType = Projectile.Harpoon;
        
    }
     
    private void Start()
    {       
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        StartCoroutine(SelfDestruct());
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
        rb.isKinematic = true;
        animator.SetTrigger("OnExplode");
    }

    public void EndOfAnim()
    {
        Destroy(gameObject);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        EndOfAnim();
    }
}
