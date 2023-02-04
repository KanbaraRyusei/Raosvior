using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ジャミングウェーブ・F(チョキ)
/// 勝利した時ダメージを与える前に相手のシールドトークンを全て破壊する
/// </summary>
public class FScissorsCardJammingWave : RSPHandEffect
{
    #region Public Method

    public override void Effect()
    {
        ChangePlayersIndex(Player);
        var allShield = Players[EnemyIndex].PlayerParameter.Shield;
        Players[EnemyIndex].LifeChange.GetShield(-allShield);//相手のシールドを全て破壊
        Players[EnemyIndex].LifeChange.ReceiveDamage();
    }

    #endregion
}
