using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リーダーの効果の基底クラス
/// </summary>
public abstract class LeaderHandEffect : HandEffect
{
    #region public propterty

    public LeaderParameter LeaderType { get; protected set; }

    #endregion

    #region constants

    protected RSPParameter ROCK = RSPParameter.Rock;
    protected RSPParameter SCISSORS = RSPParameter.Scissors;
    protected RSPParameter PAPER = RSPParameter.Paper;

    #endregion

    #region public method

    /// <summary>
    /// じゃんけんの勝敗がついたときに呼び出される
    /// </summary>
    public abstract void CardEffect(PlayerData player);

    #endregion
}
