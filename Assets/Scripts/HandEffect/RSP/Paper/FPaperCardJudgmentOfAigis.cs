using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// アイギスの裁き・F(パー)
/// 勝利した時、自分のシールドトークンを1つに付きさらに1ダメージを与えてもよいそうした場合、自分のシールドトークンを破壊する。
/// ※自分のシールドを減らす枚数を決める処理が必要
/// </summary>
public class FPaperCardJudgmentOfAigis : RSPHandEffect
{
    async public override UniTask Effect()
    {
        ChangePlayersIndex(Player);

        //自分のシールドを破壊する枚数を決めれるようにする

        PhaseManager.OnNextPhase(true);

        await UniTask.NextFrame();
    }
}
