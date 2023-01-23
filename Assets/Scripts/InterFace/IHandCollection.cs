using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandCollection
{
    /// <summary>
    /// 手札を場にセットする関数
    /// </summary>
    /// <param name="playerHand"></param>
    void SetHand(PlayerHand playerHand);

    /// <summary>
    /// 場にセットされたカードをリザーブに送る関数
    /// </summary>
    void SetCardOnReserve();

    /// <summary>
    /// 手札にカードを加える関数
    /// </summary>
    /// <param name="playerHand"></param>
    void AddHand(PlayerHand playerHand);

    /// <summary>
    /// 手札をリザーブに送る関数
    /// </summary>
    /// <param name="playerHand"></param>
    void OnReserveHand(PlayerHand playerHand);

    /// <summary>
    /// リーダーカードをセットする関数
    /// </summary>
    /// <param name="leader"></param>
    void SetLeaderHand(LeaderPlayerHand leader);

    /// <summary>
    /// 手札を場にセットする関数
    /// </summary>
    void ResetHand();
}
