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
/// </summary>
public class ShamanData : LeaderHandEffect
{
    #region Public Property

    public bool IsDecide { get; private set; }
    public bool IsReducing { get; private set; }

    #endregion

    #region Private Member

    private PlayerHand _playerHand;

    #endregion

    #region public method

    public override void CardEffect()
    {
        ChangePlayersIndex(Player);
        var enemyLeader = Players[EnemyIndex].PlayerParameter.LeaderHand.HandEffect.GetType();
        var playerRSP = Players[PlayerIndex].PlayerParameter .PlayerSetHand.Hand;
        var enemyRSP = Players[EnemyIndex].PlayerParameter.PlayerSetHand.Hand;

        var value = RSPManager.Calculator(playerRSP, enemyRSP);
        //相手がアーチャーの引き分けor負けだったら
        var draw = value == RSPManager.DRAW && enemyLeader == typeof(ArcherData);
        if (draw || value == RSPManager.LOSE)
        {
            //チョキのカードを絞り込む
            foreach (var RSP in Players[PlayerIndex].PlayerParameter.PlayerHands)
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

        Invoke("Init", 1f);
    }

    public void DecideScissorsHand()
    {
        Players[PlayerIndex].HandCollection.OnReserveHand(_playerHand);

        IsDecide = true;
        IsReducing = true;

        Invoke("Init", 1f);
    }

    async public void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(20000, cancellationToken: token);

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
