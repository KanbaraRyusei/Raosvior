using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ドレインシールド(パー)
/// 勝利した時、相手の手札を1枚選んで裏向きのまま自分のシールドトークンにする。
/// (このシールドトークンは破壊されたとき持ち物のレシーブに置く)
/// ※相手のカードを選択する必要がある。そのカードが破壊されたときの処理も必要
/// </summary>
public class PaperCardDrainShield : RSPHandEffect
{
    #region Public Property

    public bool IsDecide { get; private set; }

    #endregion

    #region Private Member

    private PlayerHand _enemyHand;

    #endregion

    #region Public Methods

    public override void Effect()
    {
        ChangePlayersIndex(Player);

        //相手のカードを自分のシールドトークンにしたいので
        //ここで相手の手札を選ぶ
        PhaseManager.OnNextPhase(this);
    }

    public void SelectEnemyHand(PlayerHand enemyHand)
    {
        _enemyHand = enemyHand;
    }

    public void DecideEnemyHand()
    {
        if(_enemyHand == null) _enemyHand = Players[EnemyIndex].PlayerParameter.PlayerHands[0];
        
        Players[EnemyIndex].HandCollection.RemoveHand(_enemyHand);
        Players[PlayerIndex].LifeChange.GetShield(playerHand: _enemyHand);

        Invoke("Init", 1f);
    }

    async public void LimitSelectTime(CancellationToken token)
    {
        await UniTask.Delay(20000, cancellationToken: token);

        var currentPhase = PhaseManager.CurrentPhaseProperty;
        var interventionPhase = PhaseParameter.Intervention;
        if (currentPhase == interventionPhase) DecideEnemyHand();
    }

    #endregion

    #region Private Method

    private void Init()
    {
        IsDecide = false;
        _enemyHand = null;
    }

    #endregion
}
