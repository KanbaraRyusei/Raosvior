using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 使い方
/// 1 新しいクラスを作る
/// 2 このクラスを継承させる
/// 3 Effect関数をoverrideさせてその中に効果を書く
/// </summary>
public abstract class RSPHandEffect : HandEffect
{
    /// <summary>
    /// カードの効果を発動させる関数
    /// </summary>
    public abstract void Effect();

}
