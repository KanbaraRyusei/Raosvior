using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// アイギスの裁き・F(パー)
/// 勝利した時、自分のシールドトークンを1つに付きさらに1ダメージを与えてもよいそうした場合、自分のシールドトークンを破壊する。
/// </summary>
public class FPaperCardJudgmentOfAigis : HandEffect
{
    PlayerBase[] _playerBase;

    int _addDamege = 0;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[1].ReceiveDamage(ONE);//相手にダメージを与える

        //自分のシールドを破壊する枚数を決める

        _playerBase[0].GetShield(_addDamege);//自分のシールドを減らす
        _playerBase[1].ReceiveDamage(_addDamege);//相手にのダメージ(自分のシールドを減らした枚数分)を与える
    }

    /// <summary>シールドを減らす量を設定する</summary>
    public int BreakMyShield(int num) => _addDamege -= num;
}
