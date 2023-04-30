using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// アイギスの裁き・F(パー)
/// 勝利した時、自分のシールドトークンを1つに付きさらに1ダメージを与えてもよいそうした場合、
/// 自分のシールドトークンを破壊する
/// ※介入処理
/// </summary>
public class FPaperCardJudgmentOfAigis : RSPHandEffect
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

    private int _breakCount;
    private float _initTime = 1f;

    #endregion

    #region Public Methods

    public override void Effect()
    {
        //自分のシールドを破壊する枚数を決めれるようにする
        PhaseManager.Instance.OnNextPhase(this);
    }

    /// <summary>
    /// 選択するシールド破壊数を1増やす関数
    /// </summary>
    public void AddBreakCount()
    {
        var shildCount = Player.PlayerParameter.Shield;
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
            var index = PlayerManager.Instance.Players[0] == Player ? 0 : 1;
            RPCManager.Instance.SendFPaperCardJudgmentOfAigis(index, _breakCount);
        }
        
        Invoke(nameof(Init),_initTime);
    }

    public async void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(_selectTime, cancellationToken: token);

        var currentPhase = PhaseManager.Instance.CurrentPhaseProperty;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) DecideBreakCount();
    }

    #endregion

    #region Private Methods

    private void Init()
    {
        _breakCount = 0;
        IsDecide = false;
    }

    #endregion
}
