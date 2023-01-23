using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ハンドの基底クラス
/// </summary>
public abstract class HandEffect : MonoBehaviour
{
    #region protected property

    // プレイヤーを参照するList用のインデックス
    protected int PlayerIndex { get; private set; }
    protected int EnemyIndex { get; private set; }

    protected IHandCollection HandCollection { get; private set; }
    protected ILifeChange LifeChange { get; private set; }

    #endregion

    #region protected member

    /// <summary>
    /// プレイヤーを参照するList
    /// </summary>
    protected IReadOnlyList<PlayerData> _players = PlayerManager.Players;
    protected IReadOnlyList<IHandCollection> _handCollections = PlayerManager.HandCollections;
    protected IReadOnlyList<ILifeChange> _lifeChanges = PlayerManager.LifeChanges;

    #endregion

    #region

    public void SetPlayerData(PlayerData playerData)
    {
        LifeChange = playerData;
    }

    #endregion

    #region protected method

    /// <summary>
    /// プレイヤーを切り替えるメソッド
    /// </summary>
    protected void ChangePlayersIndex(IHandCollection player)
    {
        if (_handCollections[ConstParameter.ZERO] == player)
        {
            PlayerIndex = ConstParameter.ZERO;
            EnemyIndex = ConstParameter.ONE;
        }
        else
        {
            PlayerIndex = ConstParameter.ONE;
            EnemyIndex = ConstParameter.ZERO;
        }
    }

    #endregion
}
