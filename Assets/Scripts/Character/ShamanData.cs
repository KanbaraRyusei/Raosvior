using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// シャーマン
/// ダメージを受ける時
/// チョキを1枚捨てて
/// ダメージを1減らしてもよい。
/// </summary>
public class ShamanData : CharacterBase
{
    #region private menber

    /// <summary>
    /// 選んだチョキのカードを保存
    /// </summary>
    PlayerHand _scissorsHand;

    #endregion

    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        //チョキのカードを絞り込む
        PlayerHand[] scissorsHands =
            Players[MyselfIndex]
                .PlayerHands
                .Where(x => x.Hand == RSPParameter.Scissors).ToArray();
        //チョキのカードがなかったら
        if (scissorsHands[ConstParameter.ZERO] == null)
        {
            return;
        }
        else//チョキのカードがあったら
        {
            ChangeIntervetion(true);//介入処理ありに変更

            //チョキのカードを選択
            _scissorsHand = scissorsHands[default];
            //チョキを捨てる
            Players[MyselfIndex]
            .OnReserveHand(_scissorsHand);
            //1回復(ダメージを1減らしてもよいの代わりに)
            Players[MyselfIndex]
            .HealLife(ConstParameter.ONE);
        }  
    }

    #endregion
}
