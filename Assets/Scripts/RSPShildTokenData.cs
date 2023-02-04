using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手札がシールドトークンになった時の番号と手札を登録できるスクリプト
/// </summary>
[Serializable]
public class RSPShildTokenData
{
    #region Public Property

    public PlayerHand RSPHand { get; private set; }
    public int Number { get; private set; }

    #endregion

    #region Public Method

    public void SetRSPShildTokenData(PlayerHand rspHand,int number)
    {
        RSPHand = rspHand;
        Number = number;
    }

    #endregion
}
