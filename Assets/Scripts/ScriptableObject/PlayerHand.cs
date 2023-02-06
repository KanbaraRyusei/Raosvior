using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// カードの情報を持つクラス
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/CreatePlayerHandAsset")]
public class PlayerHand : ScriptableObject
{
    #region Public property

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
    public RSPHandEffect HandEffect => _handEffect;

    public Image CardImage => _cardImage;

    /// <summary>
    /// シールドトークンになった時の番号
    /// </summary>
    public int ShildNumber { get; private set; }

    #endregion

    #region Inspector Member

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
    private RSPHandEffect _handEffect;

    [SerializeField]
    [Header("カードの画像")]
    private Image _cardImage;
    #endregion

    #region Public Method

    public void SetShildNumber(int number)
    {
        ShildNumber = number;
    }

    #endregion
}
