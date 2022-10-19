using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アーチャー
/// 引き分けた時1ダメージ与える。
/// 負けた時、1ダメージを受ける。
/// </summary>
public class ArcherData : CharacterBase
{
    bool test;

    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        bool draw =
            Players[MyselfIndex].PlayerSetHand.Hand == 
            Players[EnemyIndex].PlayerSetHand.Hand;
        bool[] losePattern =
        {
            //グー<パー
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper,
            //チョキ<グー
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Scissors &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock,
            //パー<チョキ
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Scissors
        };
        bool lose =
            losePattern[ConstParameter.ZERO] ||
            losePattern[ConstParameter.ONE] ||
            losePattern[ConstParameter.TWO];
        if (draw)//引き分けなら
        {
            //1ダメージ与える。
            Players[EnemyIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
        else if(lose)//負けなら
        {
            //1ダメージを受ける。
            Players[MyselfIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
    }

    #endregion
}
