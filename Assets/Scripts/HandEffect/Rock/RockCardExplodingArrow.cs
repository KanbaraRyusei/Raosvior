using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炸裂する矢(グー)
/// 勝利した時相手がシールドトークンを所持しているならさらに1ダメージを与える。
/// </summary>
public class RockCardExplodingArrow : HandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player;
    int _enemy;

    const int ONE = 1;
    const int TWO = 2;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();   
    }

    public override  void Effect()
    { 
        //相手がシールドトークンを所持しているなら
        if (_playerBase[_enemy].Shild > 0)
        {
            _playerBase[_enemy].ReceiveDamage(TWO);//2ダメージを与える
        }
        else//所持していなかったら
        {
            _playerBase[_enemy].ReceiveDamage(ONE);//相手にダメージを与える
        }

        _playerBase[_player].DeleteHand(_playerHand);//このカードを捨てる  
    }
}
