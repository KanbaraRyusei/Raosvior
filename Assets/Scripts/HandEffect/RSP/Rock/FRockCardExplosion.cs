using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エクスプロージョン・F(グー)
/// 勝利した時自分のリザーフにカードが3枚以上あればさらに2ダメージを与える。
/// </summary>
public class FRockCardExplosion : RSPHandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    public override void Effect()
    {
        ChangePlayersIndex(Player);

        LifeChanges[EnemyIndex].ReceiveDamage(ConstParameter.ONE);//相手にダメージを与える
        //自分のリザーフにカードが3枚以上あれば
        if (PlayerParameters[PlayerIndex].PlayerReserve.Count >= ConstParameter.THREE)
        {
            //さらに2ダメージを与える。
            LifeChanges[EnemyIndex].ReceiveDamage(ConstParameter.TWO);
        }
        HandCollections[PlayerIndex].OnReserveHand(_playerHand);//このカードを捨てる  
    }
}
