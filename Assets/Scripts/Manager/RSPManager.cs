using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// じゃんけんの勝敗を手助けしてくれるマネージャー
/// </summary>
public struct RSPManager
{
    #region Constants

    public const int WIN = 2;
    public const int DRAW = 0;
    public const int LOSE = 1;

    private const int RSP_OFFSET = 3;
    private const int RSP_REMAINDER = 3;

    #endregion

    #region Public Method

    public static int Calculator(RSPParameter playerRSP, RSPParameter enemyRSP)
    {
        var value = (playerRSP - enemyRSP + RSP_OFFSET) % RSP_REMAINDER;
        return value;
    }

    #endregion
}
