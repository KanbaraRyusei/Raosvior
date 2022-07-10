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
    [Header("����񂯂�̎�")]
    RSP _rSP = RSP.Rock;

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

    public enum RSP
    {
        [Tooltip("�O�[")]
        Rock,
        [Tooltip("�`���L")]
        Scissors,
        [Tooltip("�p�[")]
        Paper
    }
}
