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
    private RPCManager _rpcManager = null;

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

    #region Unity Methods

    private void Awake()
    {
        
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// プレイヤーにリーダーカードを渡す関数
    /// </summary>
    /// /// <param name="playerData">プレイヤー</param>
    /// <param name="leader">カードの種類(nullならランダム)</param>
    public void SelectLeaderHand(int index, string name = "")
    {
        var player = PlayerManager.Players[index];
        var leaderHands = index == 0 ? _clientLeaderHands : _otherLeaderHands;

        foreach (var hand in leaderHands)
        {
            if (hand.CardName == name)
            {
                hand.HandEffect.ChangePlayersIndex(player);
                player.HandCollection.SetLeaderHand(hand);
                return;
            }
        }
        
        int randomIndex = Random.Range(0, leaderHands.Length);
        leaderHands[randomIndex].HandEffect.ChangePlayersIndex(player);
        player.HandCollection.SetLeaderHand(leaderHands[randomIndex]);
    }

    /// <summary>
    /// プレイヤーにじゃんけんカードを渡す関数
    /// </summary>
    /// <param name="playerData">プレイヤー</param>
    /// <param name="handName">カード名(nullならランダム)</param>
    public void SelectRSPHand(int index, string handName = "")
    {
        var player = PlayerManager.Players[index];
        var rspHands = index == 0 ? _clientRSPHands : _otherRSPHands;

        foreach (var hand in rspHands)
        {
            if (hand.CardName == handName)
            {
                hand.HandEffect.ChangePlayersIndex(player);
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
    public void SetRSPHand(int index, string handName = "")
    {
        var player = PlayerManager.Players[index];
        var rspHands = PlayerManager.Players[index].PlayerParameter.PlayerHands;
        foreach (var hand in rspHands)
        {
            if (hand.CardName == handName)
            {
                player.HandCollection.SetHand(hand);
                return;
            }
        }
        int randomIndex = Random.Range(0, rspHands.Count);
        player.HandCollection.SetHand(rspHands[randomIndex]);
    }

    #endregion
}
