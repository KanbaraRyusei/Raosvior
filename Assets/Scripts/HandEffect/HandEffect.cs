using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全てのハンドの効果の基底クラス
/// </summary>
public abstract class HandEffect : MonoBehaviour
{
    #region protected property

    // プレイヤーを参照するList用のインデックス
    protected int PlayerIndex { get; private set; }
    protected int EnemyIndex { get; private set; }

    protected IPlayerParameter Player { get; private set; }

    #endregion

    #region protected member

    // プレイヤーのインターフェースをを参照するList
    protected IReadOnlyList<IPlayerParameter> PlayerParameters => PlayerManager.PlayerParameters;
    protected IReadOnlyList<IUseHand> UseHands => PlayerManager.UseHands;
    protected IReadOnlyList<IHandCollection> HandCollections => PlayerManager.HandCollections;
    protected IReadOnlyList<ILifeChange> LifeChanges => PlayerManager.LifeChanges;

    #endregion

    #region

    public void SetPlayerData(PlayerData playerData)
    {
        Player = playerData;
    }

    #endregion

    #region protected method

    /// <summary>
    /// プレイヤーを切り替えるメソッド
    /// </summary>
    protected void ChangePlayersIndex(IPlayerParameter player)
    {
        if (PlayerParameters[0] == player)
        {
            PlayerIndex = 0;
            EnemyIndex = 1;
        }
        else
        {
            PlayerIndex = 1;
            EnemyIndex = 0;
        }
    }

    #endregion
}
