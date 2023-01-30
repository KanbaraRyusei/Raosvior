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
    //効果ストック処理フェーズ...ストックした処理を行う

    //HPが0になっていたらゲームエンド
    //リザーブ処理フェーズ...使ったじゃんけんカードをリザーブに送る
    //リフレッシュ処理フェーズ...じゃんけんカードがなかったらリザーブを手持ちに戻す
    //決着処理フェーズ...プレイヤーのライフが残っていたら、ゲームを継続する。

    ///ターン終了(カード選択フェーズに戻る)

    //介入処理フェーズ...介入処理がある場合に

    #endregion

    #region Public Property

    /// <summary>
    /// 現在のターン
    /// </summary>
    public int CurrentTurn { get; private set; }

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("カードマネージャー")]
    private CardManager _cardManager;

    [SerializeField]
    [Header("勝利したプレイヤー")]
    private PlayerInterface _winner;

    [SerializeField]
    [Header("敗北したプレイヤー")]
    private PlayerInterface _loser;

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

    [Header("効果ストック処理フェーズ終了時間")]
    private int _stockEffectTime = 5000;

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

    #endregion

    #region Constans

    private const int MAX_HAND_COUNT = 5;
    private const int DEFAULT_DAMEGE = 1;

    #endregion

    #region Events

    private event Action OnStockEffect;

    #endregion

    private void Awake()
    {
        AllPhase();
    }

    async private void AllPhase()
    {
        await HandSelect();

        while (true)
        {
            CurrentTurn++;
            PhaseManager.OnNextPhase();//カード選択フェーズへ
            await CardSelect();
            PhaseManager.OnNextPhase();//バトル勝敗決定処理フェーズへ
            await Battle();
            PhaseManager.OnNextPhase();//勝者のダメージ処理フェーズへ
            await WinnerDamageProcess();
            PhaseManager.OnNextPhase();//勝者のカード効果処理フェーズへ
            await WinnerCardEffect();
            PhaseManager.OnNextPhase();//リーダーの効果処理フェーズへ
            if (await LeaderEffect()) break;
            PhaseManager.OnNextPhase();//効果ストック処理フェーズへ
            await StockEffect();
            PhaseManager.OnNextPhase();//リザーブ処理フェーズへ
            await UseCardOnReserve();
            PhaseManager.OnNextPhase();//リフレッシュ処理フェーズへ
            await Refresh();
            PhaseManager.OnNextPhase();//決着処理フェーズへ
            await InitForJudgement();
        }

        GameEnd();
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
            if (player.PlayerParameter.LeaderHand != null)
            {
                _cardManager.SetLeaderHand(player);
            }

            //じゃんけんカードが5枚未満だったら
            var handCount =
                player.PlayerParameter.PlayerHands.Count;

            for (int count = handCount; count < MAX_HAND_COUNT; count++)
            {
                _cardManager.SetRSPHand(player);
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
            await UniTask.WaitUntil(() =>
                player.PlayerParameter.LeaderHand != null);
            await UniTask.WaitUntil(() =>
                player.PlayerParameter.PlayerHands.Count == MAX_HAND_COUNT);
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
            if (player.PlayerParameter.PlayerSetHand == null)
            {
                //ランダムでセット
                var count = player.PlayerParameter.PlayerHands.Count;
                var random = UnityEngine.Random.Range(0, count);
                player
                    .HandCollection
                    .SetHand (player.PlayerParameter.PlayerHands[random]);
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
            await UniTask.WaitUntil(() =>
                player.PlayerParameter.PlayerSetHand != null);
        }
    }

    #endregion

    #region Battle Method

    /// <summary>
    /// バトルの勝敗を決定する
    /// </summary>
    async private UniTask Battle()
    {
        var clientRSP = PlayerManager.Players[0].PlayerParameter.PlayerSetHand.Hand;
        var otherRSP = PlayerManager.Players[1].PlayerParameter.PlayerSetHand.Hand;
        var judg = RSPManager.Calculator(clientRSP, otherRSP);

        if (judg == RSPManager.WIN)//クライアントの勝利なら
        {
            _winner = PlayerManager.Players[0];
            _loser = PlayerManager.Players[1];
            Debug.Log("クライアントの勝利");
        }
        else if (judg == RSPManager.DRAW)//引き分けなら
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
        _loser.LifeChange.ReceiveDamage(DEFAULT_DAMEGE);
        await UniTask.Delay(_winnerDamegeProcessTime);
    }

    #endregion

    #region WinnerCardEffect Method

    async private UniTask WinnerCardEffect()
    {
        await _winner.PlayerParameter.PlayerSetHand.HandEffect.Effect();
        await UniTask.Delay(_winnerCardEffectTime);
        await UniTask.WaitUntil(() =>
            PhaseManager.CurrentPhaseProperty != PhaseParameter.Intervention);
    }

    #endregion

    #region LeaderEffect Method

    /// <summary>
    /// リーダー効果発動用
    /// </summary>
    async private UniTask<bool> LeaderEffect()
    {
        var winnerLeaderType = _winner.PlayerParameter.LeaderHand.HandEffect.GetType();
        var loserLeaderType = _loser.PlayerParameter.LeaderHand.HandEffect.GetType();
        if (winnerLeaderType != typeof(ShamanData)) _winner.PlayerParameter.LeaderHand.HandEffect.CardEffect();
        if (loserLeaderType != typeof(ShamanData)) _loser.PlayerParameter.LeaderHand.HandEffect.CardEffect();

        await UniTask.Delay(_leaderEffectTime);
        await UniTask.WaitUntil(() =>
            PhaseManager.CurrentPhaseProperty != PhaseParameter.Intervention);

        //どっちかのプレイヤーが0になったら
        var client = PlayerManager.Players[0].PlayerParameter;
        var other = PlayerManager.Players[1].PlayerParameter;
        if (client.Life <= 0 || other.Life <= 0) return true;
        return false;
    }

    #endregion

    #region StockEffect Method

    async public UniTask StockEffect()
    {
        OnStockEffect?.Invoke();
        await UniTask.Delay(_stockEffectTime);
        await UniTask.WaitUntil(() => PhaseManager.CurrentPhaseProperty != PhaseParameter.Intervention);
    }

    #endregion

    #region UseCardOnReserve

    async private UniTask UseCardOnReserve()
    {
        foreach (var player in PlayerManager.Players)
            player.HandCollection.SetCardOnReserve();
        await UniTask.Delay(_useCardOnReserveTime);
    }

    #endregion

    #region Refresh

    async private UniTask Refresh()
    {
        foreach (var player in PlayerManager.Players)
        {
            var count = player.PlayerParameter.PlayerHands.Count;
            if (count == 0) player.HandCollection.ResetHand();
        }

        await UniTask.Delay(_refreshTime);
    }

    #endregion

    #region Judgement

    async private UniTask InitForJudgement()
    {
        _winner = null;
        _loser = null;
        await UniTask.Delay(_judgementTime);
    }

    #endregion

    #region GameEnd Method

    private void GameEnd()
    {
        //どっちかのプレイヤーが0になったら
        var client = PlayerManager.Players[0].PlayerParameter;
        var other = PlayerManager.Players[1].PlayerParameter;
        if (client.Life > 0 && other.Life <= 0)
        {
            _winner = PlayerManager.Players[0];
            _loser = PlayerManager.Players[1];
            Debug.Log("クライアントの勝利");
        }
        else if (client.Life <= 0 && other.Life > 0)
        {
            _winner = PlayerManager.Players[1];
            _loser = PlayerManager.Players[0];
            Debug.Log("クライアントの敗北");
        }
        else
        {
            _winner = null;
            _loser = null;
            Debug.Log("引き分け");
        }
    }

    #endregion
}
