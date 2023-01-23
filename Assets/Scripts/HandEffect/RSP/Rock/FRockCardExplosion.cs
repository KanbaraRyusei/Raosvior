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

    int _playerIndex = 0;
    int _enemyIndex = 1;

    public override void Effect()
    {
        ChangePlayersIndex(HandCollection);


        _players[_enemyIndex].ReceiveDamage(ConstParameter.ONE);//相手にダメージを与える
        //自分のリザーフにカードが3枚以上あれば
        if (_players[_playerIndex].PlayerReserve.Count >= ConstParameter.THREE)
        {
            //さらに2ダメージを与える。
            _players[_enemyIndex].ReceiveDamage(ConstParameter.TWO);
        }
        _players[_playerIndex].OnReserveHand(_playerHand);//このカードを捨てる  
    }
}
