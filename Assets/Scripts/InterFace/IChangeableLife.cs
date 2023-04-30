using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChangeableLife
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
}
