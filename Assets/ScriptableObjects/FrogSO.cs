using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrogSO", menuName = "ScriptableObjects/FrogSO", order = 1)]
public class FrogSO : ScriptableObject
{
    [Header("Static Values")]

    public GameObject prefab;

    [Header("LOGIC")]
    public Logic baseLogic;

    [Header("VISUAL")]
    public Animator animatorController;
    public BaseAudioVisual<Animation> baseAnimation;
    public Sprite UISprite;
    public string UIShopTextInfo;

    [Header("AUDIO")]
    public BaseAudioVisual<AudioClip> baseAudio;

    [Header("UPGRADE")]
    public Upgrade<int> discipline;
    public Upgrade<int> damage;
    public Upgrade<float> range;
    public Upgrade<float> attackSpeed;
    public enum UpgradeAmount { Zero, One, Two, Three }

    [Serializable]
    public struct Logic
    {
        public string frogName;
        [Range(50, 500)]
        public int cost;
        [Range(0, 6)]
        public int discipline;
        public int damage;
        public float range;
        public float attackSpeed;
        public LayerMask targetLayer;
    }
    [Serializable]
    public struct Upgrade<T>
    {
        public T max;
        public T amount;
        public UpgradeAmount increment;
    }
    [Serializable]
    public struct BaseAudioVisual<T>
    {
        public T idle;
        public T jump;
        public T attack;
        public T lure;
    }

}
