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

    public override void CardEffect()
    {
        ChangePlayersIndex(Player);
        var enemyLeader = Players[EnemyIndex].PlayerParameter.LeaderHand.HandEffect.GetType();
        var playerRSP = Players[PlayerIndex].PlayerParameter .PlayerSetHand.Hand;
        var enemyRSP = Players[EnemyIndex].PlayerParameter.PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        //相手がアーチャーの引き分けor負けだったら
        var draw = value == RSPManager.DRAW && enemyLeader == typeof(ArcherData);
        if (draw || value == RSPManager.LOSE)
        {
            //チョキのカードを絞り込む
            foreach (var RSP in Players[PlayerIndex].PlayerParameter.PlayerHands)
            {
                //チョキのカードがあったら
                if (RSP.Hand == RSPParameter.Scissors)
                {
                    PhaseManager.OnNextPhase(this);
                    return;
                }
            }
        }
    }

    public void Intervention()
    {
                
    }
    #endregion
}
