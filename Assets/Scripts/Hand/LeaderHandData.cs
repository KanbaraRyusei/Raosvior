using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class LeaderHandData : HandDataBase
{
    #region Properties

    public LeaderPlayerHand LeaderHand { get; private set; }
    public LeaderHandEffect HandEffect { get; private set; }

    #endregion

    #region Constructor

    public LeaderHandData(LeaderPlayerHand hand, LeaderHandEffect effect)
    {
        LeaderHand = hand;
        HandEffect = effect;
    }

    #endregion
}
