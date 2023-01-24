using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ジャミングウェーブ・F(チョキ)
/// 勝利した時ダメージを与える前に相手のシールドトークンを全て破壊する
/// </summary>
public class FScissorsCardJammingWave : RSPHandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    public override void Effect()
    {
        ChangePlayersIndex(Player);

        var allShield = PlayerParameters[EnemyIndex].Shield;
        LifeChanges[EnemyIndex].GetShield(-allShield);//相手のシールドを全て破壊
        LifeChanges[EnemyIndex].ReceiveDamage(ConstParameter.ONE);//相手にダメージを与える
        HandCollections[PlayerIndex].OnReserveHand(_playerHand);//このカードを捨てる  
        PhaseManager.OnNextPhase();
    }
}
