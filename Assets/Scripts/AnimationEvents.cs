using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public event Action OnEndAnim;
    public event Action OnDamageLogic;
    public void OnEndAnimation()
    {
        OnEndAnim?.Invoke();
    }

    public void OnStartLogic()
    {
        OnDamageLogic?.Invoke();
    }
}
