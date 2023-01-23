using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チェーンアックス(チョキ)
/// 勝利した時このカードを手札に戻せる。
/// ※カードを手札に戻すかどうかの選択が必要
/// </summary>
public class ScissorsCardChainAx : RSPHandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    int _playerIndex = 0;
    int _enemyIndex = 1;

    public override void Effect()
    {
        //このカードを手札に戻すのでカードを捨てるか選択できるようにする
        PhaseManager.OnNextPhase(true);
    }
}
