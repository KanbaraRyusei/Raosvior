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

    public override void CardEffect()
    {
        ChangePlayersIndex(Player);
        var playerRSP = Players[PlayerIndex].PlayerParameter.PlayerSetHand.Hand;
        var enemyRSP = Players[EnemyIndex].PlayerParameter.PlayerSetHand.Hand;
        if (playerRSP == ROCK && enemyRSP == SCISSORS)//グーで勝利したら
        {
            //効果をもう一度発動
            Players[PlayerIndex].PlayerParameter.PlayerSetHand.HandEffect.Effect();
        }
    }

    #endregion
}
