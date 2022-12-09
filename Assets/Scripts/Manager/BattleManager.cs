using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Battleの進行管理システムの参照を持ち、実際に関数を呼び出すクラス
/// シャーマン...おめぇどこにでもいるな...
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

    //リザーブ処理フェーズ...カードをリザーブに送る
    //リフレッシュ処理フェーズ...ストックした処理を行う
    //決着処理フェーズ...プレイヤーのライフが残っていたら、ゲームを継続する。じゃんけんカードがなかったらリザーブを手持ちに戻す
    ///ターン終了(カード選択フェーズに戻る)

    //介入処理フェーズ...介入処理がある場合に

    [SerializeField]
    [Header("手札選択フェーズ強制終了時間")]
    int _handSelectTime = 30000;

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
    [Header("リーダーの効果フェーズ終了時間")]
    float _leaderEffectTime = 5f;

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

    const int MAX_HAND_COUNT = 5;

    private event Action EffectStock;

    private void Awake()
    {
        HandSelect();
    }

    /// <summary>
    /// カードを選択
    /// </summary>

    async private void HandSelect()
    {
        await DelaySetHand(_handSelectTime);
        PhaseManager.OnNextPhase();//カード選択フェーズへ
        CardSelect();
    }

    /// <summary>
    /// 出すカードを決める
    /// </summary>
    async private void CardSelect()
    {
        await DelaySetHand(_cardSelectTime);
        PhaseManager.OnNextPhase();//バトル勝敗決定処理フェーズへ
        Battle();
    }

    /// <summary>
    /// バトルの勝敗を決定する
    /// </summary>
    private void Battle()
    {
        var player1RSP = PlayerManager.Players[0].PlayerSetHand.Hand;
        var player2RSP = PlayerManager.Players[1].PlayerSetHand.Hand;
        var r = RSPParameter.Rock;
        var s = RSPParameter.Scissors;
        var p = RSPParameter.Paper;
        var player1Win = player1RSP == r && player2RSP == s
                      || player1RSP == s && player2RSP == p
                      || player1RSP == p && player2RSP == r;
        var drow = player1RSP == player2RSP;

        if(player1Win)//プレイヤー0の勝利なら
        {
            Debug.Log("プレイヤー0の勝利");
        }
        else if(drow)//引き分けなら
        {
            Debug.Log("引き分け");
        }
        else//プレイヤー1の勝利なら
        {
            Debug.Log("プレイヤー0の敗北");
        }
        PhaseManager.OnNextPhase();//勝者のカード効果フェーズへ
    }

    /// <summary>
    /// リーダー効果発動用
    /// </summary>
    private void WinForLeader()
    {
        var loserLeader = PlayerManager.Players[1].LeaderHand.Leader;
        var shaman = LeaderParameter.Shaman;
        if (loserLeader == shaman)
        {
            var isIntervetion = PlayerManager.Players[1].LeaderEffect();
            if(isIntervetion)PhaseManager.OnNextPhase(true);
        }
        var leaderEffect = PlayerManager.Players[1].LeaderEffect();
        //待機する予定
        PlayerManager.Players[0].PlayerSetHand.HandEffect.Effect();
    }

    /// <summary>
    /// リーダー効果発動用
    /// </summary>
    async private void DrawForLeader()
    {
        var player0Leader = PlayerManager.Players[0].LeaderHand.Leader;
        var player1Leader = PlayerManager.Players[1].LeaderHand.Leader;
        var archer = LeaderParameter.Archer;
        var shaman = LeaderParameter.Shaman;
        if (player0Leader == archer && player1Leader == shaman)
        {
            var isIntervetion = PlayerManager.Players[1].LeaderEffect();
            PhaseManager.OnNextPhase(isIntervetion);
            await DelayShaman(_interventionTime);
        }
        else if (player1Leader == archer && player0Leader == shaman)
        {
            var isIntervetion = PlayerManager.Players[0].LeaderEffect();
            PhaseManager.OnNextPhase(isIntervetion);
            await DelayShaman(_interventionTime);
        }
        else
        {
            PlayerManager.Players[0].LeaderEffect();
            PlayerManager.Players[1].LeaderEffect();
        }
    }

    /// <summary>
    /// リーダー効果発動用
    /// </summary>
    private void LoseForLeader()
    {
        var player0Leader = PlayerManager.Players[0].LeaderHand.Leader;
        var player1Leader = PlayerManager.Players[1].LeaderHand.Leader;
        var archer = LeaderParameter.Archer;
        var shaman = LeaderParameter.Shaman;
        if (player0Leader == shaman)
        {

        }
        if (player1Leader == archer)
        {

        }
        else
        {
            PlayerManager.Players[1].PlayerSetHand.HandEffect.Effect();
        }
    }


    /// <summary>
    /// 最初の手札選択フェーズの待機用
    /// </summary>

    async private UniTask DelaySetHand(float delayTime)
    {
        foreach (var player in PlayerManager.Players)
        {
            await UniTask.WaitUntil(() => player.LeaderHand != null);
            await UniTask.WaitUntil(() => player.PlayerHands.Count == MAX_HAND_COUNT);
        }
        foreach (var player in PlayerManager.Players)
        {
            if (player.LeaderHand != null)
            {
                var random = UnityEngine.Random.Range(0, 4);
                player.AddLeaderHand(default);
            }
        }
    }

    async private void SetHand()
    {
        await UniTask.Delay(_handSelectTime);
        foreach (var player in PlayerManager.Players)
        {
            if (player.LeaderHand == null)
            {
                var random = UnityEngine.Random.Range(0, 4);
                player.AddLeaderHand(default);
            }
            if(player.PlayerHands.Count < MAX_HAND_COUNT)
            {

                player.AddHand(default);
            }
        }
    }

    /// <summary>
    /// カードの選択フェーズの待機用
    /// </summary>
    async private UniTask DelayPlayerSetCard(float delayTime)
    {
        var isSetting = false;
        for (float i = 0f; i < delayTime; i += Time.deltaTime)
        {
            foreach (var player in PlayerManager.Players)
            {
                if (player.PlayerSetHand != null)
                {
                    if (isSetting) return;
                    isSetting = true;
                    continue;
                }
                break;
            }
            await UniTask.NextFrame();
        }
        //20秒経過時点で「技カード配置スペース」にカードがセットされていない場合
        //手札のカードをランダムに1枚選びセットする
        foreach (var player in PlayerManager.Players)
        {
            if (player.PlayerSetHand != null)
            {
                var random = UnityEngine.Random.Range(0, player.PlayerHands.Count);
                player.SetHand(player.PlayerHands[random]);
            }
        }
    }

    /// <summary>
    /// 介入処理フェーズの待機用
    /// </summary>
    async private UniTask DelayShaman(float delayTime)
    {
        for (float i = 0f; i < delayTime; i += Time.deltaTime)
        {
            await UniTask.NextFrame();
        }
    }
}
