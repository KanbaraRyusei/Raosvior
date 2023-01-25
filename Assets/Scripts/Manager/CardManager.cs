using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全てのカードを管理しているマネージャー
/// 最初の手札選択フェーズのみで使用する
/// </summary>
public class CardManager : MonoBehaviour
{
    [SerializeField]
    [Header("リーダーカード")]
    List<LeaderPlayerHand> _leaderHands = new List<LeaderPlayerHand>(4);

    [SerializeField]
    [Header("プレイヤー用のじゃんけんカード")]
    List<PlayerHand> _CliantRSPHands = new List<PlayerHand>(9);

    [SerializeField]
    [Header("もう片方のプレイヤー用のじゃんけんカード")]
    List<PlayerHand> _otherRSPHands = new List<PlayerHand>(9);

    /// <summary>
    /// プレイヤーにリーダーカードを渡す関数
    /// </summary>
    /// /// <param name="playerData">プレイヤー</param>
    /// <param name="leader">カードの種類(nullならランダム)</param>
    public void SetLeaderHand(PlayerInterface player, string name = "")
    {
        foreach (var hand in _leaderHands)
        {
            if (hand.CardName == name)
            {
                hand.HandEffect.SetPlayerData(player);
                player.HandCollection.SetLeaderHand(hand);
            }
        }
        var randomIndex = Random.Range(0, _leaderHands.Count);
        _leaderHands[randomIndex].HandEffect.SetPlayerData(player);
        player.HandCollection.SetLeaderHand(_leaderHands[randomIndex]);
    }

    /// <summary>
    /// プレイヤーにじゃんけんカードを渡す関数
    /// </summary>
    /// <param name="playerData">プレイヤー</param>
    /// <param name="handName">カード名(nullならランダム)</param>
    public void SetRSPHand(PlayerInterface playerData, string handName = "")
    {
        var randomIndex = 0;
        if (playerData == PlayerManager.Players[0])
        {
            var player = PlayerManager.Players[0];
            foreach (var hand in _CliantRSPHands)
            {
                if (hand.CardName == handName)
                {
                    hand.HandEffect.SetPlayerData(playerData);
                    player.HandCollection.AddHand(hand);
                    _CliantRSPHands.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _CliantRSPHands.Count);
            PlayerManager.Players[0].HandCollection.AddHand(_CliantRSPHands[randomIndex]);
            _CliantRSPHands.Remove(_CliantRSPHands[randomIndex]);
        }
        else
        {
            var player = PlayerManager.Players[1];
            foreach (var hand in _otherRSPHands)
            {
                if (hand.CardName == handName)
                {
                    hand.HandEffect.SetPlayerData(playerData);
                    player.HandCollection.AddHand(hand);
                    _otherRSPHands.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _otherRSPHands.Count);
            player.HandCollection.AddHand(_otherRSPHands[randomIndex]);
            _otherRSPHands.Remove(_otherRSPHands[randomIndex]);
        }
    }
}
