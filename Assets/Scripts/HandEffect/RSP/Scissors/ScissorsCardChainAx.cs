using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// チェーンアックス(チョキ)
/// 勝利した時このカードを手札に戻せる。
/// ※カードを手札に戻すかどうかの選択が必要
/// </summary>
public class ScissorsCardChainAx : RSPHandEffect
{
    #region Public Property

    public bool IsDecide { get; private set; }

    #endregion

    #region Public Methods

    public override void Effect()
    {
        ChangePlayersIndex(Player);
        //このカードを手札に戻すのでカードを捨てるか選択できるようにする
        PhaseManager.OnNextPhase(this);
    }

    public void CardBack()
    {
        Players[PlayerIndex].HandCollection.CardBack();
        IsDecide = true;

        Invoke("Init", 1f);
    }

    public void NotCardBack()
    {
        IsDecide = true;

        Invoke("Init", 1f);
    }

    async public void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(20000, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhaseProperty;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) NotCardBack();
    }

    #endregion

    #region Private Method

    private void Init()
    {
        IsDecide = false;
    }

    #endregion
}
