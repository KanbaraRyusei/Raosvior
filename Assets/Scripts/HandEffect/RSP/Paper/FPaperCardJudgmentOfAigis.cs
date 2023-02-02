using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// アイギスの裁き・F(パー)
/// 勝利した時、自分のシールドトークンを1つに付きさらに1ダメージを与えてもよいそうした場合、自分のシールドトークンを破壊する。
/// ※自分のシールドを減らす枚数を決める処理が必要
/// </summary>
public class FPaperCardJudgmentOfAigis : RSPHandEffect
{
    public int BreakCount { get; private set; }
    public bool IsDecide { get; private set; }

    public override void Effect()
    {
        ChangePlayersIndex(Player);

        //自分のシールドを破壊する枚数を決めれるようにする

        PhaseManager.OnNextPhase(this);
    }

    /// <summary>
    /// 選択するシールド破壊数を1増やす関数
    /// </summary>
    public void AddBreakCount()
    {
        var shildCount = Players[PlayerIndex].PlayerParameter.Shield;
        if (BreakCount >= shildCount) return;
        BreakCount++;
    }

    /// <summary>
    /// 選択するシールド破壊数を1減らす関数
    /// </summary>
    public void RemoveBreakCount()
    {
        if (BreakCount <= 0) return;
        BreakCount--;
    }

    /// <summary>
    /// シールド破壊数を決定してダメージを与える関数
    /// </summary>
    public void DecideBreakCount()
    {
        IsDecide = true;

        if (BreakCount > 0)
        {
            Players[EnemyIndex].LifeChange.GetShield(-BreakCount);
            Players[EnemyIndex].LifeChange.ReceiveDamage(BreakCount);
        }
            
        PhaseManager.OnNextPhase();

        Init();
    }

    async public void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(2000, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhaseProperty;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) DecideBreakCount();
    }

    private void Init()
    {
        BreakCount = 0;
        IsDecide = false;
    }
}
