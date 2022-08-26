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

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;
    int _addShild = 0;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[_enemy].ReceiveDamage(ONE);//相手にダメージを与える

        //相手のカードを自分のシールドトークンにしたいので
        //ここで相手の手札を選ぶ
        _playerBase[_enemy]
            .DeleteHand(_playerBase[_enemy]
            .PlayerHands//プレイヤーハンドのカードのindex(何番目か)で絞り込む
            .FirstOrDefault(x => x == _playerBase[_enemy].PlayerHands[_addShild]));

        _playerBase[_player].GetShield(ONE);//シールドを追加
        _playerBase[_player].DeleteHand(_playerHand);//このカードを捨てる  
    }

    /// <summary>選んだカードが何番目かを決める</summary>
    public int SelectCardNumber(int number) =>  _addShild = number;
}
