using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャミングウェーブ・F(チョキ)
/// 勝利した時ダメージを与える前に相手のシールドトークンを全て破壊する
/// </summary>
public class FScissorsCardJammingWave : HandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[_enemy].GetShield(-_playerBase[_enemy].Shild);//相手のシールドを全て破壊
        _playerBase[_enemy].ReceiveDamage(ONE);//相手にダメージを与える
        _playerBase[_player].DeleteHand(_playerHand);//このカードを捨てる  
    }
}
