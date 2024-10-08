using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "ScriptableObjects/Frogs/AudioSO", order = 3)]
public class AudioSO : ScriptableObject
{
    //This scriptable object is in charge for everything audio related to the frogSO
    [Header("AUDIO")]
    public Audio[] _spawn;
    public Audio jump;
    public Audio attack;
    public Audio _ninja2ndAttack;
    public Audio _missingSFX;
    //here you can add other audio types that are required, for example, placing and purchasing

    [Serializable] 
    public struct Audio
    {
        public AudioClip _audioClip;
        [Range(0f, 1f)]
        public float volume;
        //Add any other variables that need to be tweeked for each sound 
    }
}
