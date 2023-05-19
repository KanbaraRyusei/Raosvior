using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// Battleの進行管理システムの参照を持ち、実際に関数を呼び出すクラス
/// </summary>
public class BattleManager : MonoBehaviour, IRegisterableGameEnd
{
    #region Properties

    /// <summary>
    /// 現在のターン数
    /// </summary>
    public int CurrentTurn { get; private set; } = 1;
    public int PlayerIndex { get; private set; } = 0;
    public int EnemyIndex { get; private set; } = 1;
    private IPlayerParameter Player => PlayerManager.Instance.Players[PlayerIndex].PlayerParameter;
    private IPlayerParameter Enemy => PlayerManager.Instance.Players[EnemyIndex].PlayerParameter;

    #endregion

    #region Inspector Variables

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

    #region Member Variables

    private int _privLife = 0;
    private int _privEnemyLife = 0;

    #endregion

    #region Constants

    private const int MAX_HAND_COUNT = 5;

    #endregion

    #region Events

    public event Action<LogData> OnGameEnd;
    private event Action OnStockEffect;

    #endregion

    #region Public Methods

    public async void AllPhase()
    {
        if (PhaseManager.CurrentPhase == PhaseParameter.LeaderSelect) await LeaderSelect();
        if (PhaseManager.CurrentPhase == PhaseParameter.HandSelect) await HandSelect();

        while (true)
        {
            if(PhaseManager.CurrentPhase == PhaseParameter.CardSelect)
                await CardSelect();
            if (PhaseManager.CurrentPhase == PhaseParameter.Battle)
                await Battle();
            if (PhaseManager.CurrentPhase == PhaseParameter.WinnerDamageProcess)
                await WinnerDamageProcess();
            if (PhaseManager.CurrentPhase == PhaseParameter.WinnerCardEffect)
                await WinnerCardEffect();
            if (PhaseManager.CurrentPhase == PhaseParameter.CharacterEffect)
                await LeaderEffect();
            if (PhaseManager.CurrentPhase == PhaseParameter.GameEnd)
                break;
            if (PhaseManager.CurrentPhase == PhaseParameter.StockEffect)
                await StockEffect();
            if (PhaseManager.CurrentPhase == PhaseParameter.UseCardOnReserve)
                await UseCardOnReserve();
            if (PhaseManager.CurrentPhase == PhaseParameter.Refresh)
                await Refresh();
            if (PhaseManager.CurrentPhase == PhaseParameter.Init)
                await Init();

            CurrentTurn++;
        }

        GameEnd();
    }

    public void SetPlayerIndex()
    {
        PlayerIndex = PhotonNetwork.IsMasterClient ? 0 : 1;
        EnemyIndex = PhotonNetwork.IsMasterClient ? 1 : 0;
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

        var player = PlayerManager.Instance.Players[PlayerIndex];

        var isSelecting = player.PlayerParameter.LeaderHand;
        if (isSelecting != null) RPCManager.Instance.SendSelectLeaderHand(PlayerIndex);
    }

    private async UniTask DelaySetLeader()
    {
        //プレイヤーがカードを選ぶまで待つ
        foreach (var player in PlayerManager.Instance.Players)
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

        var cards = Player
                        .RSPHands
                        .OrderBy(_ => _.RSPHand.Hand)
                        .Select(x => x.RSPHand.CardName)
                        .ToArray();

        LogManager.SetSelectCards(cards);

        PhaseManager.OnNextPhase();//カード選択フェーズへ
    }

    /// <summary>
    /// 一定時間経過後プレイヤーにカードがなかったら付与する関数
    /// </summary>
    private async void SetHandRandom(CancellationToken token)
    {
        //一定時間経過後
        await UniTask.Delay(_handSelectTime, cancellationToken: token);

        //クライアントかどうかの判定をする
        var player = PlayerManager.Instance.Players[PlayerIndex];

        var handCount = player 
                        .PlayerParameter
                        .RSPHands
                        .Count;

        //じゃんけんカードが5枚になるまで繰り返す
        for (int count = handCount; count < MAX_HAND_COUNT; count++)
            RPCManager.Instance.SendSelectRSPHand(PlayerIndex);
    }

    /// <summary>
    /// プレイヤーが全てのカードを選ぶまで待機する関数
    /// </summary>

