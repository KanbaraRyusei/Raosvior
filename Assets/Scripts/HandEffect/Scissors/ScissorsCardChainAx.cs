using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チェーンアックス(チョキ)
/// 勝利した時このカードを手札に戻せる。
/// ※カードを手札に戻すかどうかの選択が必要
/// </summary>
public class ScissorsCardChainAx : HandEffect
{
    public bool IsDelete => _isDelete;

    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;
    bool _isDelete = false;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[_enemy].ReceiveDamage(ONE);//相手にダメージを与える

        //このカードを手札に戻すのでカードを捨てるか選択できるようにする


        //捨てるなら
        if(_isDelete == true)
        {
            _playerBase[_player].DeleteHand(_playerHand);//このカードを捨てる  
        }
    }

    /// <summary>カードを捨てるかどうか</summary>
    public bool SelectIsDelete(bool delete) => _isDelete = delete;
}
