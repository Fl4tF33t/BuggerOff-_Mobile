using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bubbles : MonoBehaviour
{
    [SerializeField]private Animator _bubblesAnimator;
    //private Animator _frogBubblesAnimator;

    private float _pauseTime = 0f;

    private const string BUBBLES_ATTACK = "Bubbles_Attack";
    private const string BUBBLES_RELOADING = "Bubbles_Reloading";
    // Start is called before the first frame update
    void Start()
    {
        //_frogBubblesAnimator = GetComponent<Animator>();
        _bubblesAnimator.speed = 0;
       // StartBubblesAttackAnimation();
        //StartCoroutine(TestingAnimation());
    }

    //IEnumerator TestingAnimation()
    //{
    //    yield return new WaitForSeconds(1f);
    //    StartBubblesAttackAnimation();
    //    yield return new WaitForSeconds(1f);
    //    PauseBubblesAnimation();
    //    yield return new WaitForSeconds(1f);
    //    ResumeBubblesAnimation();
    //}

    //public void StartBubblesAttackAnimation()
    //{        
    //    Debug.Log("Start Bubbles Attack Animation");
    //    _bubblesAnimator.speed = 1;
    //    _bubblesAnimator.Play(BUBBLES_ATTACK, 0, 0f);
    //}


    /***** Whenever the Bubbles goes back to idle Invoke PauseBubblesAnimation ******/
    public void PauseBubblesAnimation()
    {
        Debug.Log("Pause Bubbles Attack Animation");
        _pauseTime = _bubblesAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        _bubblesAnimator.speed = 0;
    }

    /***** Whenever tracking goes to attack Invoke ResumeBubblesAnimation *********/
    public void ResumeBubblesAnimation()
    {
        Debug.Log("Resume Bubbles Attack Animation");
        _bubblesAnimator.speed = 1;
        _bubblesAnimator.Play(BUBBLES_ATTACK, 0, _pauseTime);
    }

    public void Reload()
    {
        Debug.Log("Reloading Bubbles");
        _bubblesAnimator.speed = 1;
        _bubblesAnimator.Play(BUBBLES_RELOADING, 0, 0f);
    }

    public void PauseAnimator()
    {
        _bubblesAnimator.speed = 0;
    }
   
}
