using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class LeaderHandData : HandDataBase
{
    #region Properties

    public LeaderPlayerHand Hand { get; private set; }
    public LeaderHandEffect HandEffect { get; private set; }

    #endregion

    #region Constructor

    public LeaderHandData(LeaderPlayerHand hand, LeaderHandEffect effect)
    {
        Hand = hand;
        HandEffect = effect;
    }

    #endregion
}
