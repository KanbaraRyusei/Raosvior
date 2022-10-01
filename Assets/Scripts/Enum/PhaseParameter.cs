using UnityEngine;

/// <summary>
/// フェーズのパラメータ
/// 順番になっているのでインクリメントすればフェーズが進む
/// 介入処理だけ特殊なので最後にしている
/// </summary>
public enum PhaseParameter
{
    [Tooltip("手札選択フェーズ")]
    HandSelect,

    [Tooltip("カードの選択フェーズ")]
    CardSelect,

    [Tooltip("バトル勝敗決定処理フェーズ")]
    Battle,

    [Tooltip("勝者のダメージ処理フェーズ")]
    WinnerDamageProcess,

    [Tooltip("勝者のカード効果処理フェーズ")]
    WinnerCardEffect,

    [Tooltip("キャラクターの効果処理フェーズ")]
    CharacterEffect,

    [Tooltip("リザーブ処理フェーズ")]
    UseCardOnReserve,

    [Tooltip("リフレッシュ処理フェーズ")]
    Refresh,

    [Tooltip("決着処理フェーズ")]
    Judgement,

    [Tooltip("介入処理フェーズ")]
    Intervention
}