    private async UniTask DelaySetHand()
    {
        //プレイヤーがカードを選ぶまで待つ
        foreach (var player in PlayerManager.Instance.Players)
        {
            await UniTask.WaitUntil(() =>
                player.PlayerParameter.RSPHands.Count == MAX_HAND_COUNT);
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

        _privLife = Player.Life;
        _privEnemyLife = Enemy.Life;

        LogManager.SetUseCards(Player.SetRSPHand.RSPHand.CardName);

        if (CurrentTurn == 1) 
            LogManager.SetFirstCard(Player.SetRSPHand.RSPHand.CardName);

        PhaseManager.OnNextPhase();//バトル勝敗決定処理フェーズへ
    }

    /// <summary>
    /// 一定時間経過後プレイヤーにカードがなかったら付与する関数
    /// </summary>
    private async void SelectRSPCardRandom(CancellationToken token)
    {
        //20秒後
        await UniTask.Delay(_cardSelectTime, cancellationToken: token);

        var player = PlayerManager.Instance.Players[PlayerIndex];

        //カードがセットされていなかったら
        if (player.PlayerParameter.SetRSPHand == null)
        {
            //ランダムでセット
            var count = player.PlayerParameter.RSPHands.Count;
            var random = UnityEngine.Random.Range(0, count);
            player
                .HandCollection
                .SetHand(player.PlayerParameter.RSPHands[random]);
        }
    }

    /// <summary>
    /// カードの選択フェーズの待機用
    /// </summary>
    private async UniTask DelaySelectRSPCard()
    {
        //20秒経過時点で「技カード配置スペース」にカードがセットされていない場合
        //手札のカードをランダムに1枚選びセットする
        foreach (var player in PlayerManager.Instance.Players)
        {
            await UniTask.WaitUntil(() =>
                player.PlayerParameter.SetRSPHand != null);
        }
    }

    #endregion

    #region Battle Method

    /// <summary>
    /// バトルの勝敗を決定する
    /// </summary>
    private async UniTask Battle()
    {
        var clientRSP = PlayerManager.Instance.Players[0].PlayerParameter.SetRSPHand.RSPHand.Hand;
        var otherRSP = PlayerManager.Instance.Players[1].PlayerParameter.SetRSPHand.RSPHand.Hand;
        var judg = RSPManager.Calculator(clientRSP, otherRSP);

        if (judg == RSPManager.WIN)//クライアントの勝利なら
        {
            _winner = PlayerManager.Instance.Players[0];
            _loser = PlayerManager.Instance.Players[1];
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
            _winner = PlayerManager.Instance.Players[1];
            _loser = PlayerManager.Instance.Players[0];
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
            _winner.PlayerParameter.SetRSPHand.HandEffect.GetType();

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
        var handEffect = _winner.PlayerParameter.SetRSPHand.HandEffect;
        var handType = _winner.PlayerParameter.SetRSPHand.HandEffect.GetType();

        if (handType != typeof(ScissorsCardChainAx))
            handEffect.Effect();
        else
            OnStockEffect += handEffect.Effect;

        await UniTask.Delay(_winnerCardEffectTime);
        await UniTask.WaitUntil(() =>
            PhaseManager.CurrentPhase != PhaseParameter.Intervention);

        PhaseManager.OnNextPhase();//リーダーの効果処理フェーズへ
    }

    #endregion

    #region LeaderEffect Method

    /// <summary>
    /// リーダー効果発動用の関数
    /// </summary>
    private async UniTask LeaderEffect()
    {
        foreach (var player in PlayerManager.Instance.Players)
            if (player.GetType() != typeof(ShamanData))
                _winner.PlayerParameter.LeaderHand.HandEffect.CardEffect();

        await UniTask.Delay(_leaderEffectTime);
        await UniTask.WaitUntil(() =>
            PhaseManager.CurrentPhase != PhaseParameter.Intervention);

        var cardName = Player.SetRSPHand.RSPHand.CardName;
        LogManager.SetCardAddDamage(new(cardName, _privLife - Player.Life));

        //どっちかのプレイヤーが0になったら
        var client = PlayerManager.Instance.Players[0].PlayerParameter;
        var other = PlayerManager.Instance.Players[1].PlayerParameter;
        if (client.Life <= 0 || other.Life <= 0)
            PhaseManager.OnGameEndPhase();
        else
            PhaseManager.OnNextPhase();//効果ストック処理フェーズへ

        return;
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
            await UniTask.WaitUntil(() => PhaseManager.CurrentPhase != PhaseParameter.Intervention);
            var handEffect = _winner.PlayerParameter.SetRSPHand.HandEffect;
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
        foreach (var player in PlayerManager.Instance.Players)
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
        foreach (var player in PlayerManager.Instance.Players)
            if (player.PlayerParameter.RSPHands.Count == 0)
                player.HandCollection.ResetHand();

        await UniTask.Delay(_refreshTime);

        PhaseManager.OnNextPhase();//決着処理フェーズへ
    }

    #endregion

    #region Init Method

    /// <summary>
    /// 初期化する関数
    /// </summary>
    private async UniTask Init()
    {
        _winner = null;
        _loser = null;
        OnStockEffect = null;

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
        var client = PlayerManager.Instance.Players[0].PlayerParameter;
        var other = PlayerManager.Instance.Players[1].PlayerParameter;
        if (client.Life > 0)
        {
            _winner = PlayerManager.Instance.Players[0];
            _loser = PlayerManager.Instance.Players[1];
            Debug.Log("クライアントの勝利");
        }
        else if (other.Life > 0)
        {
            _winner = PlayerManager.Instance.Players[1];
            _loser = PlayerManager.Instance.Players[0];
            Debug.Log("クライアントの敗北");
        }
        else
        {
            _winner = null;
            _loser = null;
            Debug.Log("引き分け");
        }

        LogManager.SetTurnCount(CurrentTurn);

        var selectCards = LogManager.SelectCards as string[];
        var useCards = LogManager.UseCards as List<string>;
        var cardAddDamage = LogManager.CardAddDamage as List<CardAddDamageLogData>;
        var firstCardName = LogManager.FirstCardName;
        var turnCount = LogManager.TurnCount;
        OnGameEnd(new(selectCards , useCards, cardAddDamage, firstCardName, turnCount));
    }

    #endregion

    #endregion
}
