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
    List<PlayerHand> _rSPHands = new List<PlayerHand>(9);

    [SerializeField]
    [Header("もう片方のプレイヤー用のじゃんけんカード")]
    List<PlayerHand> _anotherRSPHands = new List<PlayerHand>(9);

    /// <summary>
    /// プレイヤーにリーダーカードを渡す関数
    /// </summary>
    /// /// <param name="playerData">プレイヤー</param>
    /// <param name="leader">カードの種類(nullならランダム)</param>
    public void SetLeaderHand(PlayerData player, LeaderParameter leader = 0)
    {
        var randomIndex = 0;
        foreach (var hand in _leaderHands)
        {
            if (hand.Leader == leader)
            {
                player.SetLeaderHand(hand);
            }
        }
        randomIndex = Random.Range(0, _leaderHands.Count);
        player.SetLeaderHand(_leaderHands[randomIndex]);
    }

    /// <summary>
    /// プレイヤーにじゃんけんカードを渡す関数
    /// </summary>
    /// <param name="playerData">プレイヤー</param>
    /// <param name="handName">カード名(nullならランダム)</param>
    public void SetRSPHand(PlayerData playerData, string handName = "")
    {
        var randomIndex = 0;
        if (playerData == PlayerManager.Players[0])
        {
            var player = PlayerManager.Players[0];
            foreach (var hand in _rSPHands)
            {
                if(hand.CardName == handName)
                {
                    player.AddHand(hand);
                    _rSPHands.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _rSPHands.Count);
            player.AddHand(_rSPHands[randomIndex]);
            _rSPHands.Remove(_rSPHands[randomIndex]);
        }
        else
        {
            foreach (var hand in _anotherRSPHands)
            {
                if (hand.CardName == handName)
                {
                    PlayerManager.Players[1].AddHand(hand);
                    _anotherRSPHands.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _anotherRSPHands.Count);
            PlayerManager.Players[1].AddHand(_anotherRSPHands[randomIndex]);
            _anotherRSPHands.Remove(_anotherRSPHands[randomIndex]);
        }
    }

    public void SelectRSPHand(PlayerData playerData, string handName = "")
    {
        var randomIndex = 0;
        if (playerData == PlayerManager.Players[0])
        {
            var player = PlayerManager.Players[0];
            foreach (var hand in _rSPHands)
            {
                if (hand.CardName == handName)
                {
                    player.AddHand(hand);
                    _rSPHands.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _rSPHands.Count);
            player.AddHand(_rSPHands[randomIndex]);
            _rSPHands.Remove(_rSPHands[randomIndex]);
        }
        else
        {
            foreach (var hand in _anotherRSPHands)
            {
                if (hand.CardName == handName)
                {
                    PlayerManager.Players[1].AddHand(hand);
                    _anotherRSPHands.Remove(hand);
                }
            }
            randomIndex = Random.Range(0, _anotherRSPHands.Count);
            PlayerManager.Players[1].AddHand(_anotherRSPHands[randomIndex]);
            _anotherRSPHands.Remove(_anotherRSPHands[randomIndex]);
        }
    }
}
