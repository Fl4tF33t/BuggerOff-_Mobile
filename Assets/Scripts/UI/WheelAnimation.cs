using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimation : MonoBehaviour
{
    private Animator animator;

    public FrogShopData[] frogShopData;
    private int currentIndex;

    private Transform wheelChild;

    // Start is called before the first frame update
    private void Awake()
    {
        wheelChild = transform.GetChild(0);
        animator = GetComponentInParent<Animator>();

        frogShopData = GetComponentsInChildren<FrogShopData>();
    }
    private void Start()
    {
        WheelManager.Instance.OnWheelAnim += Wheel_OnWheelAnim;        
    }

   

    private void Wheel_OnWheelAnim(string obj, int index)
    {
        wheelChild.gameObject.SetActive(true);
        currentIndex = index - 1;
        if (currentIndex < 0)
        {
            currentIndex = ShopManager.Instance.frogPool.Length - 1;
        }
        for (int i = 0; i < frogShopData.Length; i++)
        {
            frogShopData[i].SetFrogSO(ShopManager.Instance.frogPool[currentIndex]);
            currentIndex = (currentIndex + 1) % ShopManager.Instance.frogPool.Length;
        }
        if (obj == "Down")
        {
            animator.SetTrigger("OnStoreMovingDown");
        }
        else animator.SetTrigger("OnStoreMovingUp");
    }

}
