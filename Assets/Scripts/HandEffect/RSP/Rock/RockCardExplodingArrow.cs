using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炸裂する矢(グー)
/// 勝利した時相手がシールドトークンを所持しているならさらに1ダメージを与える。
/// </summary>
public class RockCardExplodingArrow : RSPHandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    int _player = 0;
    int _enemy = 1;

    public override  void Effect()
    {
        ChangePlayersIndex(HandCollection);


        _players[_enemy].ReceiveDamage(ConstParameter.ONE);//相手にダメージを与える

        //相手がシールドトークンを所持しているなら
        if (_players[_enemy].Shield > ConstParameter.ZERO)
        {
            //さらに1ダメージを与える
            _players[_enemy].ReceiveDamage(ConstParameter.ONE);
        }

        _players[_player].OnReserveHand(_playerHand);//このカードを捨てる
        PhaseManager.OnNextPhase();
    }
}
