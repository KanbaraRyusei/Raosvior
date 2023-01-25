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

    public override void CardEffect(PlayerInterface player)
    {
        ChangePlayersIndex(player as PlayerInterface);
        var playerRSP = Players[PlayerIndex].PlayerParameter.PlayerSetHand.Hand;
        var enemyRSP = Players[EnemyIndex].PlayerParameter.PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        if (value == RSPManager.DRAW)//引き分けなら
        {
            //エネミーに1ダメージ与える。
            Players[EnemyIndex]
                .LifeChange
                .ReceiveDamage();
        }
        else if(value == RSPManager.LOSE)//負けなら
        {
            //プレイヤーがもう1ダメージを受ける。
            Players[PlayerIndex]
                .LifeChange
                .ReceiveDamage();
        }
    }

    #endregion
}
