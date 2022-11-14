using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// グラップラー
/// グーで勝利したとき
/// カード効果をもう一度発動する。
/// </summary>
public class GrapplerData : CharacterBase
{
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        // グー>チョキ
        bool win =
            _players[PlayerIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock &&
            _players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Scissors;
        if (win)//グーで勝利したら
        {
            //効果をもう一度発動
            _players[PlayerIndex].PlayerSetHand.HandEffect.Effect();
        }
        PhaseManager.OnNextPhase();
    }

    #endregion
}
