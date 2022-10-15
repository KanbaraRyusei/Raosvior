using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アーチャー
/// 引き分けた時1ダメージ与える。
/// 負けた時、1ダメージを受ける。
/// </summary>
public class ArcherData : CharacterBase
{
    #region public method

    /// <summary>
    /// 勝ち以外の時に呼び出す
    /// </summary>
    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);

        if(true)//負けなら
        {
            //1ダメージを受ける。
            Players[MyselfPlayerIndex].ReceiveDamage(ConstParameter.ONE);
        }
        else//引き分けなら
        {
            //1ダメージ与える。
            Players[EnemyPlayerIndex].ReceiveDamage(ConstParameter.ONE);
        }
    }

    #endregion
}
