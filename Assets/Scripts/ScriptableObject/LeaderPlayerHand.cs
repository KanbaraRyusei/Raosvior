using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リーダーカードの情報を持つクラス
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/CreateLeaderHandAsset")]
public class LeaderPlayerHand : ScriptableObject
{
    #region public property

    /// <summary>
    /// カードの手の読み取り専用プロパティ
    /// </summary>
    public LeaderParameter Leader => _leader;

    /// <summary>
    /// リーダーカードの名前の読み取り専用プロパティ
    /// </summary>
    public string CardName => _cardName;

    /// <summary>
    /// リーダーカードの効果の文の読み取り専用プロパティ
    /// </summary>
    public string CardEffect => _cardEffect;

    /// <summary>
    /// リーダーカードの効果の読み取り専用プロパティ
    /// </summary>
    public LeaderHandEffect HandEffect => _handEffect;

    public Image CardImage => _cardImage;

    #endregion

    #region private property

    [SerializeField]
    [Header("リーダーのジョブ")]
    private LeaderParameter _leader = LeaderParameter.Shaman;

    [SerializeField]
    [Header("カード名")]
    private string _cardName = "None";

    [SerializeField]
    [Header("効果名")]
    [Multiline]
    private string _cardEffect = "None";

    [SerializeField]
    [Header("効果")]
    private LeaderHandEffect _handEffect;

    [SerializeField]
    [Header("カードの画像")]
    private Image _cardImage;

    #endregion
}
