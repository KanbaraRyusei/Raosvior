using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シャーマン
/// ダメージを受ける時
/// チョキを1枚捨てて
/// ダメージを1減らしてもよい。
/// </summary>
public class ShamanData : CharacterBase
{
    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        //チョキのカードを絞り込む
        foreach (var RSP in _players[PlayerIndex].PlayerHands)
        {
            //チョキのカードがあったら
            if(RSP.Hand == RSPParameter.Scissors)
            {
                PhaseManager.OnNextPhase(true);
                return;
            }
        }
        //チョキのカードがなかったら
        PhaseManager.OnNextPhase();
    }

    #endregion
}
