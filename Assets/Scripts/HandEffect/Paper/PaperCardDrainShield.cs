using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ドレインシールド(パー)
/// 勝利した時、相手の手札を1枚選んで裏向きのまま自分のシールドトークンにする。(このシールドトークンは破壊されたとき持ち物のレシーブに置く)
/// ※相手のカードを選択する必要がある。そのカードが破壊されたときの処理も必要
/// </summary>
public class PaperCardDrainShield : HandEffect
{
    public int AddShild => _addShild;

    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    IReadOnlyList<PlayerData> _players = PlayerManager.Players;
    int _playerIndex = 0;
    int _enemyIndex = 1;
    int _addShild = 0;


    public override void Effect()
    {
        _players[_enemyIndex].ReceiveDamage(ConstParameter.ONE);//相手にダメージを与える

        //相手のカードを自分のシールドトークンにしたいので
        //ここで相手の手札を選ぶ

        PhaseManager.OnNextPhase(true);
    }

}
