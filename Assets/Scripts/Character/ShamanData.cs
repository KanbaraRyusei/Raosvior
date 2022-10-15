using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シャーマン
/// ダメージを受ける時
/// チョキを1枚捨ててダメージを1減らしてもよい。
/// </summary>
public class ShamanData : CharacterBase
{
    #region Unity Methods

    private void Awake()
    {
        ChangeIntervetion(true);//介入処理がある
    }

    #endregion

    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);

        if(true)
        {
            //チョキを捨てる
            //Players[MyselfPlayerIndex].OnReserveHand();
        }
    }

    #endregion
}
