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
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        var playerRSP = PlayerParameters[PlayerIndex].PlayerSetHand.Hand;
        var enemyRSP = PlayerParameters[EnemyIndex].PlayerSetHand.Hand;
        if (playerRSP == PAPER && enemyRSP == ROCK)//パーで勝利したら
        {
            // 相手のリザーブの数だけシールドトークンを獲得
            var count = PlayerParameters[EnemyIndex].PlayerReserve.Count;
            LifeChanges[PlayerIndex].GetShield(count);
        }
    }

    #endregion
}
