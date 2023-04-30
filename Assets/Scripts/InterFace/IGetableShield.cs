using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetableShield
{
    /// <summary>
    /// プレイヤーのシールドを増やす関数
    /// </summary>
    /// <param name="num"></param>
    void GetShield(int num = 1, RSPHandData playerHand = null);
}
