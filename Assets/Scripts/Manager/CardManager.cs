using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    [Header("リーダーカード")]
    List<LeaderPlayerHand> _leaderPlayerHands = new List<LeaderPlayerHand>(4);

    [SerializeField]
    [Header("プレイヤー用のじゃんけんカード")]
    List<PlayerHand> _rSPPlayerHands1 = new List<PlayerHand>(9);

    [SerializeField]
    [Header("もう片方のプレイヤー用のじゃんけんカード")]
    List<PlayerHand> _rSPPlayerHands2 = new List<PlayerHand>(9);

    /// <summary>
    /// プレイヤーにリーダーカードを渡す関数
    /// </summary>
    public void SetLeaderHand(PlayerData player, LeaderParameter leader = 0)
    {
        var randomIndex = 0;
        foreach (var hand in _leaderPlayerHands)
        {
            if (hand.Leader == leader)
            {
                player.AddLeaderHand(hand);
            }
        }
        randomIndex = Random.Range(0, _leaderPlayerHands.Count);
        player.AddLeaderHand(_leaderPlayerHands[randomIndex]);
    }

    public void SetRSPHand(PlayerData playerData, string handName = "")
    {
        var randomIndex = 0;
        if (playerData == PlayerManager.Players[0])
        {
            var player = PlayerManager.Players[0];
            foreach (var hand in _rSPPlayerHands1)
            {
                if(hand.CardName == handName)
                {
                    player.AddHand(hand);
                    _rSPPlayerHands1.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _rSPPlayerHands1.Count);
            player.AddHand(_rSPPlayerHands1[randomIndex]);
            _rSPPlayerHands1.Remove(_rSPPlayerHands1[randomIndex]);
        }
        else
        {
            foreach (var hand in _rSPPlayerHands2)
            {
                if (hand.CardName == handName)
                {
                    PlayerManager.Players[1].AddHand(hand);
                    _rSPPlayerHands2.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _rSPPlayerHands2.Count);
            PlayerManager.Players[1].AddHand(_rSPPlayerHands2[randomIndex]);
            _rSPPlayerHands2.Remove(_rSPPlayerHands2[randomIndex]);
        }
    }
}
