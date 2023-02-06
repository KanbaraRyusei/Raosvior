using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// シャーマン
/// ダメージを受ける時
/// チョキを1枚捨てて
/// ダメージを1減らしてもよい。
/// ※介入処理
/// </summary>
public class ShamanData : LeaderHandEffect
{
    #region Public Property

    public bool IsDecide { get; private set; }
    public bool IsReducing { get; private set; }

    #endregion

    #region Inspector Member

    [SerializeField]
    [Header("選択時間")]
    private int _selectTime = 20000;

    #endregion

    #region Private Member

    private PlayerHand _playerHand;
    private float _initTime = 1f;

    #endregion

    #region public method

    public override void CardEffect()
    {
        var enemyLeader = Enemy.PlayerParameter.LeaderHand.HandEffect.GetType();
        var playerRSP = Player.PlayerParameter .PlayerSetHand.Hand;
        var enemyRSP = Enemy.PlayerParameter.PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        //相手がアーチャーの引き分けor負けだったら
        var draw = value == RSPManager.DRAW && enemyLeader == typeof(ArcherData);
        if (draw || value == RSPManager.LOSE)
        {
            //チョキのカードを絞り込む
            foreach (var RSP in Player.PlayerParameter.PlayerHands)
            {
                //チョキのカードがあったら
                if (RSP.Hand == RSPParameter.Scissors)
                {
                    PhaseManager.OnNextPhase(this);
                    return;
                }
            }
        }
    }

    public void SelectScissorsHand(PlayerHand playerHand)
    {
        _playerHand  = playerHand;     
    }

    public void DontSelectScissorsHand()
    {
        IsDecide = true;
        IsReducing = false;

        Invoke("Init", _initTime);
    }

    public void DecideScissorsHand()
    {
        Player.HandCollection.OnReserveHand(_playerHand);

        IsDecide = true;
        IsReducing = true;

        Invoke("Init", _initTime);
    }

    async public void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(_selectTime, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhaseProperty;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase)DontSelectScissorsHand();
    }

    #endregion

    #region Private Method

    private void Init()
    {
        IsDecide = false;
        IsReducing = false;
    }

    #endregion
}
