using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイギスの裁き・F(パー)
/// 勝利した時、自分のシールドトークンを1つに付きさらに1ダメージを与えてもよいそうした場合、自分のシールドトークンを破壊する。
/// ※自分のシールドを減らす枚数を決める処理が必要
/// </summary>
public class FPaperCardJudgmentOfAigis : HandEffect
{
    public int AddDamege => _addDamege;

    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;
    int _addDamege = 0;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        //自分のシールドを破壊する枚数を決めれるようにする


        //自分のシールドを減らす
        _playerBase[_player].GetShield(_addDamege);

        //通常ダメージ+追加ダメージ(シールドを減らした枚数分)を与える
        _playerBase[_enemy].ReceiveDamage(ONE + _addDamege);

        _playerBase[_player].DeleteHand(_playerHand);//このカードを捨てる
    }

    /// <summary>シールドを減らす量を設定する</summary>
    public int SelectBreakShields(int num) => _addDamege -= num;
}
