using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/CreatePlayerHandAsset")]
public class PlayerHand : ScriptableObject
{
    public RSPParameter Hand => _rSP;
    public string CardName => _cardName;
    public string CardEffect => _cardEffect;
    public HandEffect HandEffect => _handEffect;

    [SerializeField]
    [Header("‚¶‚á‚ñ‚¯‚ñ‚Ìè")]
    RSPParameter _rSP = RSPParameter.Rock;

    [SerializeField]
    [Header("ƒJ[ƒh–¼")]
    string _cardName;

    [SerializeField]
    [Header("Œø‰Ê–¼")]
    [Multiline]
    string _cardEffect;

    [SerializeField]
    [Header("Œø‰Ê")]
    HandEffect _handEffect;
}
