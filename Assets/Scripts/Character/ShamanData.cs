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
        var enemyLeader = _players[EnemyIndex].LeaderHand.Leader;
        var archer = LeaderParameter.Archer;
        var playerRSP = _players[PlayerIndex].PlayerSetHand.Hand;
        var enemyRSP = _players[EnemyIndex].PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        //相手がアーチャーの引き分けor負けだったら
        var draw = value == RSPManager.DRAW && enemyLeader == archer;
        if (draw || value == RSPManager.LOSE)
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
