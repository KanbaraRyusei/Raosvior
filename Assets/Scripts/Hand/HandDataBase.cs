using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スクリプト
/// </summary>
public abstract class HandDataBase
{
    #region Properties

    public Sprite Sprite { get; private set; }

    #endregion

    #region Public Methods

    public void GetSprite(Sprite sprite)
    {
        Sprite = sprite;
    }

    #endregion
}
