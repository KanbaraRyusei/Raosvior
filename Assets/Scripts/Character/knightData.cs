using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ナイト
/// パーで勝利した時
/// 相手のリザーブの数だけ
/// シールドトークンを獲得する。
/// </summary>
public class KnightData : CharacterBase
{
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        // パー>グー
        bool win =
            _players[PlayerIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper &&
            _players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock;
        if (win)//パーで勝利したら
        {
            // 相手のリザーブの数だけシールドトークンを獲得
            var count = _players[EnemyIndex].PlayerReserve.Count;
            _players[PlayerIndex].GetShield(count);
        }
        PhaseManager.OnNextPhase();
    }

    #endregion
}
