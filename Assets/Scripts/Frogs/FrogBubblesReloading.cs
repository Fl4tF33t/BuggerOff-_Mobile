using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBubblesReloading : MonoBehaviour
{
    private Animator _frogBubblesAnimator;
    [SerializeField]private Bubbles _bubbles;

    private void Start()
    {
        _frogBubblesAnimator = transform.parent.GetComponent<Animator>();
    }

    public void PlayReloadingAnimation()
    {
        _frogBubblesAnimator.SetBool("OnReloading", true);
        _bubbles.Reload();

    }
    public void StopReloadingAnimation()
    {
        Debug.Log("Stop Reloading Animation");
        _frogBubblesAnimator.SetBool("OnReloading", false);
        _bubbles.PauseAnimator();
    }
}
