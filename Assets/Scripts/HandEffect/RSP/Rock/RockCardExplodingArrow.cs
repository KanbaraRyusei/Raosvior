using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炸裂する矢(グー)
/// 勝利した時相手がシールドトークンを所持しているならさらに1ダメージを与える。
/// </summary>
public class RockCardExplodingArrow : RSPHandEffect
{
    [SerializeField]
    [Header("この効果がついているカード")]
    PlayerHand _playerHand;

    public override  void Effect()
    {
        ChangePlayersIndex(Player);


        LifeChanges[EnemyIndex].ReceiveDamage(ConstParameter.ONE);//相手にダメージを与える

        //相手がシールドトークンを所持しているなら
        if (PlayerParameters[EnemyIndex].Shield > ConstParameter.ZERO)
        {
            //さらに1ダメージを与える
            LifeChanges[EnemyIndex].ReceiveDamage(ConstParameter.ONE);
        }

        HandCollections[PlayerIndex].OnReserveHand(_playerHand);//このカードを捨てる
        PhaseManager.OnNextPhase();
    }
}
