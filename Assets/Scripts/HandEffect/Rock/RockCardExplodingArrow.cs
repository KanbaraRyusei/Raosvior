using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 炸裂する矢(グー)
/// 勝利した時相手がシールドトークンを所持しているならさらに1ダメージを与える。
/// </summary>
public class RockCardExplodingArrow : HandEffect
{
    PlayerBase[] _playerBase;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();   
    }

    public override void Effect()
    {
        _playerBase[1].ReceiveDamage(ONE);

        if(_playerBase[0].Shild > 0)
        {
            _playerBase[1].ReceiveDamage(ONE);
        }
    }
}
