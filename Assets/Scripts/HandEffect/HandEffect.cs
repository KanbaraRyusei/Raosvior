using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全てのハンドの効果の基底クラス
/// </summary>
public abstract class HandEffect : MonoBehaviour
{
    #region Public Property

    public PlayerInterface Player { get; private set; }

    #endregion

    #region protected property

    // プレイヤーを参照するList用のインデックス
    protected int PlayerIndex { get; private set; }
    protected int EnemyIndex { get; private set; }

    #endregion

    #region protected member

    // プレイヤーのインターフェースをを参照するList
    protected IReadOnlyList<PlayerInterface> Players => PlayerManager.Players;

    #endregion

    #region

    public void SetPlayerData(PlayerInterface player)
    {
        Player = player;
    }

    #endregion

    #region protected method

    /// <summary>
    /// プレイヤーを切り替えるメソッド
    /// </summary>
    protected void ChangePlayersIndex(PlayerInterface player)
    {
        if (Players[0].HandCollection == player)
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
