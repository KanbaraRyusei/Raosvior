using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ドレインシールド(パー)
/// 勝利した時、相手の手札を1枚選んで裏向きのまま自分のシールドトークンにする。(このシールドトークンは破壊されたとき持ち物のレシーブに置く)
/// </summary>
public class PaperCardDrainShield : HandEffect
{
    PlayerBase[] _playerBase;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[1].ReceiveDamage(ONE);//相手にダメージを与える

        //ここで相手の手札を選ぶ

        _playerBase[0].GetShield(ONE);//シールドを追加
    }
}
