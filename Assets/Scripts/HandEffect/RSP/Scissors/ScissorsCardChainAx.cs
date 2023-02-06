using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// チェーンアックス(チョキ)
/// 勝利した時このカードを手札に戻せる。
/// ※介入処理
/// </summary>
public class ScissorsCardChainAx : RSPHandEffect
{
    #region Public Property

    public bool IsDecide { get; private set; }

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("選択時間")]
    private int _selectTime = 20000;

    #endregion

    #region Private Member

    private float _initTime = 1f;

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
        Player.HandCollection.CardBack();
        IsDecide = true;

        Invoke("Init", _initTime);
    }

    public void NotCardBack()
    {
        IsDecide = true;

        Invoke("Init", _initTime);
    }

    async public void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(_selectTime, cancellationToken: token);

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
