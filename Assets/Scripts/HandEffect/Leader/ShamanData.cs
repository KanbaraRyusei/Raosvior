using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// シャーマン
/// ダメージを受ける時
/// チョキを1枚捨てて
/// ダメージを1減らしてもよい。
/// ※介入処理
/// </summary>
public class ShamanData : LeaderHandEffect
{
    #region Properties

    public bool IsDecide { get; private set; }
    public bool IsReducing { get; private set; }

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("選択時間")]
    private int _selectTime = 20000;

    #endregion

    #region Member Variables

    private RSPPlayerHand _playerHand;
    private float _initTime = 1f;

    #endregion

    #region Public Methods

    public override void CardEffect()
    {
        var enemyLeader = Enemy.PlayerParameter.LeaderHand.HandEffect.GetType();
        var playerRSP = Player.PlayerParameter .PlayerSetHand.Hand;
        var enemyRSP = Enemy.PlayerParameter.PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        //相手がアーチャーの引き分けor負けだったら
        var draw = value == RSPManager.DRAW && enemyLeader == typeof(ArcherData);
        if (draw || value == RSPManager.LOSE)
        {
            var playerHands = Player.PlayerParameter.PlayerHands;
            var scissors = RSPParameter.Scissors;
            //チョキのカードを絞り込む
            if (playerHands.FirstOrDefault(x => x.Hand == scissors) != null)
                PhaseManager.OnNextPhase(this);
        }
    }

    public void SelectScissorsHand(RSPPlayerHand playerHand)
    {
        _playerHand  = playerHand;     
    }

    public void DontSelectScissorsHand()
    {
        IsDecide = true;
        IsReducing = false;

        Invoke(nameof(Init), _initTime);
    }

    public void DecideScissorsHand()
    {
        Player.HandCollection.OnReserveHand(_playerHand);

        IsDecide = true;
        IsReducing = true;

        Invoke(nameof(Init), _initTime);
    }

    public async void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(_selectTime, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhaseProperty;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) DontSelectScissorsHand();
    }

    #endregion

    #region Private Methods

    private void Init()
    {
        IsDecide = false;
        IsReducing = false;
    }

    #endregion
}
