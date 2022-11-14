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
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        bool draw =
            _players[PlayerIndex].PlayerSetHand.Hand == 
            _players[EnemyIndex].PlayerSetHand.Hand;
        bool[] losePattern =
        {
            //グー<パー
            _players[PlayerIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock &&
            _players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper,
            //チョキ<グー
            _players[PlayerIndex].PlayerSetHand.Hand ==
            RSPParameter.Scissors &&
            _players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock,
            //パー<チョキ
            _players[PlayerIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper &&
            _players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Scissors
        };
        bool lose =
            losePattern[ConstParameter.ZERO] ||
            losePattern[ConstParameter.ONE] ||
            losePattern[ConstParameter.TWO];

        if (draw)//引き分けなら
        {
            //エネミーに1ダメージ与える。
            _players[EnemyIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
        else if(lose)//負けなら
        {
            //プレイヤーがもう1ダメージを受ける。
            _players[PlayerIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
        PhaseManager.OnNextPhase();
    }

    #endregion
}
