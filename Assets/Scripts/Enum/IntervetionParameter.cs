using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 介入用のパラメータ
/// </summary>
public enum IntervetionParameter
{
    [Tooltip("シャーマンのリーダーカード")]
    LeaderCardShaman,

    [Tooltip("アイギスの裁き・Fのじゃんけんカード")]
    FPaperCardJudgmentOfAigis,

    [Tooltip("ドレインシールドのじゃんけんカード")]
    PaperCardDrainShield,

    [Tooltip("チェーンアックスのじゃんけんカード")]
    ScissorsCardChainAx,
}
