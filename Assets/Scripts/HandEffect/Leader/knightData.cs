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
        var playerRSP = Player.PlayerParameter.PlayerSetHand.Hand;
        var enemyRSP = Enemy.PlayerParameter.PlayerSetHand.Hand;
        if (playerRSP == PAPER && enemyRSP == ROCK)//パーで勝利したら
        {
            // 相手のリザーブの数だけシールドトークンを獲得
            var count = Enemy.PlayerParameter.PlayerReserve.Count;
            Player.LifeChange.GetShield(count);
        }
    }

    #endregion
}
