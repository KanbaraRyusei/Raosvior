using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ドレインシールド(パー)
/// 勝利した時、相手の手札を1枚選んで裏向きのまま自分のシールドトークンにする。
/// (このシールドトークンは破壊されたとき持ち物のレシーブに置く)
/// ※介入処理
/// </summary>
public class PaperCardDrainShield : RSPHandEffect
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

    private RSPHandData _enemyHand;
    private float _initTime = 1f;

    #endregion

    #region Constants

    private const int DEFAULT_DAMEGE = 1;

    #endregion

    #region Public Methods

    public override void Effect()
    {
        //相手のカードを自分のシールドトークンにしたいので
        //ここで相手の手札を選ぶ
        PhaseManager.OnNextPhase(this);
    }

    public void SelectEnemyHand(RSPHandData enemyHand)
    {
        _enemyHand = enemyHand;
    }

    public void DecideEnemyHand()
    {
        if(_enemyHand == null) _enemyHand = Enemy.PlayerParameter.RSPHands[0];

        var index = PlayerManager.Instance.Players[0] == Player ? 0 : 1;
        var name = _enemyHand.Hand.CardName;
        RPCManager.Instance.SendPaperCardDrainShield(index, name);

        Invoke(nameof(Init), _initTime);
    }

    public async void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(_selectTime, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhase;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) DecideEnemyHand();
    }

    #endregion

    #region Private Methods

    private void Init()
    {
        IsDecide = false;
        _enemyHand = null;
    }

    #endregion
}