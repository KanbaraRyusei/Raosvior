using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リーダーの効果の基底クラス
/// </summary>
public abstract class LeaderHandEffect : HandEffect
{
    #region Constants

    protected RSPParameter ROCK = RSPParameter.Rock;
    protected RSPParameter SCISSORS = RSPParameter.Scissors;
    protected RSPParameter PAPER = RSPParameter.Paper;

    #endregion

    #region Public Method

    /// <summary>
    /// じゃんけんの勝敗がついたときに呼び出される
    /// </summary>
    public abstract void CardEffect();

    #endregion
}
