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
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        var enemyLeader = PlayerParameters[EnemyIndex].LeaderHand.HandEffect.GetType();
        var playerRSP = PlayerParameters[PlayerIndex].PlayerSetHand.Hand;
        var enemyRSP = PlayerParameters[EnemyIndex].PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        //相手がアーチャーの引き分けor負けだったら
        var draw = value == RSPManager.DRAW && enemyLeader == typeof(ArcherData);
        if (draw || value == RSPManager.LOSE)
        {
            //チョキのカードを絞り込む
            foreach (var RSP in PlayerParameters[PlayerIndex].PlayerHands)
            {
                //チョキのカードがあったら
                if (RSP.Hand == RSPParameter.Scissors)
                {
                    PhaseManager.OnNextPhase(true);
                    return;
                }
            }
        }
    }

    #endregion
}
