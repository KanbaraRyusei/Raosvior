using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;

public class CardPresenter : MonoBehaviour
{
    #region Properties

    private PhaseParameter CurrentPhase => PhaseManager.CurrentPhaseProperty;

    #endregion

    #region Inspector Variables

    [SerializeField]
    [Header("ハンドのビュー")]
    private CardView _handView;

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

                var player = _playerPresenter.PlayerData;
                var client = PlayerManager.Players[0].PlayerParameter;

                var isClient = player == client && PhaseManager.IsClient;
                var isOther = player != client && !PhaseManager.IsClient;

                if (isClient) DisplayViewByIntervetion();     
                else if(isOther) DisplayViewByIntervetion();

                break;
        }
    }

    private async void DisplayViewByIntervetion()
    {
        switch (PhaseManager.IntervetionProperty)
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

        PhaseManager.OnNextPhase();
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
                        .PlayerHands
                        .Where(hand => 
                                hand.HandEffect.GetType() == chainAx ||
                                hand.HandEffect.GetType() == jammingWave);

        var methods = new List<UnityAction>();

        foreach (var scissorsHand in scissorsHands)
            methods.Add(() => shaman.SelectScissorsHand(scissorsHand));

        _handView.SelectCardForLeaderCardShaman
                    (methods, shaman.DontSelectScissorsHand, shaman.DecideScissorsHand,scissorsHands);

        var cts = new CancellationTokenSource();

        shaman.LimitSelectTime(cts.Token);

        await UniTask.WaitUntil(() => shaman.IsDecide);

        cts.Cancel();

        _handView.InactiveScissorsHandButton
                    (methods, shaman.DontSelectScissorsHand,shaman.DecideScissorsHand);
    }

    private async UniTask FPaperCardJudgmentOfAigis()
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

    private async UniTask PaperCardDrainShield()
    {
        var paperCardDrainShield =
            _playerPresenter
                .PlayerData
                .PlayerSetHand
                .HandEffect as PaperCardDrainShield;


        PlayerInterface player;
        if(PhaseManager.IsClient)player = PlayerManager.Players[1];
        else player = PlayerManager.Players[0];

        var methods = new List<UnityAction>();

        foreach (var enemyHand in player.PlayerParameter.PlayerHands)
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

    #endregion
}
