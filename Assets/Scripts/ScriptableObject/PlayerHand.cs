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
    [Header("����񂯂�̎�")]
    RSPParam _rSP = RSPParam.Rock;

    [SerializeField]
    [Header("�J�[�h��")]
    string _cardName;

    [SerializeField]
    [Header("���ʖ�")]
    [Multiline]
    string _cardEffect;

    [SerializeField]
    [Header("����")]
    HandEffect _handEffect;
}
