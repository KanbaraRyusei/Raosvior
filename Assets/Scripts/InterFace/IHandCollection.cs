using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandCollection
{
    /// <summary>
    /// 手札を場にセットする関数
    /// </summary>
    /// <param name="playerHand"></param>
    void SetHand(RSPPlayerHand playerHand);

    /// <summary>
    /// 場にセットされたカードをリザーブに送る関数
    /// </summary>
    void SetCardOnReserve();

    /// <summary>
    /// 手札にカードを加える関数
    /// </summary>
    /// <param name="playerHand"></param>
    void AddHand(RSPPlayerHand playerHand);

    /// <summary>
    /// 手札のカードを消す関数
    /// </summary>
    /// <param name="playerHand"></param>
    void RemoveHand(RSPPlayerHand playerHand);

    /// <summary>
    /// 手札をリザーブに送る関数
    /// </summary>
    /// <param name="playerHand"></param>
    void OnReserveHand(RSPPlayerHand playerHand);

    /// <summary>
    /// セットしたカードを手札に戻す関数
    /// </summary>
    void CardBack();

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
