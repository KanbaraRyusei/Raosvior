using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シャーマン
/// ダメージを受ける時
/// チョキを1枚捨てて
/// ダメージを1減らしてもよい。
/// </summary>
public class ShamanData : LeaderHandEffect
{
    #region unity method

    private void Awake()
    {
        LeaderType = LeaderParameter.Shaman;
    }

    #endregion

    #region public method

    public override bool CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        var enemyLeader = _players[EnemyIndex].LeaderHand.LeaderType;
        var archer = LeaderParameter.Archer;
        var playerRSP = _players[PlayerIndex].PlayerSetHand.Hand;
        var enemyRSP = _players[EnemyIndex].PlayerSetHand.Hand;
        var r = RSPParameter.Rock;
        var s = RSPParameter.Scissors;
        var p = RSPParameter.Paper;
        var draw = playerRSP == enemyRSP;
        var lose =
            //グー<パー
            playerRSP == r && enemyRSP == p ||
            //チョキ<グー
            playerRSP == s && enemyRSP == r ||
            //パー<チョキ
            playerRSP == p && enemyRSP == s;
        //相手がアーチャーの引き分けor負けだったら
        if (draw && enemyLeader == archer || lose)
        {
            //チョキのカードを絞り込む
            foreach (var RSP in _players[PlayerIndex].PlayerHands)
            {
                //チョキのカードがあったら
                if (RSP.Hand == RSPParameter.Scissors)
                {
                    return true;
                }
            }
        }
        return false;
    }

    #endregion
}
