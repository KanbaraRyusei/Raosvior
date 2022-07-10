using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/CreatePlayerHandAsset")]
public class PlayerHand : ScriptableObject
{
    public RSP Hand => _rSP;
    public string CardName => _cardName;

    [SerializeField]
    [Header("じゃんけんの手")]
    RSP _rSP = RSP.Rock;

    [SerializeField]
    [Header("カード名")]
    string _cardName;

    [SerializeField]
    [Header("効果名")]
    [Multiline]
    string _cardEffect;

    [SerializeField]
    [Header("効果")]
    HandEffect _handEffect;

    public enum RSP
    {
        [Tooltip("グー")]
        Rock,
        [Tooltip("チョキ")]
        Scissors,
        [Tooltip("パー")]
        Paper
    }
}
