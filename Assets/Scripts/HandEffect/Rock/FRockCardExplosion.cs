using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エクスプロージョン・F(グー)
/// 勝利した時自分のリザーフにカードが3枚以上あればさらに2ダメージを与える。
/// </summary>
public class FRockCardExplosion : HandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;

    const int ONE = 1;
    const int THREE = 3;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {

        //自分のリザーフにカードが3枚以上あれば
        if (_playerBase[_player].PlayerTrashs.Count >= THREE)
        {
            _playerBase[_enemy].ReceiveDamage(THREE);//3ダメージを与える
        }
        else
        {
            _playerBase[_enemy].ReceiveDamage(ONE);//相手にダメージを与える
        }

        _playerBase[_player].DeleteHand(_playerHand);//このカードを捨てる  
    }
}
