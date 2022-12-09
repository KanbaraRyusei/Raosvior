using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    [Header("リーダー1カード")]
    List<LeaderPlayerHand> _leaderPlayerHands1 = new List<LeaderPlayerHand>(4);

    [SerializeField]
    [Header("リーダー2カード")]
    List<LeaderPlayerHand> _leaderPlayerHands2 = new List<LeaderPlayerHand>(4);

    [SerializeField]
    [Header("じゃんけん1カード")]
    List<PlayerHand> _playerRSPHands1 = new List<PlayerHand>(9);

    [SerializeField]
    [Header("じゃんけん2カード")]
    List<PlayerHand> _rSPPlayerHands2 = new List<PlayerHand>(9);

    /// <summary>
    /// プレイヤーにリーダーカードを渡す関数
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="leader"></param>
    public void SetLeaderHand(PlayerData playerData,LeaderParameter leader = 0)
    {
        var randomIndex = 0;
        if (playerData == PlayerManager.Players[0])
        {
            foreach (var leaderHand in _leaderPlayerHands1)
            {
                if (leaderHand.Leader == leader)
                {
                    PlayerManager.Players[0].AddLeaderHand(leaderHand);
                }
            }
            randomIndex = Random.Range(0, _leaderPlayerHands1.Count);
            PlayerManager.Players[0].AddLeaderHand(_leaderPlayerHands1[randomIndex]);
        }
        else
        {
            foreach (var leaderHand in _leaderPlayerHands2)
            {
                if (leaderHand.Leader == leader)
                {
                    PlayerManager.Players[1].AddLeaderHand(leaderHand);
                }
            }
            randomIndex = Random.Range(0, _leaderPlayerHands2.Count);
            PlayerManager.Players[1].AddLeaderHand(_leaderPlayerHands2[randomIndex]);
        }
    }



    public void SetRSPHand(PlayerData playerData)
    {

    }
}
