using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// チェーンアックス(チョキ)
/// 勝利した時このカードを手札に戻せる。
/// ※カードを手札に戻すかどうかの選択が必要
/// </summary>
public class ScissorsCardChainAx : RSPHandEffect
{
    public override void Effect()
    {
        ChangePlayersIndex(Player);
        //このカードを手札に戻すのでカードを捨てるか選択できるようにする
        PhaseManager.OnNextPhase(this);
    }
}
