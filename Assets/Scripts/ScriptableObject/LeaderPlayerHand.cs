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
public class LeaderPlayerHand : PlayerHandBase
{
    #region Properties

    /// <summary>
    /// リーダーカードの効果の読み取り専用プロパティ
    /// </summary>
    public LeaderHandEffect HandEffect => _handEffect;

    #endregion

    #region Private Variables

    [SerializeField]
    [Header("効果")]
    private LeaderHandEffect _handEffect = null;

    #endregion
}
