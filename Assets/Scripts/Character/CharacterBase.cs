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
    #region public property

    /// <summary>
    /// 介入処理の有無を公開するプロパティ
    /// </summary>
    public bool IsIntervetion => _isIntervetion;

    #endregion

    #region protected property

    // プレイヤーを参照する配列用のインデックス
    protected int MyselfIndex => _MyselfIndex;
    protected int EnemyIndex => _enemyIndex;

    /// <summary>
    /// プレイヤーを参照するものがないため
    /// 仮で用意した派生クラス用プロパティ(仮)
    /// </summary>
    protected PlayerData[] Players => _players;

    #endregion

    #region private member
    // <summary>
    /// 介入処理の有無のメンバー変数
    /// </summary>
    private bool _isIntervetion;

    //プレイヤーを参照する配列用のインデックス
    private int _MyselfIndex;
    private int _enemyIndex;

    /// <summary>
    /// プレイヤーを参照するものがないため仮で用意した配列(仮)
    /// </summary>
    private PlayerData[] _players = {new(),new()};

    #endregion

    #region public method

    /// <summary>
    /// じゃんけんの勝敗がついたときに呼び出される
    /// </summary>
    public abstract void CardEffect(PlayerData player);

    #endregion

    #region protected method

    /// <summary>
    /// 介入処理の有無を変更するメソッド
    /// </summary>
    protected bool ChangeIntervetion(bool isIntervetion)
    {
        return _isIntervetion = isIntervetion;
    }

    /// <summary>
    /// プレイヤーを切り替えるメソッド(仮)
    /// </summary>
    protected void ChangePlayersIndex(PlayerData player)
    {
        if(_players[ConstParameter.ZERO] == player)
        {
            _MyselfIndex = ConstParameter.ZERO;
            _enemyIndex = ConstParameter.ONE;
        }
        else
        {
            _MyselfIndex = ConstParameter.ONE;
            _enemyIndex = ConstParameter.ZERO;
        }     
    }

    #endregion
}
