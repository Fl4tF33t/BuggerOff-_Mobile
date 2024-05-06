using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EndPoint : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        boxCollider.isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Brain"))
        {
            int num = other.transform.childCount;
            for (int i = 0; i < num; i++)
            {
                other.transform.GetChild(i).gameObject.SetActive(false);
            }
            other.GetComponent<SphereCollider>().enabled = false;
        }
        else
        {
            other.gameObject.SetActive(false);
        }

        if (other.GetComponent<IPlayerTakeDamage>() != null)
        {
            other.GetComponent<IPlayerTakeDamage>().PlayerTakeDamage(1 /*CancelInvoke put a multiplier to make the damage more*/);          
        }
    }
}
