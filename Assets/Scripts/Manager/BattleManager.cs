using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// Battleの進行管理システムの参照を持ち、実際に関数を呼び出すクラス
/// </summary>
public class BattleManager : MonoBehaviour
{
    #region Properties

    /// <summary>
    /// 現在のターン数
    /// </summary>
    public int CurrentTurn { get; private set; }

    #endregion

    #region Inspector Variables

    [SerializeField]
    RPCManager _rpcManager = null;

    [SerializeField]
    [Header("カードマネージャー")]
    private CardManager _cardManager = null;

    [SerializeField]
    [Header("勝利したプレイヤー")]
    private PlayerInterface _winner = null;

    [SerializeField]
    [Header("敗北したプレイヤー")]
    private PlayerInterface _loser = null;

    [SerializeField]
    [Header("リーダーカード選択フェーズ強制終了時間")]
    private int _leaderSelectTime = 20000;

    [SerializeField]
    [Header("手札選択フェーズ強制終了時間")]
    private int _handSelectTime = 30000;

    [SerializeField]
    [Header("カード選択フェーズ強制終了時間")]
    private int _cardSelectTime = 20000;

    //ここからの終了時間は仮で用意(本来なら演出?の時間)
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

    #endregion

    #region Constants

    private const int MAX_HAND_COUNT = 5;

    #endregion

    #region Events

    private event Action OnStockEffect;

    #endregion

    #region Public Methods

    public async void AllPhase()
    {
        await LeaderSelect();
        await HandSelect();

        while (true)
        {
            CurrentTurn++;         
            await CardSelect();
            await Battle();
            await WinnerDamageProcess();
            await WinnerCardEffect();
            if (await LeaderEffect()) break;
            await StockEffect();
            await UseCardOnReserve();
            await Refresh();
            await InitForJudgement();
        }

        GameEnd();
    }

    #endregion

    #region Private Methods

    #region LeaderSelect Methods

    private async UniTask LeaderSelect()
    {
        var cts = new CancellationTokenSource();

        //一定時間たったらランダムでカードを付与
        SetLeaderRandom(cts.Token);

        //カードを選ぶまで待つ
        await DelaySetLeader();

        cts.Cancel();

        PhaseManager.OnNextPhase();//手札選択フェーズへ
    }

    private async void SetLeaderRandom(CancellationToken token)
    {
        //一定時間経過後
        await UniTask.Delay(_leaderSelectTime, cancellationToken: token);

        var player = PlayerManager.Players[1];
        //クライアントかどうかの判定をする
        if (PhotonNetwork.IsMasterClient) player = PlayerManager.Players[0];

        var isSelecting = player.PlayerParameter.LeaderHand;
        if (isSelecting != null) _cardManager.SetLeaderHand(player);
    }

    private async UniTask DelaySetLeader()
    {
        //プレイヤーがカードを選ぶまで待つ
        foreach (var player in PlayerManager.Players)
        {
            await UniTask.WaitUntil(() =>
                player.PlayerParameter.LeaderHand != null);
        }
    }

    #endregion

    #region HandSelect Methods

    /// <summary>
    /// 最初にプレイヤーがカードを選択するフェーズ用の関数
    /// </summary>
    private async UniTask HandSelect()
    {
        var cts = new CancellationTokenSource();

        //一定時間たったらランダムでカードを付与
        SetHandRandom(cts.Token);

        //カードを選ぶまで待つ
        await DelaySetHand();

        cts.Cancel();

        PhaseManager.OnNextPhase();//カード選択フェーズへ
    }

    /// <summary>
    /// 一定時間経過後プレイヤーにカードがなかったら付与する関数
    /// </summary>
    private async void SetHandRandom(CancellationToken token)
    {
        //一定時間経過後
        await UniTask.Delay(_handSelectTime, cancellationToken: token);

        var player = PlayerManager.Players[1];
        //クライアントかどうかの判定をする
        if (true) player = PlayerManager.Players[0];

        var handCount = player
                        .PlayerParameter
                        .PlayerHands
                        .Count;

        //じゃんけんカードが5枚になるまで繰り返す
        for (int count = handCount; count < MAX_HAND_COUNT; count++)
            _cardManager.SetRSPHand(player);
    }

    /// <summary>
    /// プレイヤーが全てのカードを選ぶまで待機する関数
    /// </summary>

    private async UniTask DelaySetHand()
    {
        //プレイヤーがカードを選ぶまで待つ
        foreach (var player in PlayerManager.Players)
        {
            await UniTask.WaitUntil(() =>
                player.PlayerParameter.PlayerHands.Count == MAX_HAND_COUNT);
        }
    }

    #endregion

    #region CardSelect Methods

    /// <summary>
    /// セットするカードを選択するフェーズ用の関数
    /// </summary>
    private async UniTask CardSelect()
    {
        var cts = new CancellationTokenSource();

        //一定時間たったらランダムでカードをセット
        SelectRSPCardRandom(cts.Token);

        //カードをセットするまで待つ
        await DelaySelectRSPCard();

        cts.Cancel();

        PhaseManager.OnNextPhase();//バトル勝敗決定処理フェーズへ
    }

