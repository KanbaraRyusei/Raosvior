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

    public override void CardEffect()
    {
        ChangePlayersIndex(Player);
        var playerRSP = Players[PlayerIndex].PlayerParameter.PlayerSetHand.Hand;
        var enemyRSP = Players[EnemyIndex].PlayerParameter.PlayerSetHand.Hand;
        if (playerRSP == PAPER && enemyRSP == ROCK)//パーで勝利したら
        {
            // 相手のリザーブの数だけシールドトークンを獲得
            var count = Players[EnemyIndex].PlayerParameter.PlayerReserve.Count;
            Players[PlayerIndex].LifeChange.GetShield(count);
        }
    }

    #endregion
}
