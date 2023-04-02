using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// カードの情報を持つクラス
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/CreatePlayerHandAsset")]
public class RSPPlayerHand : PlayerHandBase
{
    #region Properties

    /// <summary>
    /// カードの手の読み取り専用プロパティ
    /// </summary>
    public RSPParameter Hand => _rSP;

    /// <summary>
    /// カードの効果の読み取り専用プロパティ
    /// </summary>
    public RSPHandEffect HandEffect => _handEffect;

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("じゃんけんの手")]
    private RSPParameter _rSP = RSPParameter.Rock;

    [SerializeField]
    [Header("効果")]
    private RSPHandEffect _handEffect = null;

    #endregion
}
