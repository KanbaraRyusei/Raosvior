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
    #region unity method

    private void Awake()
    {
        LeaderType = LeaderParameter.Archer;
    }

    #endregion

    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        var playerRSP = _players[PlayerIndex].PlayerSetHand.Hand;
        var enemyRSP = _players[EnemyIndex].PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        if (value == RSPManager.DRAW)//引き分けなら
        {
            //エネミーに1ダメージ与える。
            _players[EnemyIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
        else if(value == RSPManager.LOSE)//負けなら
        {
            //プレイヤーがもう1ダメージを受ける。
            _players[PlayerIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
    }

    #endregion
}
