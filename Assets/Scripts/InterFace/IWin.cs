using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// じゃんけん勝利時に効果を発動する機能の実装を強制するインターフェース
/// 使い方
/// 1　インターフェースを継承させる
/// 2　Win関数を実装する
/// 3　機能を関数内に書く
/// </summary>
public interface IWin
{
    /// <summary>
    /// じゃんけん勝利時に発動する関数
    /// BattleManagerが呼び出す
    /// </summary>
    void Win();
}
