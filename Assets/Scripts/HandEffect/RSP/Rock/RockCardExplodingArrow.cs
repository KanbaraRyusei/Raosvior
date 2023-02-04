using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 炸裂する矢(グー)
/// 勝利した時相手がシールドトークンを所持しているならさらに1ダメージを与える。
/// </summary>
public class RockCardExplodingArrow : RSPHandEffect
{
    #region Public Method

    public override void Effect()
    {
        ChangePlayersIndex(Player); 

        //相手がシールドトークンを所持しているなら
        if (Players[EnemyIndex].PlayerParameter.Shield > 0)
        {
            //さらに1ダメージを与える
            Players[EnemyIndex].LifeChange.ReceiveDamage();
        }
    }

    #endregion
}
