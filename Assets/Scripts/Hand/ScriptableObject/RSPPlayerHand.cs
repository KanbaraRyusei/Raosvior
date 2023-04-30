using System;
using UnityEngine;

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

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("じゃんけんの手")]
    private RSPParameter _rSP = RSPParameter.Rock;

    #endregion
}
