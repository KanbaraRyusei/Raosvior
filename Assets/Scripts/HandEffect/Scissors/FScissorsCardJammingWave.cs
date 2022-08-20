using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ジャミングウェーブ・F(チョキ)
/// 勝利した時ダメージを与える前に相手のシールドトークンを全て破壊する
/// </summary>
public class FScissorsCardJammingWave : HandEffect
{
    PlayerBase[] _playerBase;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[1].GetShield(-_playerBase[1].Shild);
        _playerBase[1].ReceiveDamage(ONE);
    }
}
