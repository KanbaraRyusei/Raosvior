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

    /// <summary>
    /// グーで勝利したときに呼び出す
    /// </summary>
    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        if(true)//グーで勝利したら
        {
            Players[MyselfPlayerIndex].PlayerSetHand.HandEffect.Effect();
        }
    }

    #endregion
}
