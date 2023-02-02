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
    const int RESERVE_COUNT = 3;
    const int ADD_DAMEGE = 2;

    public override void Effect()
    {
        ChangePlayersIndex(Player);

        //自分のリザーフにカードが3枚以上あれば
        if (Players[PlayerIndex].PlayerParameter.PlayerReserve.Count >= RESERVE_COUNT)
        {
            //さらに2ダメージを与える。
            Players[EnemyIndex].LifeChange.ReceiveDamage(ADD_DAMEGE);
        }
    }
}