    /// <summary>
    /// 一定時間経過後プレイヤーにカードがなかったら付与する関数
    /// </summary>
    private async void SelectRSPCardRandom(CancellationToken token)
    {
        //20秒後
        await UniTask.Delay(_cardSelectTime, cancellationToken: token);

        var player = PlayerManager.Players[1];
        if (PhotonNetwork.IsMasterClient) player = PlayerManager.Players[0];

        //カードがセットされていなかったら
        if (player.PlayerParameter.PlayerSetHand == null)
        {
            //ランダムでセット
            var count = player.PlayerParameter.PlayerHands.Count;
            var random = UnityEngine.Random.Range(0, count);
            player
                .HandCollection
                .SetHand(player.PlayerParameter.PlayerHands[random]);
        }
    }

    /// <summary>
    /// カードの選択フェーズの待機用
    /// </summary>
    private async UniTask DelaySelectRSPCard()
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
    private async UniTask Battle()
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

        PhaseManager.OnNextPhase();//勝者のダメージ処理フェーズへ
    }

    #endregion

    #region WinnerDamageProcess Method

    /// <summary>
    /// ダメージ処理
    /// </summary>
    private async UniTask WinnerDamageProcess()
    {
        var handEffectType =
            _winner.PlayerParameter.PlayerSetHand.HandEffect.GetType();

        if (handEffectType != typeof(FScissorsCardJammingWave))
        {
            _loser.ChangeableLife.ReceiveDamage();
            await UniTask.Delay(_winnerDamegeProcessTime);
        }
        
        PhaseManager.OnNextPhase();//勝者のカード効果処理フェーズへ
    }

    #endregion

    #region WinnerCardEffect Method

    private async UniTask WinnerCardEffect()
    {
        var handEffect = _winner.PlayerParameter.PlayerSetHand.HandEffect;
        var handType = _winner.PlayerParameter.PlayerSetHand.HandEffect.GetType();

        if (handType != typeof(ScissorsCardChainAx)) handEffect.Effect();
        else OnStockEffect += handEffect.Effect;

        await UniTask.Delay(_winnerCardEffectTime);
        await UniTask.WaitUntil(() =>
            PhaseManager.CurrentPhaseProperty != PhaseParameter.Intervention);

        PhaseManager.OnNextPhase();//リーダーの効果処理フェーズへ
    }

    #endregion

    #region LeaderEffect Method

    /// <summary>
    /// リーダー効果発動用の関数
    /// </summary>
    private async UniTask<bool> LeaderEffect()
    {
        foreach (var player in PlayerManager.Players)
        {
            if (player.GetType() != typeof(ShamanData))
                _winner.PlayerParameter.LeaderHand.HandEffect.CardEffect();
        }

        await UniTask.Delay(_leaderEffectTime);
        await UniTask.WaitUntil(() =>
            PhaseManager.CurrentPhaseProperty != PhaseParameter.Intervention);

        //どっちかのプレイヤーが0になったら
        var client = PlayerManager.Players[0].PlayerParameter;
        var other = PlayerManager.Players[1].PlayerParameter;
        if (client.Life <= 0 || other.Life <= 0) return true;

        PhaseManager.OnNextPhase();//効果ストック処理フェーズへ

        return false;
    }

    #endregion

    #region StockEffect Method

    /// <summary>
    /// ストックしたカードの効果を発動する関数
    /// </summary>
    public async UniTask StockEffect()
    {      
        if (OnStockEffect != null)
        {
            OnStockEffect?.Invoke();
            await UniTask.Delay(_stockEffectTime);
            await UniTask.WaitUntil(() => PhaseManager.CurrentPhaseProperty != PhaseParameter.Intervention);
            var handEffect = _winner.PlayerParameter.PlayerSetHand.HandEffect;
            OnStockEffect -= handEffect.Effect;
        }

        PhaseManager.OnNextPhase();//リザーブ処理フェーズへ
    }

    #endregion

    #region UseCardOnReserve Method

    /// <summary>
    /// 使用したカードをリザーブに送る関数
    /// </summary>
    private async UniTask UseCardOnReserve()
    {
        foreach (var player in PlayerManager.Players)
            player.HandCollection.SetCardOnReserve();

        await UniTask.Delay(_useCardOnReserveTime);

        PhaseManager.OnNextPhase();//リフレッシュ処理フェーズへ
    }

    #endregion

    #region Refresh Method

    /// <summary>
    /// カードがなかったらリフレッシュする
    /// </summary>
    /// <returns></returns>
    private async UniTask Refresh()
    {
        foreach (var player in PlayerManager.Players)
        {
            var count = player.PlayerParameter.PlayerHands.Count;
            if (count == 0) player.HandCollection.ResetHand();
        }

        await UniTask.Delay(_refreshTime);

        PhaseManager.OnNextPhase();//決着処理フェーズへ
    }

    #endregion

    #region Judgement Method

    /// <summary>
    /// 初期化する関数
    /// </summary>
    private async UniTask InitForJudgement()
    {
        _winner = null;
        _loser = null;

        await UniTask.Delay(_judgementTime);

        PhaseManager.OnNextPhase();//カード選択フェーズへ
    }

    #endregion

    #region GameEnd Method

    /// <summary>
    /// 勝者を決める関数
    /// </summary>
    private void GameEnd()
    {
        //どっちかのプレイヤーが0になったら
        var client = PlayerManager.Players[0].PlayerParameter;
        var other = PlayerManager.Players[1].PlayerParameter;
        if (client.Life > 0)
        {
            _winner = PlayerManager.Players[0];
            _loser = PlayerManager.Players[1];
            Debug.Log("クライアントの勝利");
        }
        else if (other.Life > 0)
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

    #endregion
}
