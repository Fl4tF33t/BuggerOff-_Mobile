using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EndPoint : MonoBehaviour
{
    private SphereCollider sphereCollider;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void Start()
    {
        sphereCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        if (other.GetComponent<IPlayerTakeDamage>() != null)
        {
            other.GetComponent<IPlayerTakeDamage>().PlayerTakeDamage(1 /*CancelInvoke put a multiplier to make the damage more*/);          
        }
    }
}
