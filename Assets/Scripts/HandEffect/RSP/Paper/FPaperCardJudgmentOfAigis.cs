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
    #region Public Property

    public bool IsDecide { get; private set; }

    #endregion

    #region Private Member

    private int _breakCount;

    #endregion

    #region Public Methods

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
        if (_breakCount >= shildCount) return;
        _breakCount++;
    }

    /// <summary>
    /// 選択するシールド破壊数を1減らす関数
    /// </summary>
    public void RemoveBreakCount()
    {
        if (_breakCount <= 0) return;
        _breakCount--;
    }

    /// <summary>
    /// シールド破壊数を決定してダメージを与える関数
    /// </summary>
    public void DecideBreakCount()
    {
        IsDecide = true;

        if (_breakCount > 0)
        {
            Players[EnemyIndex].LifeChange.GetShield(-_breakCount);
            Players[EnemyIndex].LifeChange.ReceiveDamage(_breakCount);
        }

        Invoke("Init",1f);
    }

    async public void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(20000, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhaseProperty;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) DecideBreakCount();
    }

    #endregion

    #region Private Method

    private void Init()
    {
        _breakCount = 0;
        IsDecide = false;
    }

    #endregion
}
