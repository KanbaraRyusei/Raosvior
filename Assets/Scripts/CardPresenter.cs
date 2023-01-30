using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

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
                switch (phase)
                {
                    case PhaseParameter.HandSelect:

                        _handView.SelectHand();

                        break;

                    case PhaseParameter.CardSelect:

                        _handView.SelectCard(_playerPresenter.PlayerData.PlayerHands);

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

                        switch (PhaseManager.IntervetionProperty)
                        {
                            case IntervetionParameter.LeaderCardShaman:

                                var scissorsHands =
                                    _playerPresenter
                                        .PlayerData
                                        .PlayerHands
                                        .Where(x => x.HandEffect.GetType() == typeof(ScissorsCardChainAx) ||                                x.HandEffect.GetType() == typeof(FScissorsCardJammingWave));

                                _handView.SelectCardForLeaderCardShaman(scissorsHands);

                                break;

                            case IntervetionParameter.FPaperCardJudgmentOfAigis:

                                var shildCount = _playerPresenter.PlayerData.Shield;
                                _handView.SelectCardForFPaperCardJudgmentOfAigis(shildCount);

                                break;

                            case IntervetionParameter.PaperCardDrainShield:

                                IReadOnlyList<PlayerHand> enemyHands;
                                if (_playerPresenter.PlayerData != PlayerManager.Players[0].PlayerParameter)
                                {
                                    enemyHands = PlayerManager.Players[0].PlayerParameter.PlayerHands;
                                }
                                else enemyHands = PlayerManager.Players[1].PlayerParameter.PlayerHands;

                                _handView.SelectCardForPaperCardDrainShield(enemyHands);

                                break;

                            case IntervetionParameter.ScissorsCardChainAx:

                                _handView.SelectCardForScissorsCardChainAx();

                                break;
                        }

                        break;
                }
            }
            ).AddTo(this);
    }
}
