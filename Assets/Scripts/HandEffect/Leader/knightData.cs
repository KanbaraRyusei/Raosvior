using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ナイト
/// パーで勝利した時
/// 相手のリザーブの数だけ
/// シールドトークンを獲得する。
/// </summary>
public class KnightData : LeaderHandEffect
{
    #region unity method

    private void Awake()
    {
        LeaderType = LeaderParameter.Knight;
    }

    #endregion

    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        var playerRSP = _players[PlayerIndex].PlayerSetHand.Hand;
        var enemyRSP = _players[EnemyIndex].PlayerSetHand.Hand;
        if (playerRSP == PAPER && enemyRSP == ROCK)//パーで勝利したら
        {
            // 相手のリザーブの数だけシールドトークンを獲得
            var count = _players[EnemyIndex].PlayerReserve.Count;
            _players[PlayerIndex].GetShield(count);
        }
    }

    #endregion
}