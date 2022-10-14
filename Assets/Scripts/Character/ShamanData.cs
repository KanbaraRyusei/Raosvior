using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シャーマン
/// ダメージを受ける時
/// チョキを1枚捨ててダメージを1減らしてもよい。
/// </summary>
public class ShamanData : CharacterBase
{
    #region Unity Methods

    private void Awake()
    {
        ChangeIntervetion(true);
    }

    #endregion

    public override void CardEffect()
    {
       
    }
}
