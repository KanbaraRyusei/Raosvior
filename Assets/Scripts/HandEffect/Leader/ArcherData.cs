using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アーチャー
/// 引き分けた時1ダメージ与える。
/// 負けた時、1ダメージを受ける。
/// </summary>
public class ArcherData : LeaderHandEffect
{
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        var playerRSP = PlayerParameters[PlayerIndex].PlayerSetHand.Hand;
        var enemyRSP = PlayerParameters[EnemyIndex].PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        if (value == RSPManager.DRAW)//引き分けなら
        {
            //エネミーに1ダメージ与える。
            LifeChanges[EnemyIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
        else if(value == RSPManager.LOSE)//負けなら
        {
            //プレイヤーがもう1ダメージを受ける。
            LifeChanges[PlayerIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
    }

    #endregion
}
