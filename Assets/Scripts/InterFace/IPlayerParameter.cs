using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerParameter
{
    /// <summary>
    /// プレイヤーのライフの読み取り専用プロパティ
    /// </summary>
    int Life { get; }

    /// <summary>
    /// プレイヤーのシールドの読み取り専用プロパティ
    /// </summary>
    int Shield { get; }

    /// <summary>
    /// プレイヤーのリーダーカードの読み取り専用プロパティ
    /// </summary>
    LeaderPlayerHand LeaderHand { get; }

    /// <summary>
    /// プレイヤーの手札の読み取り専用プロパティ
    /// キャストして変更しようとする奴は殺すので要注意
    /// </summary>
    IReadOnlyList<PlayerHand> PlayerHands { get; }

    /// <summary>
    /// プレイヤーのリザーブの読み取り専用プロパティ
    /// キャストして変更しようとする奴は殺すので要注意
    /// </summary>
    IReadOnlyList<PlayerHand> PlayerReserve { get; }

    /// <summary>
    /// 場にセットするカードの読み取り専用プロパティ
    /// </summary>
    PlayerHand PlayerSetHand { get; }
}
