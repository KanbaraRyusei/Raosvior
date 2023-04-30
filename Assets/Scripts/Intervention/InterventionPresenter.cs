using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;

public class InterventionPresenter : MonoBehaviour
{
    #region Properties

    private PhaseParameter CurrentPhase => PhaseParameter.Judgement;

    #endregion

    #region Inspector Variables

    [SerializeField]
    private BattleManager _battleManager = null;

    [SerializeField]
    [Header("ハンドのビュー")]
    private InterventionView _handView;

    [SerializeField]
    [Header("プレイヤーのプレゼンター")]
    private PlayerPresenter _playerPresenter;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        this
            .ObserveEveryValueChanged(x => x.CurrentPhase)
            .Subscribe(phase =>
            {
                DisplayViewByPhase(phase);
            })
            .AddTo(this);
    }

    #endregion

    #region Private Methods

    private void DisplayViewByPhase(PhaseParameter phase)
    {
        switch (phase)
        {
            case PhaseParameter.Intervention:

                var isClient = PhaseManager.Instance.IsFirstPlayer && _battleManager.PlayerIndex == 0;
                var isOther = !PhaseManager.Instance.IsFirstPlayer && _battleManager.PlayerIndex == 1;

                if (isClient) DisplayViewByIntervetion();     
                else if(isOther) DisplayViewByIntervetion();

                break;
        }
    }

    private async void DisplayViewByIntervetion()
    {
        switch (PhaseManager.Instance.IntervetionProperty)
        {
            case IntervetionParameter.LeaderCardShaman:

                await LeaderCardShaman();

                break;

            case IntervetionParameter.FPaperCardJudgmentOfAigis:

                await FPaperCardJudgmentOfAigis();

                break;

            case IntervetionParameter.PaperCardDrainShield:

                await PaperCardDrainShield();

                break;

            case IntervetionParameter.ScissorsCardChainAx:

                await ScissorsCardChainAxAsync();

                break;
        }

        PhaseManager.Instance.OnNextPhase();
    }

    private async UniTask LeaderCardShaman()
    {
        var shaman = _playerPresenter
                        .PlayerData
                        .LeaderHand
                        .HandEffect as ShamanData;

        var chainAx = typeof(ScissorsCardChainAx);
        var jammingWave = typeof(FScissorsCardJammingWave);
        var scissorsHands =
                    _playerPresenter
                        .PlayerData
                        .RSPHands
                        .Where(hand => 
                                hand.HandEffect.GetType() == chainAx ||
                                hand.HandEffect.GetType() == jammingWave);

        var methods = new List<UnityAction>();

        foreach (var scissorsHand in scissorsHands)
            methods.Add(() => shaman.SelectScissorsHand(scissorsHand));

        _handView.SelectCardForLeaderCardShaman
                    (methods, shaman.DontSelectScissorsHand, shaman.DecideScissorsHand,scissorsHands);

        await UniTask.WaitUntil(() => 
                PhaseManager.Instance.CurrentPhaseProperty != PhaseParameter.Intervention);

        _handView.InactiveScissorsHandButton
                    (methods, shaman.DontSelectScissorsHand,shaman.DecideScissorsHand);
    }

    private async UniTask FPaperCardJudgmentOfAigis()
    {
        var judgmentOfAigis =
            _playerPresenter
                .PlayerData
                .SetRSPHand
                .HandEffect as FPaperCardJudgmentOfAigis;

        var shildCount = _playerPresenter.PlayerData.Shield;

        _handView
            .SelectCardForFPaperCardJudgmentOfAigis
                (shildCount,
                    judgmentOfAigis.AddBreakCount,
                    judgmentOfAigis.RemoveBreakCount,
                    judgmentOfAigis.DecideBreakCount);

        var cts = new CancellationTokenSource();
        judgmentOfAigis.LimitSelectTime(cts.Token);

        await UniTask.WaitUntil(() => judgmentOfAigis.IsDecide);

        cts.Cancel();
        _handView.InactiveBreakCountButton
                    (judgmentOfAigis.AddBreakCount,
                        judgmentOfAigis.RemoveBreakCount,
                        judgmentOfAigis.DecideBreakCount);
    }

    private async UniTask PaperCardDrainShield()
    {
        var paperCardDrainShield =
            _playerPresenter
                .PlayerData
                .SetRSPHand
                .HandEffect as PaperCardDrainShield;


        PlayerInterface player = new();
        //if(_battleManager.PhaseManager.IsFirstPlayer)player = PlayerManager.Players[1];
        //else player = PlayerManager.Players[0];

        var methods = new List<UnityAction>();

        foreach (var enemyHand in player.PlayerParameter.RSPHands)
            methods.Add(() => paperCardDrainShield.SelectEnemyHand(enemyHand));

        _handView.SelectCardForPaperCardDrainShield
                    (methods,paperCardDrainShield.DecideEnemyHand);

        var cts = new CancellationTokenSource();

        paperCardDrainShield.LimitSelectTime(cts.Token);

        await UniTask.WaitUntil(() => paperCardDrainShield.IsDecide);

        cts.Cancel();

        _handView.InactiveEnemyHandButton(methods, paperCardDrainShield.DecideEnemyHand);
    }

    private async UniTask ScissorsCardChainAxAsync()
    {
        var scissorsCardChainAx =
            _playerPresenter
                .PlayerData
                .SetRSPHand
                .HandEffect as ScissorsCardChainAx;

        _handView.SelectCardForScissorsCardChainAx
                    (scissorsCardChainAx.PutCardBack,
                        scissorsCardChainAx.DontPutCardBack);

        var cts = new CancellationTokenSource();
        scissorsCardChainAx.LimitSelectTime(cts.Token);

        await UniTask.WaitUntil(() => scissorsCardChainAx.IsDecide);

        cts.Cancel();

        _handView.InactiveCardBackButton
                    (scissorsCardChainAx.PutCardBack,
                        scissorsCardChainAx.DontPutCardBack);
    }

    #endregion
}
