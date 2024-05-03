using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public event Action OnEndAnim;
    public event Action OnDamageLogic;

    private AudioSource _audiosource;
    private FrogBrain _frogBrain;

    private const string FORG = "Forg";
    private const string SUNGLASSES = "Sunglasses";
    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();        
    }
    private void Start()
    {
        _frogBrain = transform.parent.GetComponentInParent<FrogBrain>();

        _frogBrain.OnFrogSpawned += PlaySpawnSFX;

    }
    public void OnEndAnimation()
    {
        OnEndAnim?.Invoke();
    }

    public void OnStartLogic()
    {
        OnDamageLogic?.Invoke();
    }

    public void PlayAttackSFX()
    {
        AudioClip frogAttack = _frogBrain.frogSO.audioSO.attack._audioClip;

        if (frogAttack == null)
        {
            frogAttack = _frogBrain.frogSO.audioSO._missingSFX._audioClip;
            Debug.Log("Attack SFX is missing");
        }

        _audiosource.PlayOneShot(frogAttack);
    }

    public void PlayJumpSFX()
    {
        AudioClip frogJump = _frogBrain.frogSO.audioSO.jump._audioClip;

        if (frogJump == null)
        {
            frogJump = _frogBrain.frogSO.audioSO._missingSFX._audioClip;
            Debug.Log("Jump SFX is missing");
        }

        _audiosource.PlayOneShot(frogJump);
    }

    public void PlaySpawnSFX()
    {
        Debug.Log("Spawn SFX");
        AudioClip frogSpawnSFX = null;
        if (_frogBrain.frogSO.logicSO.frogName == SUNGLASSES || _frogBrain.frogSO.logicSO.frogName == FORG)
        {
            int spwanIndexLenght = _frogBrain.frogSO.audioSO._spawn.Length;
            int randomIndex = UnityEngine.Random.Range(0, spwanIndexLenght);
            frogSpawnSFX = _frogBrain.frogSO.audioSO._spawn[randomIndex]._audioClip;
        }
        else
        {
            frogSpawnSFX = _frogBrain.frogSO.audioSO._spawn[0]._audioClip;
        }

        if (frogSpawnSFX == null)
        {
            frogSpawnSFX = _frogBrain.frogSO.audioSO._missingSFX._audioClip;
            Debug.Log("Spawn SFX is missing");
        }

        _audiosource.PlayOneShot(frogSpawnSFX);

        _frogBrain.OnFrogSpawned -= PlaySpawnSFX;
    }


}
