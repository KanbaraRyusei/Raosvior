using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ナイト
/// パーで勝利した時
/// 相手のリザーブの数だけ
/// シールドトークンを獲得する。
/// </summary>
public class knightData : CharacterBase
{
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        // パー>グー
        bool win =
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock;
        if (win)//パーで勝利したら
        {
            // 相手のリザーブの数だけシールドトークンを獲得
            Players[MyselfIndex]
                .GetShield(Players[EnemyIndex]
                            .PlayerReserve
                            .Count);
        }
    }

    #endregion
}
