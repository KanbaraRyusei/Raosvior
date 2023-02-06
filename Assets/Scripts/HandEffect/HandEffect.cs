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

    public PlayerInterface Enemy { get; private set; }

    #endregion 

    #region Public Method

    /// <summary>
    /// プレイヤーを切り替えるメソッド
    /// </summary>
    public void ChangePlayersIndex(PlayerInterface player)
    {
        int playerIndex;
        int enemyIndex;
        if (PlayerManager.Players[0].HandCollection == player)
        {
            playerIndex = 0;
            enemyIndex = 1;
        }
        else
        {
            playerIndex = 1;
            enemyIndex = 0;
        }
        Player = PlayerManager.Players[playerIndex];
        Enemy = PlayerManager.Players[enemyIndex];
    }

    #endregion
}
