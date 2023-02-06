using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInterface
{
    #region Public Properties

    public IPlayerParameter PlayerParameter { get; private set; }
    public IHandCollection HandCollection { get; private set; }
    public ILifeChange LifeChange { get; private set; }

    #endregion

    #region Public Methods

    public void SetInterface(PlayerData player)
    {
        PlayerParameter = player;
        HandCollection = player;
        LifeChange = player;
    }

    public void SetPlayerParameter(IPlayerParameter player) =>
        PlayerParameter = player;

    public void SetHandCollection(IHandCollection player) =>
        HandCollection = player;

    public void SetLifeChange(ILifeChange player) =>
        LifeChange = player;

    #endregion
}
