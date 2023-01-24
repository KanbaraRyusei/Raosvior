using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// グラップラー
/// グーで勝利したとき
/// カード効果をもう一度発動する。
/// </summary>
public class GrapplerData : LeaderHandEffect
{
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        var playerRSP = PlayerParameters[PlayerIndex].PlayerSetHand.Hand;
        var enemyRSP = PlayerParameters[EnemyIndex].PlayerSetHand.Hand;
        if (playerRSP == ROCK && enemyRSP == SCISSORS)//グーで勝利したら
        {
            //効果をもう一度発動
            PlayerParameters[PlayerIndex].PlayerSetHand.HandEffect.Effect();
        }
    }

    #endregion
}
