using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// カードの情報を持つ基底クラス
/// </summary>
public abstract class PlayerHandBase : ScriptableObject
{
    #region Properties

    /// <summary>
    /// カードの名前の読み取り専用プロパティ
    /// </summary>
    public string CardName => _cardName;

    /// <summary>
    /// カードの効果の文の読み取り専用プロパティ
    /// </summary>
    public string CardEffect => _cardEffect;

    public Sprite CardSprite => _cardSprite;

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("カード名")]
    private string _cardName = "";

    [SerializeField]
    [Header("効果名")]
    [Multiline]
    private string _cardEffect = "";

    [SerializeField]
    [Header("カードの画像")]
    private Sprite _cardSprite = null;

    #endregion

    #region Public Methods

    public void SetCardImage(Sprite sprite) =>
        _cardSprite = sprite;

    #endregion
}
