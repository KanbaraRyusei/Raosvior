using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// エクスプロージョン・F(グー)
/// 勝利した時自分のリザーフにカードが3枚以上あればさらに2ダメージを与える。
/// </summary>
public class FRockCardExplosion : RSPHandEffect
{
    #region Constants

    const int RESERVE_COUNT = 3;
    const int ADD_DAMEGE = 2;

    #endregion

    #region Public Method

    public override void Effect()
    {
        ChangePlayersIndex(Player);

        //自分のリザーフにカードが3枚以上あれば
        if (Player.PlayerParameter.PlayerReserve.Count >= RESERVE_COUNT)
        {
            //さらに2ダメージを与える。
            Enemy.LifeChange.ReceiveDamage(ADD_DAMEGE);
        }
    }

    #endregion
}
