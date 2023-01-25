using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInterface
{
    #region Public Properties

    public IPlayerParameter PlayerParameter => _playerParameter;
    public IUseHand UseHand => _useHand;
    public IHandCollection HandCollection => _handCollection;
    public ILifeChange LifeChange => _lifeChange;

    #endregion

    #region Private Member

    private IPlayerParameter _playerParameter;
    private IUseHand _useHand;
    private IHandCollection _handCollection;
    private ILifeChange _lifeChange;

    #endregion

    #region Public Methods

    public void SetInterface(PlayerData player)
    {
        _playerParameter = player;
        _useHand = player;
        _handCollection = player;
        _lifeChange = player;
    }

    public void SetPlayerParameter(IPlayerParameter player) =>
        _playerParameter = player;

    public void SetUseHand(IUseHand player) =>
        _useHand = player;

    public void SetHandCollection(IHandCollection player) =>
        _handCollection = player;

    public void SetLifeChange(ILifeChange player) =>
        _lifeChange = player;

    #endregion
}
