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
    #region unity method

    private void Awake()
    {
        LeaderType = LeaderParameter.Grappler;
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
        if (playerRSP == r && enemyRSP == s)//グーで勝利したら
        {
            //効果をもう一度発動
            _players[PlayerIndex].PlayerSetHand.HandEffect.Effect();
        }
        return false;
    }

    #endregion
}
