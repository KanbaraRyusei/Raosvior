using UnityEngine;
using System;

/// <summary>
/// カードの情報を持つクラス
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/CreatePlayerHandAsset")]
public class PlayerHand : ScriptableObject
{
    /// <summary>
    /// カードの手の読み取り専用プロパティ
    /// </summary>
    public RSPParameter Hand => _rSP;
    /// <summary>
    /// カードの名前の読み取り専用プロパティ
    /// </summary>
    public string CardName => _cardName;
    /// <summary>
    /// カードの効果の文の読み取り専用プロパティ
    /// </summary>
    public string CardEffect => _cardEffect;
    /// <summary>
    /// カードの効果の読み取り専用プロパティ
    /// </summary>
    public HandEffect HandEffect => _handEffect;

    [SerializeField]
    [Header("じゃんけんの手")]
    private RSPParameter _rSP = RSPParameter.Rock;

    [SerializeField]
    [Header("カード名")]
    private string _cardName = "None";

    [SerializeField]
    [Header("効果名")]
    [Multiline]
    private string _cardEffect = "None";

    [SerializeField]
    [Header("効果")]
    private HandEffect _handEffect;
}
