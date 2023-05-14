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
        //このカードを手札に戻すのでカードを捨てるか選択できるようにする
        PhaseManager.OnNextPhase(this);
    }

    public void PutCardBack()
    {
        var index = PlayerManager.Instance.Players[0] == Player ? 0 : 1;
        RPCManager.Instance.SendScissorsCardChainAx(index);
        IsDecide = true;

        Invoke(nameof(Init), _initTime);
    }

    public void DontPutCardBack()
    {
        IsDecide = true;

        Invoke(nameof(Init), _initTime);
    }

    public async void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(_selectTime, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhase;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) DontPutCardBack();
    }

    #endregion

    #region Private Methods

    private void Init()
    {
        IsDecide = false;
    }

    #endregion
}
