using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public class RSPHandData : HandDataBase
{
    #region Properties

    public RSPPlayerHand RSPHand { get; private set; }
    public RSPHandEffect HandEffect { get; private set; }

    #endregion

    #region Constructor

    public RSPHandData(RSPPlayerHand hand, RSPHandEffect effect)
    {
        RSPHand = hand;
        HandEffect = effect;
    }

    #endregion
}
