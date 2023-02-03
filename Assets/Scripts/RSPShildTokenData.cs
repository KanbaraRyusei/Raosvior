using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RSPShildTokenData
{
    public int Number { get; private set; }
    public PlayerHand RSPHand { get; private set; }

    public void SetRSPShildTokenData(int number, PlayerHand rspHand)
    {
        Number = number;
        RSPHand = rspHand;
    }
}
