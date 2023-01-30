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
    [Header("プレイヤー用のリーダーカード")]
    List<LeaderPlayerHand> _cliantLeaderHands = new List<LeaderPlayerHand>(4);

    [SerializeField]
    [Header("もう片方のプレイヤー用のリーダーカード")]
    List<LeaderPlayerHand> _otherLeaderHands = new List<LeaderPlayerHand>(4);

    [SerializeField]
    [Header("プレイヤー用のじゃんけんカード")]
    List<PlayerHand> _cliantRSPHands = new List<PlayerHand>(9);

    [SerializeField]
    [Header("もう片方のプレイヤー用のじゃんけんカード")]
    List<PlayerHand> _otherRSPHands = new List<PlayerHand>(9);

    /// <summary>
    /// プレイヤーにリーダーカードを渡す関数
    /// </summary>
    /// /// <param name="playerData">プレイヤー</param>
    /// <param name="leader">カードの種類(nullならランダム)</param>
    public void SetLeaderHand(PlayerInterface playerData, string name = "")
    {
        var randomIndex = 0;
        if (playerData == PlayerManager.Players[0])
        {
            var player = PlayerManager.Players[0];
            foreach (var hand in _cliantLeaderHands)
            {
                if (hand.CardName == name)
                {
                    hand.HandEffect.SetPlayerData(player);
                    player.HandCollection.SetLeaderHand(hand);
                }
            }
            randomIndex = Random.Range(0, _cliantLeaderHands.Count);
            _cliantLeaderHands[randomIndex].HandEffect.SetPlayerData(player);
            player.HandCollection.SetLeaderHand(_cliantLeaderHands[randomIndex]);
        }
        else
        {
            var player = PlayerManager.Players[1];
            foreach (var hand in _otherLeaderHands)
            {
                if (hand.CardName == name)
                {
                    hand.HandEffect.SetPlayerData(player);
                    player.HandCollection.SetLeaderHand(hand);
                }
            }
            randomIndex = Random.Range(0, _otherLeaderHands.Count);
            _otherLeaderHands[randomIndex].HandEffect.SetPlayerData(player);
            player.HandCollection.SetLeaderHand(_otherLeaderHands[randomIndex]);
        }
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
            foreach (var hand in _cliantRSPHands)
            {
                if (hand.CardName == handName)
                {
                    hand.HandEffect.SetPlayerData(playerData);
                    player.HandCollection.AddHand(hand);
                    _cliantRSPHands.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _cliantRSPHands.Count);
            PlayerManager.Players[0].HandCollection.AddHand(_cliantRSPHands[randomIndex]);
            _cliantRSPHands.Remove(_cliantRSPHands[randomIndex]);
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
