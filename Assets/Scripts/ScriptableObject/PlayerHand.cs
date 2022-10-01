using UnityEngine;
using System;

/// <summary>
/// �J�[�h�̏������N���X
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/CreatePlayerHandAsset")]
public class PlayerHand : ScriptableObject
{
    #region public property

    /// <summary>
    /// �J�[�h�̎�̓ǂݎ���p�v���p�e�B
    /// </summary>
    public RSPParameter Hand => _rSP;

    /// <summary>
    /// �J�[�h�̖��O�̓ǂݎ���p�v���p�e�B
    /// </summary>
    public string CardName => _cardName;

    /// <summary>
    /// �J�[�h�̌��ʂ̕��̓ǂݎ���p�v���p�e�B
    /// </summary>
    public string CardEffect => _cardEffect;

    /// <summary>
    /// �J�[�h�̌��ʂ̓ǂݎ���p�v���p�e�B
    /// </summary>
    public HandEffect HandEffect => _handEffect;

    #endregion

    #region private property

    [SerializeField]
    [Header("����񂯂�̎�")]
    private RSPParameter _rSP = RSPParameter.Rock;

    [SerializeField]
    [Header("�J�[�h��")]
    private string _cardName = "None";

    [SerializeField]
    [Header("���ʖ�")]
    [Multiline]
    private string _cardEffect = "None";

    [SerializeField]
    [Header("����")]
    private HandEffect _handEffect;

    #endregion
}
