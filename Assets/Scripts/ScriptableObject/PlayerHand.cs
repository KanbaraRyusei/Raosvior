using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/CreatePlayerHandAsset")]
public class PlayerHand : ScriptableObject
{
    public RSPParam Hand => _rSP;
    public string CardName => _cardName;
    public string CardEffect => _cardEffect;
    public HandEffect HandEffect => _handEffect;

    [SerializeField]
    [Header("じゃんけんの手")]
    RSPParam _rSP = RSPParam.Rock;

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
}
