using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// ダメージを受けるときに呼び出すメソッド
    /// </summary>
    /// <param name="player"></param>
    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);

        //チョキを捨てる
        Players[MyselfPlayerIndex]
            .OnReserveHand(Players[MyselfPlayerIndex]
                            .PlayerHands
                            .FirstOrDefault(x => x
                                .Hand == RSPParameter
                                    .Scissors));
    }

    #endregion
}
