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

    public override bool CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
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
        return false;
    }

    #endregion
}
