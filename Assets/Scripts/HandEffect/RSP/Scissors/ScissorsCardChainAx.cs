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
    #region Properties

    public bool IsDecide { get; private set; }

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("選択時間")]
    private int _selectTime = 20000;

    #endregion

    #region Member Variables

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

        Invoke(nameof(Init), _initTime);
    }

    public void NotCardBack()
    {
        IsDecide = true;

        Invoke(nameof(Init), _initTime);
    }

    public async void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(_selectTime, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhaseProperty;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) NotCardBack();
    }

    #endregion

    #region Private Methods

    private void Init()
    {
        IsDecide = false;
    }

    #endregion
}
