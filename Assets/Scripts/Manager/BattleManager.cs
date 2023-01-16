using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Battleの進行管理システムの参照を持ち、実際に関数を呼び出すクラス
/// シャーマン...おめぇどこにでもいるな...
/// </summary>
public class BattleManager : MonoBehaviour
{
    #region Memo

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

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("手札選択フェーズ強制終了時間")]
    private int _handSelectTime = 30000;

    [SerializeField]
    [Header("カード選択フェーズ強制終了時間")]
    private int _cardSelectTime = 20000;

    [SerializeField]
    [Header("バトル勝敗決定処理フェーズ終了時間")]
    private int _battleTime = 5000;

    [SerializeField]
    [Header("勝者のダメージ処理フェーズ終了時間")]
    private int _winnerDamegeProcessTime = 5000;

    [SerializeField]
    [Header("勝者のカード効果フェーズ終了時間")]
    private int _winnerCardEffectTime = 5000;

    [SerializeField]
    [Header("リーダーの効果フェーズ終了時間")]
    private int _leaderEffectTime = 5000;

    [SerializeField]
    [Header("リザーブ処理フェーズ終了時間")]
    private int _useCardOnReserveTime = 5000;

    [SerializeField]
    [Header("リフレッシュ処理フェーズ終了時間")]
    private int _refreshTime = 5000;

    [SerializeField]
    [Header("決着処理フェーズ終了時間")]
    private int _judgementTime = 5000;

    [SerializeField]
    [Header("介入処理フェーズ終了時間")]
    private int _interventionTime = 2000;

    [SerializeField]
    [Header("カードマネージャー")]
    private CardManager _cardManager;

    [SerializeField]
    [Header("勝利したプレイヤー")]
    private PlayerData _winner;

    [SerializeField]
    [Header("敗北したプレイヤー")]
    private PlayerData _loser;

    #endregion

    #region Constans

    private const int MAX_HAND_COUNT = 5;
    private const int DEFAULT_DAMEGE = 1;
    private const int RSP_OFFSET = 3;
    private const int RSP_REMAINDER = 3;
    private const int WIN_NUMBER = 2;
    private const int DRAW_NUMBER = 0;

    #endregion

    #region Events

    private event Action EffectStock;

    #endregion

    private void Awake()
    {
        AllPhase();
    }

    async private void AllPhase()
    {
        await HandSelect();
        PhaseManager.OnNextPhase();//カード選択フェーズへ

        while (true)
        {
            await CardSelect();
            PhaseManager.OnNextPhase();//バトル勝敗決定処理フェーズへ
            await JudgmentForBattle();
            PhaseManager.OnNextPhase();//勝者のダメージ処理フェーズへ
            await WinnerDamageProcess();
            PhaseManager.OnNextPhase();//勝者のカード効果処理フェーズへ
            await WinnerCardEffect();
            PhaseManager.OnNextPhase();//リーダーの効果処理フェーズへ
            await LeaderEffect();
        }
    }

    #region HandSelect Methods

    /// <summary>
    /// 最初にプレイヤーがカードを選択するフェーズ用の関数
    /// </summary>
    async private UniTask HandSelect()
    {
        var cts = new CancellationTokenSource();

        //一定時間たったらランダムでカードを付与
        RandomSetHand(cts.Token);

        //カードを選ぶまで待つ
        await DelaySetHand();

        cts.Cancel();
    }

    /// <summary>
    /// 一定時間経過後プレイヤーにカードがなかったら付与する関数
    /// </summary>
    async private void RandomSetHand(CancellationToken token)
    {
            //一定時間経過後
            await UniTask.Delay(_handSelectTime, cancellationToken: token);

        foreach (var player in PlayerManager.Players)
        {
            //リーダーカードが無かったら
            if (player.LeaderHand != null)
            {
                _cardManager.SetLeaderHand(player);
            }

            //じゃんけんカードが5枚未満だったら
            var handCount = player.PlayerHands.Count;

            if (handCount < MAX_HAND_COUNT)
            {
                for (int i = handCount; i < MAX_HAND_COUNT; i++)
                {
                    _cardManager.SetRSPHand(player);
                }
            }
        }
    }

    /// <summary>
    /// プレイヤーが全てのカードを選ぶまで待機する関数
    /// </summary>

    async private UniTask DelaySetHand()
    {
        //プレイヤーがカードを選ぶまで待つ
        foreach (var player in PlayerManager.Players)
        {
            await UniTask.WaitUntil(() => player.LeaderHand != null);
            await UniTask.WaitUntil(() => player.PlayerHands.Count == MAX_HAND_COUNT);
        }
    }

    #endregion

    #region CardSelect Methods

    /// <summary>
    /// セットするカードを選択するフェーズ用の関数
    /// </summary>
    async private UniTask CardSelect()
    {
        var cts = new CancellationTokenSource();

        //一定時間たったらランダムでカードをセット
        RandomSelectRSPCard(cts.Token);

        //カードをセットするまで待つ
        await DelaySelectRSPCard();

        cts.Cancel();
    }

    /// <summary>
    /// 一定時間経過後プレイヤーにカードがなかったら付与する関数
    /// </summary>
    async private void RandomSelectRSPCard(CancellationToken token)
    {
        //20秒後
        await UniTask.Delay(_cardSelectTime, cancellationToken: token);
        foreach (var player in PlayerManager.Players)
        {
            //カードがセットされていなかったら
            if (player.PlayerSetHand == null)
            {
                var count = player.PlayerHands.Count;
                var random = UnityEngine.Random.Range(0, count);
                player.SetHand(player.PlayerHands[random]);
            }
        }
    }

    /// <summary>
    /// カードの選択フェーズの待機用
    /// </summary>
    async private UniTask DelaySelectRSPCard()
    {
        //20秒経過時点で「技カード配置スペース」にカードがセットされていない場合
        //手札のカードをランダムに1枚選びセットする
        foreach (var player in PlayerManager.Players)
        {
            await UniTask.WaitUntil(() => player.PlayerSetHand != null);
        }
    }

    #endregion

    #region Battle Method

    /// <summary>
    /// バトルの勝敗を決定する
    /// </summary>
    async private UniTask JudgmentForBattle()
    {
        var clientRSP = PlayerManager.Players[0].PlayerSetHand.Hand;
        var otherRSP = PlayerManager.Players[1].PlayerSetHand.Hand;
        var judg = (clientRSP - otherRSP + RSP_OFFSET) % RSP_REMAINDER;

        if(judg == WIN_NUMBER)//クライアントの勝利なら
        {
            _winner = PlayerManager.Players[0];
            _loser = PlayerManager.Players[1];
            Debug.Log("クライアントの勝利");
        }
        else if(judg == DRAW_NUMBER)//引き分けなら
        {
            _winner = null;
            _loser = null;
            Debug.Log("引き分け");
        }
        else//クライアントの敗北なら
        {
            _winner = PlayerManager.Players[1];
            _loser = PlayerManager.Players[0];
            Debug.Log("クライアントの敗北");
        }
        await UniTask.Delay(_battleTime);
    }

    #endregion

    #region WinnerDamageProcess Method

    /// <summary>
    /// ダメージ処理
    /// </summary>
    async private UniTask WinnerDamageProcess()
    {
        //敗北者のリーダーカードがシャーマンだったら
        if (_loser.LeaderHand.Leader == LeaderParameter.Shaman)
        {
            //チョキのカードを絞り込む
            foreach (var RSP in _loser.PlayerHands)
            {
                //チョキのカードがあったら
                if (RSP.Hand == RSPParameter.Scissors)
                {
                    PhaseManager.OnNextPhase(true);
                    //介入処理用の関数を呼び出す
                    return;
                }
            }
        }
        else _loser.ReceiveDamage(DEFAULT_DAMEGE);

        await UniTask.Delay(_winnerDamegeProcessTime);
    }

    #endregion

    #region WinnerCardEffect Method

    async private UniTask WinnerCardEffect()
    {
        _winner.PlayerSetHand.HandEffect.Effect();
        await UniTask.Delay(_winnerCardEffectTime);
    }

    #endregion

    #region LeaderEffect Method

    /// <summary>
    /// リーダー効果発動用(勝ちの場合)
    /// </summary>
    async private UniTask LeaderEffect()
    {
        var shaman = LeaderParameter.Shaman;
        if (_loser.LeaderHand.Leader == shaman)
        {
            var isIntervetion = PlayerManager.Players[1].LeaderEffect();
            if (isIntervetion) PhaseManager.OnNextPhase(true);
        }
        var leaderEffect = _winner.LeaderEffect();

        await UniTask.Delay(_leaderEffectTime);
    }

    #endregion

    ///// <summary>
    ///// リーダー効果発動用(引き分けの場合)
    ///// </summary>
    //async private void DrawForLeader()
    //{
    //    var player0Leader = PlayerManager.Players[0].LeaderHand.Leader;
    //    var player1Leader = PlayerManager.Players[1].LeaderHand.Leader;
    //    var archer = LeaderParameter.Archer;
    //    var shaman = LeaderParameter.Shaman;
    //    if (player0Leader == archer && player1Leader == shaman)
    //    {
    //        var isIntervetion = PlayerManager.Players[1].LeaderEffect();
    //        PhaseManager.OnNextPhase(isIntervetion);
    //        await DelayShaman(_interventionTime);
    //    }
    //    else if (player1Leader == archer && player0Leader == shaman)
    //    {
    //        var isIntervetion = PlayerManager.Players[0].LeaderEffect();
    //        PhaseManager.OnNextPhase(isIntervetion);
    //        await DelayShaman(_interventionTime);
    //    }
    //    else
    //    {
    //        PlayerManager.Players[0].LeaderEffect();
    //        PlayerManager.Players[1].LeaderEffect();
    //    }
    //}

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
