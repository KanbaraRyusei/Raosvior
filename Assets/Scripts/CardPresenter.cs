using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;

public class CardPresenter : MonoBehaviour
{
    private PhaseParameter CurrentPhase => PhaseManager.CurrentPhaseProperty;

    [SerializeField]
    [Header("ハンドのビュー")]
    private CardView _handView;

    [SerializeField]
    [Header("プレイヤーのプレゼンター")]
    private PlayerPresenter _playerPresenter;

    private void Awake()
    {
        this
            .ObserveEveryValueChanged(x => x.CurrentPhase)
            .Subscribe(phase =>
            {
                DisplayViewByPhase(phase);
            }
            ).AddTo(this);
    }

    private void DisplayViewByPhase(PhaseParameter phase)
    {
        switch (phase)
        {
            case PhaseParameter.HandSelect:

                _handView.SelectAllHand();

                break;

            case PhaseParameter.CardSelect:

                _handView.SelectRSPCard(_playerPresenter.PlayerData.PlayerHands);

                break;

            case PhaseParameter.Battle:
                break;
            case PhaseParameter.WinnerDamageProcess:
                break;
            case PhaseParameter.WinnerCardEffect:
                break;
            case PhaseParameter.CharacterEffect:
                break;
            case PhaseParameter.StockEffect:
                break;
            case PhaseParameter.UseCardOnReserve:
                break;
            case PhaseParameter.Refresh:
                break;
            case PhaseParameter.Judgement:
                break;

            case PhaseParameter.Intervention:

                DisplayViewByIntervetion();

                break;
        }
    }

    private void DisplayViewByIntervetion()
    {
        switch (PhaseManager.IntervetionProperty)
        {
            case IntervetionParameter.LeaderCardShaman:

                LeaderCardShaman();

                break;

            case IntervetionParameter.FPaperCardJudgmentOfAigis:

                FPaperCardJudgmentOfAigis();

                break;

            case IntervetionParameter.PaperCardDrainShield:

                PaperCardDrainShield();

                break;

            case IntervetionParameter.ScissorsCardChainAx:

                ScissorsCardChainAxAsync();

                break;
        }
    }

    private void LeaderCardShaman()
    {
        var shaman = _playerPresenter
                        .PlayerData
                        .LeaderHand
                        .HandEffect as ShamanData;

        var scissorsHands =
                    _playerPresenter
                        .PlayerData
                        .PlayerHands
                        .Where(x => x.HandEffect.GetType() == typeof(ScissorsCardChainAx) || x.HandEffect.GetType() == typeof(FScissorsCardJammingWave));

        _handView.SelectCardForLeaderCardShaman(scissorsHands);

    }

    async private void FPaperCardJudgmentOfAigis()
    {
        var judgmentOfAigis =
            _playerPresenter
                .PlayerData
                .PlayerSetHand
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

    private void PaperCardDrainShield()
    {
        var paperCardDrainShield =
            _playerPresenter
                .PlayerData
                .PlayerSetHand
                .HandEffect as PaperCardDrainShield;

        _handView.SelectCardForPaperCardDrainShield
                    (paperCardDrainShield.EnemyHandCount);


    }

    async private void ScissorsCardChainAxAsync()
    {
        var scissorsCardChainAx =
            _playerPresenter
                .PlayerData
                .PlayerSetHand
                .HandEffect as ScissorsCardChainAx;

        _handView.SelectCardForScissorsCardChainAx
                    (scissorsCardChainAx.CardBack,
                        scissorsCardChainAx.NotCardBack);

        var cts = new CancellationTokenSource();
        scissorsCardChainAx.LimitSelectTime(cts.Token);

        await UniTask.WaitUntil(() => scissorsCardChainAx.IsDecide);

        cts.Cancel();

        _handView.InactiveCardBackButton
                    (scissorsCardChainAx.CardBack,
                        scissorsCardChainAx.NotCardBack);
    }
}
