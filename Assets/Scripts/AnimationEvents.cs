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
    private const string PROJECTILE_TAG = "Projectile";
    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();        
    }
    private void Start()
    {
        if(gameObject.tag == PROJECTILE_TAG)
            return;

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
        if (_frogBrain.frogSO.logicSO.frogName == "Cannon")
        {
            Debug.Log("Cannon attack");
            _audiosource.Stop();
        }
        AudioClip frogAttack = _frogBrain.frogSO.audioSO.attack._audioClip;

        if (frogAttack == null)
        {
            frogAttack = _frogBrain.frogSO.audioSO._missingSFX._audioClip;
            Debug.Log("Attack SFX is missing");
        }

        _audiosource.PlayOneShot(frogAttack);
        Debug.Log("Suppppp");
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

    public void BoomSfx()
    {
        _audiosource.Play();
    }

    public void NinjaRange()
    {
        AudioClip frogNinjaRange = _frogBrain.frogSO.audioSO._ninja2ndAttack._audioClip;
        _audiosource.PlayOneShot(frogNinjaRange);
    }

    public void NinjaClose()
    {
        AudioClip frogNinjaClose = _frogBrain.frogSO.audioSO.attack._audioClip;
        _audiosource.PlayOneShot(frogNinjaClose);
    }

    public void CannonFuse() 
    { 
        Debug.Log("Cannon Fuse");
        AudioClip frogCannonFuse = _frogBrain.frogSO.audioSO._ninja2ndAttack._audioClip;
        _audiosource.clip = frogCannonFuse;
        _audiosource.Play();
    }


}
