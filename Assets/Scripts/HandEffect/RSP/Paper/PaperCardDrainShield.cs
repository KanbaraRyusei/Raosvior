using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ドレインシールド(パー)
/// 勝利した時、相手の手札を1枚選んで裏向きのまま自分のシールドトークンにする。
/// (このシールドトークンは破壊されたとき持ち物のレシーブに置く)
/// ※相手のカードを選択する必要がある。そのカードが破壊されたときの処理も必要
/// </summary>
public class PaperCardDrainShield : RSPHandEffect
{
    public bool IsDecide { get; private set; }

    public int EnemyHandCount => Players[EnemyIndex].PlayerParameter.PlayerHands.Count;

    public override void Effect()
    {
        ChangePlayersIndex(Player);

        //相手のカードを自分のシールドトークンにしたいので
        //ここで相手の手札を選ぶ
        PhaseManager.OnNextPhase(this);
    }

    public void SelectEnemyHand()
    {
        
    }
}
