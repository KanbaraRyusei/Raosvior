using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILifeChange
{
    /// <summary>
    /// プレイヤーのライフを回復する関数
    /// </summary>
    /// <param name="heal"></param>
    void HealLife(int heal);

    /// <summary>
    /// プレイヤーのライフにダメージを与える関数
    /// シールドがあれば先に削ってからライフにダメージを与える
    /// </summary>
    /// <param name="damage"></param>
    void ReceiveDamage(int damage = 1);

    /// <summary>
    /// プレイヤーのシールドを増やす関数
    /// </summary>
    /// <param name="num"></param>
    void GetShield(int num = 1,PlayerHand playerHand = null);
}
