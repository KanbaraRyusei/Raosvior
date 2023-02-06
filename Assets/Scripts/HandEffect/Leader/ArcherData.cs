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

    public override void CardEffect()
    {
        var playerRSP = Player.PlayerParameter.PlayerSetHand.Hand;
        var enemyRSP = Enemy.PlayerParameter.PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
            
        //引き分けならエネミーに1ダメージ与える
        if (value == RSPManager.DRAW) Enemy.LifeChange.ReceiveDamage();
        //負けならプレイヤーがもう1ダメージを受ける
        else if(value == RSPManager.LOSE) Player.LifeChange.ReceiveDamage();
    }

    #endregion
}
