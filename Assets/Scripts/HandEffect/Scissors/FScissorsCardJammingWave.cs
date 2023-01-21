using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャミングウェーブ・F(チョキ)
/// 勝利した時ダメージを与える前に相手のシールドトークンを全て破壊する
/// </summary>
public class FScissorsCardJammingWave : HandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    IReadOnlyList<PlayerData> _players = PlayerManager.Players;
    int _playerIndex = 0;
    int _enemyIndex = 1;

    public override void Effect()
    {
        var allShield = _players[_enemyIndex].Shield;
        _players[_enemyIndex].GetShield(-allShield);//相手のシールドを全て破壊
        _players[_enemyIndex].ReceiveDamage(ConstParameter.ONE);//相手にダメージを与える
        _players[_playerIndex].OnReserveHand(_playerHand);//このカードを捨てる  
        PhaseManager.OnNextPhase();
    }
}
