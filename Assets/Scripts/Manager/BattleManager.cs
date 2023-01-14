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
    int _cardSelectTime = 20000;

    [SerializeField]
    [Header("バトル勝敗決定処理フェーズ終了時間")]
    int _battleTime = 5000;

    [SerializeField]
    [Header("勝者のダメージ処理フェーズ終了時間")]
    int _winnerDamegeProcessTime = 5000;

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

    [SerializeField]
    [Header("カードマネージャー")]
    CardManager _cardManager;

    [SerializeField]
    [Header("勝利したプレイヤー")]
    PlayerData _winnerPlayer;

    [SerializeField]
    [Header("敗北したプレイヤー")]
    PlayerData _loserPlayer;

    const int MAX_HAND_COUNT = 5;
    const int DEFAULT_DAMEGE = 1;

    private event Action EffectStock;

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
            await Battle();
            PhaseManager.OnNextPhase();//勝者のカード効果フェーズへ
            await WinnerDamageProcess();
        }
    }

    /// <summary>
    /// 最初にプレイヤーがカードを選択するフェーズ用の関数
    /// </summary>
    async private UniTask HandSelect()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //一定時間たったらランダムでカードを付与
        RandomSetHand(token);

        //カードを選ぶまで待つ
        await DelaySetHand();

        cts.Cancel();
    }

    /// <summary>
    /// セットするカードを選択するフェーズ用の関数
    /// </summary>
    async private UniTask CardSelect()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        //一定時間たったらランダムでカードをセット
        RandomSelectRSPCard(token);

        //カードをセットするまで待つ
        await DelaySelectRSPCard();

        cts.Cancel();
    }

    /// <summary>
    /// バトルの勝敗を決定する
    /// </summary>
    async private UniTask Battle()
    {
        var playerRSP = PlayerManager.Players[0].PlayerSetHand.Hand;
        var anotherPlayerRSP = PlayerManager.Players[1].PlayerSetHand.Hand;
        var r = RSPParameter.Rock;
        var s = RSPParameter.Scissors;
        var p = RSPParameter.Paper;
        var playerWin = playerRSP == r && anotherPlayerRSP == s ||
                         playerRSP == s && anotherPlayerRSP == p ||
                         playerRSP == p && anotherPlayerRSP == r;
        var drow = playerRSP == anotherPlayerRSP;

        if(playerWin)//プレイヤーの勝利なら
        {
            _winnerPlayer = PlayerManager.Players[0];
            _loserPlayer = PlayerManager.Players[1];
            Debug.Log("プレイヤーの勝利");
        }
        else if(drow)//引き分けなら
        {
            _winnerPlayer = null;
            _loserPlayer = null;
            Debug.Log("引き分け");
        }
        else//プレイヤーの敗北なら
        {
            _winnerPlayer = PlayerManager.Players[1];
            _loserPlayer = PlayerManager.Players[0];
            Debug.Log("もう一人のプレイヤーの勝利");
        }
        await UniTask.Delay(_battleTime);
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    async private UniTask WinnerDamageProcess()
    {
        if (_loserPlayer.LeaderHand.Leader == LeaderParameter.Shaman)
        {
            //チョキのカードを絞り込む
            foreach (var RSP in _loserPlayer.PlayerHands)
            {
                //チョキのカードがあったら
                if (RSP.Hand == RSPParameter.Scissors)
                {
                    PhaseManager.OnNextPhase(true);
                }
            }
        }

        _loserPlayer.ReceiveDamage(DEFAULT_DAMEGE);

        await UniTask.Delay(_winnerDamegeProcessTime);
    }

    /// <summary>
    /// リーダー効果発動用(勝ちの場合)
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
    /// リーダー効果発動用(引き分けの場合)
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
    /// リーダー効果発動用(負けの場合)
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
