using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全てのカードを管理しているマネージャー
/// 最初の手札選択フェーズのみで使用する
/// </summary>
public class HandSelecter : MonoBehaviour
{
    #region Properties

    public LeaderHandData[] ClientLeaderHands { get; private set; } = new LeaderHandData[4];
    public LeaderHandData[] OtherLeaderHands { get; private set; } = new LeaderHandData[4];
    public IReadOnlyList<RSPHandData> ClientRSPHands => _clientRSPHands;
    public IReadOnlyList<RSPHandData> OtherRSPHands => _otherRSPHands;

    #endregion

    #region Member Variables

    /// <summary>
    /// プレイヤー用のじゃんけんカード
    /// </summary>
    private List<RSPHandData> _clientRSPHands = new(9);

    /// <summary>
    /// もう片方のプレイヤー用のじゃんけんカード
    /// </summary>
    private List<RSPHandData> _otherRSPHands = new(9);

    #endregion

    #region Unity Methods

    private void Awake()
    {
        RPCManager.Instance.OnSelectLeaderHand += SelectLeaderHand;
        RPCManager.Instance.OnSelectRSPHand += SelectRSPHand;
        RPCManager.Instance.OnSetRSPHand += SetRSPHand;

        RPCManager.Instance.OnPaperCardDrainShield += SelectEnemyRSPHand;
        RPCManager.Instance.OnFPaperCardJudgmentOfAigis += SelectShildCount;
        RPCManager.Instance.OnScissorsCardChainAx += PutCardBack;
        RPCManager.Instance.OnShaman += ReserveHand;
    }

    #endregion

    #region Public Methods

    public void GetClientLeaderHands(LeaderHandData[] leaderHandsData)
    {
        ClientLeaderHands = leaderHandsData;
    }

    public void GetOtherLeaderHands(LeaderHandData[] leaderHandsData)
    {
        OtherLeaderHands = leaderHandsData;
    }

    public void GetClientRSPHands(List<RSPHandData> rspHandsData)
    {
        _clientRSPHands = rspHandsData;
    }

    public void GetOtherRSPHands(List<RSPHandData> rspHandsData)
    {
        _otherRSPHands = rspHandsData;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// プレイヤーにリーダーカードを渡す関数
    /// </summary>
    /// /// <param name="playerData">プレイヤー</param>
    /// <param name="leader">カードの種類(nullならランダム)</param>
    private void SelectLeaderHand(int index, string name = "")
    {
        var player = PlayerManager.Instance.Players[index];
        var leaderHands = index == 0 ? ClientLeaderHands : OtherLeaderHands;

        foreach (var hand in leaderHands)
        {
            if (hand.LeaderHand.CardName == name)
            {
                player.HandCollection.SetLeaderHand(hand);
                RPCManager.Instance.SendSelectLeaderHand(index, name);
                return;
            }
        }
        
        int randomIndex = Random.Range(0, leaderHands.Length);
        player.HandCollection.SetLeaderHand(leaderHands[randomIndex]);
        RPCManager.Instance.SendSelectLeaderHand(index, name);
    }

    /// <summary>
    /// プレイヤーにじゃんけんカードを渡す関数
    /// </summary>
    /// <param name="playerData">プレイヤー</param>
    /// <param name="handName">カード名(nullならランダム)</param>
    private void SelectRSPHand(int index, string handName = "")
    {
        var player = PlayerManager.Instance.Players[index];
        var rspHands = index == 0 ? _clientRSPHands : _otherRSPHands;

        foreach (var hand in rspHands)
        {
            if (hand.RSPHand.CardName == handName)
            {
                player.HandCollection.AddHand(hand);
                rspHands.Remove(hand);
                return;
            }
        }
        int randomIndex = Random.Range(0, rspHands.Count);
        player.HandCollection.AddHand(rspHands[randomIndex]);
        rspHands.Remove(rspHands[randomIndex]);
    }

    /// <summary>
    /// じゃんけんカードをセットする関数
    /// </summary>
    /// <param name="index"></param>
    /// <param name="handName"></param>
    private void SetRSPHand(int index, string handName = "")
    {
        var player = PlayerManager.Instance.Players[index];
        var rspHands = PlayerManager.Instance.Players[index].PlayerParameter.RSPHands;
        foreach (var hand in rspHands)
        {
            if (hand.RSPHand.CardName == handName)
            {
                player.HandCollection.SetHand(hand);
                return;
            }
        }
        int randomIndex = Random.Range(0, rspHands.Count);
        player.HandCollection.SetHand(rspHands[randomIndex]);
    }

    /// <summary>
    /// シールドトークンにする敵のじゃんけんカードを渡す関数
    /// </summary>
    /// <param name="index"></param>
    /// <param name="handName"></param>
    private void SelectEnemyRSPHand(int index, string handName = "")
    {
        var enemyIndex = index == 0 ? 1 : 0;
        var player = PlayerManager.Instance.Players[index];
        var enemy = PlayerManager.Instance.Players[enemyIndex];

        foreach (var hand in enemy.PlayerParameter.RSPHands)
        {
            if (hand.RSPHand.CardName == handName)
                enemy.HandCollection.RemoveHand(hand);
        }
        foreach (var hand in player.PlayerParameter.RSPHands)
        {
            if (hand.RSPHand.CardName == handName)
                player.GetableShield.GetShield(playerHand: hand);
        }
    }

    /// <summary>
    /// シールド数を選ぶ関数
    /// </summary>
    /// <param name="index"></param>
    /// <param name="shild"></param>
    private void SelectShildCount(int index, int shild)
    {
        var enemyIndex = index == 0 ? 1 : 0;
        var player = PlayerManager.Instance.Players[index];
        var enemy = PlayerManager.Instance.Players[enemyIndex];

        player.GetableShield.GetShield(-shild);
        enemy.ChangeableLife.ReceiveDamage(shild);
    }

    /// <summary>
    /// カードを戻すか選択する関数
    /// </summary>
    /// <param name="index"></param>
    private void PutCardBack(int index)
    {
        var player = PlayerManager.Instance.Players[index];
        player.HandCollection.PutCardBack();
    }

    private void ReserveHand(int index, string handName)
    {
        var player = PlayerManager.Instance.Players[index];
        foreach (var hand in player.PlayerParameter.RSPHands)
        {
            if (hand.RSPHand.CardName == handName)
            {
                player.HandCollection.OnReserveHand(hand);
            }
        }
    }

    #endregion
}
