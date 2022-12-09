using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// リーダーカードのパラメータ
/// </summary>
public enum LeaderParameter
{
    [Tooltip("シャーマン")]
    Shaman = 1,

    [Tooltip("グラップラー")]
    Grappler,

    [Tooltip("ナイト")]
    Knight,

    [Tooltip("アーチャー")]
    Archer
}
