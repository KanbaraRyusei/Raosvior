using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class HandSetter : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField]
    private LeaderPlayerHand[] _leaderPlayerHands = null;

    [SerializeField]
    private HandSelecter _handSelecter = null;

    [SerializeField]
    private RSPPlayerHand[] _rspPlayerHands = null;

    #endregion

    #region Member Variables

    private LeaderHandData[] _clientLeaderHands = new LeaderHandData[LEADER_COUNT];
    private LeaderHandData[] _otherLeaderHands = new LeaderHandData[LEADER_COUNT];
    private List<RSPHandData> _clientRSPHands = new(RSP_COUNT);
    private List<RSPHandData> _otherRSPHands = new(RSP_COUNT);

    #endregion

    #region Constants

    private const int LEADER_COUNT = 4;
    private const int RSP_COUNT = 9;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        SetLeaderData();
        SetRSPData();
    }

    #endregion

    #region Private Methods

    private void SetLeaderData()
    {
        var client = PlayerManager.Instance.Players[0];
        var other = PlayerManager.Instance.Players[1];

        for (int i = 0; i < LEADER_COUNT; i++)
        {
            LeaderHandEffect clientEffect = new ArcherData();
            switch (_leaderPlayerHands[i].CardName)
            {
                case "シャーマン":

                    clientEffect = new ShamanData();

                    break;

                case "ナイト":

                    clientEffect = new KnightData();

                    break;

                case "グラップラー":

                    clientEffect = new GrapplerData();

                    break;
            }
            clientEffect.SetPlayerInterface(client, other);
            _clientLeaderHands[i] = new LeaderHandData(_leaderPlayerHands[i], clientEffect);

            LeaderHandEffect otherEffect = new ArcherData();
            switch (_leaderPlayerHands[i].CardName)
            {
                case "シャーマン":

                    otherEffect = new ShamanData();

                    break;

                case "ナイト":

                    otherEffect = new KnightData();

                    break;

                case "グラップラー":

                    otherEffect = new GrapplerData();

                    break;
            }
            otherEffect.SetPlayerInterface(other, client);
            _otherLeaderHands[i] = new LeaderHandData(_leaderPlayerHands[i], otherEffect);
        }

        _handSelecter.GetClientLeaderHands(_clientLeaderHands);
        _handSelecter.GetClientLeaderHands(_otherLeaderHands);
    }

    private void SetRSPData()
    {
        var client = PlayerManager.Instance.Players[0];
        var other = PlayerManager.Instance.Players[1];

        for (int i = 0; i < RSP_COUNT; i++)
        {
            RSPHandEffect clientEffect = new FRockCardExplosion();
            switch (_leaderPlayerHands[i].CardName)
            {
                case "炸裂する矢":

                    clientEffect = new RockCardExplodingArrow();
                    _otherRSPHands.Add(new(_rspPlayerHands[i], clientEffect));

                    break;

                case "ジャミングウェーブ・F":

                    clientEffect = new FScissorsCardJammingWave();

                    break;

                case "チェーンアックス":

                    clientEffect = new ScissorsCardChainAx();
                    _otherRSPHands.Add(new(_rspPlayerHands[i], clientEffect));

                    break;

                case "アイギスの裁き・F":

                    clientEffect = new FPaperCardJudgmentOfAigis();

                    break;

                case "ドレインシールド":

                    clientEffect = new PaperCardDrainShield();
                    _otherRSPHands.Add(new(_rspPlayerHands[i], clientEffect));

                    break;
            }

            clientEffect.SetPlayerInterface(client, other);
            _clientRSPHands.Add(new(_rspPlayerHands[i], clientEffect));

            RSPHandEffect otherEffect = new FRockCardExplosion();
            switch (_leaderPlayerHands[i].CardName)
            {
                case "炸裂する矢":

                    otherEffect = new RockCardExplodingArrow();
                    _otherRSPHands.Add(new(_rspPlayerHands[i], otherEffect));

                    break;

                case "ジャミングウェーブ・F":

                    otherEffect = new FScissorsCardJammingWave();

                    break;

                case "チェーンアックス":

                    otherEffect = new ScissorsCardChainAx();
                    _otherRSPHands.Add(new(_rspPlayerHands[i], otherEffect));

                    break;

                case "アイギスの裁き・F":

                    otherEffect = new FPaperCardJudgmentOfAigis();

                    break;

                case "ドレインシールド":

                    otherEffect = new PaperCardDrainShield();
                    _otherRSPHands.Add(new(_rspPlayerHands[i], otherEffect));

                    break;
            }

            otherEffect.SetPlayerInterface(other, client);
            _otherRSPHands.Add(new(_rspPlayerHands[i], otherEffect));
        }

        _handSelecter.GetClientRSPHands(_clientRSPHands);
        _handSelecter.GetClientRSPHands(_otherRSPHands);
    }

    #endregion
}
