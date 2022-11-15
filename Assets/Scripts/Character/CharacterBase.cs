using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラのデータの基底クラス
/// プレイヤーを参照する必要があるが
/// 手段がないためここで仮に用意してます
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    #region protected property

    // プレイヤーを参照するList用のインデックス
    protected int PlayerIndex { get; private set; }
    protected int EnemyIndex { get; private set; }

    #endregion

    #region protected member

    /// <summary>
    /// プレイヤーを参照するList
    /// </summary>
    protected IReadOnlyList<PlayerData> _players = PlayerManager.Players;

    #endregion

    #region public method

    /// <summary>
    /// じゃんけんの勝敗がついたときに呼び出される
    /// </summary>
    public abstract void CardEffect(PlayerData player);

    #endregion

    #region protected method

    /// <summary>
    /// プレイヤーを切り替えるメソッド
    /// </summary>
    protected void ChangePlayersIndex(PlayerData player)
    {
        if(_players[ConstParameter.ZERO] == player)
        {
            PlayerIndex = ConstParameter.ZERO;
            EnemyIndex = ConstParameter.ONE;
        }
        else
        {
            PlayerIndex = ConstParameter.ONE;
            EnemyIndex = ConstParameter.ZERO;
        }     
    }

    #endregion
}
