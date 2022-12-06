using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Battleの進行管理システムの参照を持ち、実際に関数を呼び出すクラス
/// </summary>
public class BattleManager : MonoBehaviour
{
    //手札選択フェーズ(試合開始前)...キャラクターとじゃんけんのカードを選択

    ///ターン開始///  
    //カード選択フェーズ(メインフェーズ)...じゃんけんカードの選択、リザーブのチェック、リーダー効果確認

    //20秒経過したらじゃんけんカードをセット
    //待機

    //バトル勝敗決定処理フェーズ(バトルフェーズ)...勝敗を決める
    //勝者のダメージ処理フェーズ...ダメージ処理
    //勝者のカード効果処理フェーズ...じゃんけんカードの処理(ダメージでも回復でもなければストック)
    //キャラクターのカード効果処理フェーズ...キャラクターのカードの処理(ダメージでも回復でもなければストック)

    //HPが0になっていたらゲームエンド

    //リザーブ処理フェーズ...ストックした処理を行う
    //リフレッシュ処理フェーズ...カードを捨てる
    //決着処理フェーズ...ターン終了処理
    ///ターン終了(カード選択フェーズに戻る)///

    //介入処理フェーズ...介入処理がある場合に

    [SerializeField]
    [Header("手札選択フェーズ強制終了時間")]
    float _handSelectTime = 30f;

    [SerializeField]
    [Header("カード選択フェーズ強制終了時間")]
    float _cardSelectTime = 20f;

    [SerializeField]
    [Header("バトル勝敗決定処理フェーズ終了時間")]
    float _battleTime = 5f;

    [SerializeField]
    [Header("勝者のダメージ処理フェーズ終了時間")]
    float _winnerDamegeProcessTime = 5f;

    [SerializeField]
    [Header("勝者のカード効果フェーズ終了時間")]
    float _winnerCardEffectTime = 5f;

    [SerializeField]
    [Header("キャラクターの効果フェーズ終了時間")]
    float _characterEffectTime = 5f;

    [SerializeField]
    [Header("リザーブ処理フェーズ終了時間")]
    float _useCardOnReserveTime = 5f;

    [SerializeField]
    [Header("リフレッシュ処理フェーズ終了時間")]
    float _refreshTime = 5f;

    [SerializeField]
    [Header("決着処理フェーズ終了時間")]
    float _judgementTime = 5f;

    [SerializeField]
    [Header("介入処理フェーズ終了時間")]
    float _interventionTime = 20f;

    private void Awake()
    {
        HandSelect();
    }

    /// <summary>
    /// カードを選択
    /// </summary>

    async private void HandSelect()
    {
        await Delay(_handSelectTime);
    }

    /// <summary>
    /// 出すカードを決める
    /// </summary>
    private void CardSelect()
    {

    }

    private void Battle()
    {

    }

    async private UniTask Delay(float delayTime)
    {
        for (float i = 0f; i < delayTime; i += Time.deltaTime)
        {
            foreach (var player in PlayerManager.Players)
            {
                if(player.CharacterHand != null)
                {

                }
            }
            await UniTask.NextFrame();
        }
    }
}
