using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラのデータの基底クラス
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    #region public property

    /// <summary>
    /// 介入処理の有無を公開するプロパティ
    /// </summary>
    public bool IsIntervetion => _isIntervetion;

    #endregion

    #region private member

    private bool _isIntervetion;

    private PlayerData[] _playerDatas = {new(),new()};

    #endregion

    #region public method

    /// <summary>
    /// カードの効果を書く
    /// </summary>
    public abstract void CardEffect();

    /// <summary>
    /// 介入処理の有無を変更するメソッド
    /// </summary>
    protected bool ChangeIntervetion(bool isIntervetion)
    {
        return _isIntervetion = isIntervetion;
    }

    #endregion
}
