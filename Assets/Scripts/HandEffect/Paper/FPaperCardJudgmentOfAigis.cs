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

    IReadOnlyList<PlayerData> _players = PlayerManager.Players;
    int _player = 0;
    int _enemy = 1;
    int _addDamege = 0;

    public override void Effect()
    {
        //自分のシールドを破壊する枚数を決めれるようにする

        PhaseManager.OnNextPhase(true);
    }
}
