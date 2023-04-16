using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全てのカードを管理しているマネージャー
/// 最初の手札選択フェーズのみで使用する
/// </summary>
public class CardManager : MonoBehaviour
{
    #region Inspector Variables

    [SerializeField]
    private RPCManager rpcManager = null;

    [SerializeField]
    [Header("プレイヤー用のリーダーカード")]
    private LeaderPlayerHand[] _clientLeaderHands = new LeaderPlayerHand[4];

    [SerializeField]
    [Header("もう片方のプレイヤー用のリーダーカード")]
    private LeaderPlayerHand[] _otherLeaderHands = new LeaderPlayerHand[4];

    [SerializeField]
    [Header("プレイヤー用のじゃんけんカード")]
    private List<RSPPlayerHand> _clientRSPHands = new(9);

    [SerializeField]
    [Header("もう片方のプレイヤー用のじゃんけんカード")]
    private List<RSPPlayerHand> _otherRSPHands = new(9);

    #endregion

    #region Public Methods

    /// <summary>
    /// プレイヤーにリーダーカードを渡す関数
    /// </summary>
    /// /// <param name="playerData">プレイヤー</param>
    /// <param name="leader">カードの種類(nullならランダム)</param>
    public void SetLeaderHand(int index, string name = "")
    {
        int randomIndex;
        var player = PlayerManager.Players[index];
        var leaderHands = index == 0 ? _clientLeaderHands : _otherLeaderHands;

        foreach (var hand in leaderHands)
        {
            if (hand.CardName == name)
            {
                hand.HandEffect.ChangePlayersIndex(player);
                player.HandCollection.SetLeaderHand(hand);
            }
        }
        randomIndex = Random.Range(0, leaderHands.Length);
        leaderHands[randomIndex].HandEffect.ChangePlayersIndex(player);
        player.HandCollection.SetLeaderHand(leaderHands[randomIndex]);
    }

    /// <summary>
    /// プレイヤーにじゃんけんカードを渡す関数
    /// </summary>
    /// <param name="playerData">プレイヤー</param>
    /// <param name="handName">カード名(nullならランダム)</param>
    public void SetRSPHand(int index, string handName = "")
    {
        int randomIndex;
        var player = PlayerManager.Players[index];
        var rspHands = index == 0 ? _clientRSPHands : _otherRSPHands;

        foreach (var hand in rspHands)
        {
            if (hand.CardName == handName)
            {
                hand.HandEffect.ChangePlayersIndex(player);
                player.HandCollection.AddHand(hand);
                rspHands.Remove(hand);
            }
        }
        randomIndex = Random.Range(0, rspHands.Count);
        PlayerManager.Players[0].HandCollection.AddHand(rspHands[randomIndex]);
        rspHands.Remove(rspHands[randomIndex]);
    }

    #endregion
}
