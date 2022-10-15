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

    /// <summary>
    /// パーで勝利したときに呼び出す
    /// </summary>
    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);

        if(true)//パーで勝利したら
        {
            // シールドトークンを獲得する。
            Players[MyselfPlayerIndex]
                .GetShield(Players[EnemyPlayerIndex]
                            .PlayerReserve
                            .Count);
        }
    }

    #endregion
}
