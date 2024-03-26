using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimation : MonoBehaviour
{
    private Animator animator;

    [SerializeField]
    private WheelLogic wheel;
    public FrogShopData[] frogShopData;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
        frogShopData = GetComponentsInChildren<FrogShopData>();
        wheel.OnWheelAnim += Wheel_OnWheelAnim;
    }

    private void Wheel_OnWheelAnim(string obj)
    {
        if (obj == "Down")
        {
            animator.SetTrigger("OnStoreMovingDown");
        }
        else animator.SetTrigger("OnStoreMovingUp");
    }

    private void Wheel_OnWheelIconChange(int index)
    {
        //foreach (var sprite in sprites)
        //{
        //    sprite
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
