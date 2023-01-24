using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    //PlayerのInterfaceを参照できる
    public static IReadOnlyList<IPlayerParameter> PlayerParameters => _playerParameters;
    public static IReadOnlyList<IUseHand> UseHands => _useHands;
    public static IReadOnlyList<IHandCollection> HandCollections => _handCollections;
    public static IReadOnlyList<ILifeChange> LifeChanges => _lifeChanges;

    private static List<IPlayerParameter> _playerParameters = new List<IPlayerParameter>(2);
    private static List<IUseHand> _useHands = new List<IUseHand>(2);
    private static List<IHandCollection> _handCollections = new List<IHandCollection>(2);// 2人対戦のため
    private static List<ILifeChange> _lifeChanges = new List<ILifeChange>(2);

    public static void Register(PlayerData playerData)
    {
        _playerParameters.Add(playerData);
        _useHands.Add(playerData);
        _handCollections.Add(playerData);
        _lifeChanges.Add(playerData);
    }

    public static void Release(PlayerData playerData)
    {
        _playerParameters.Remove(playerData);
        _useHands.Remove(playerData);
        _handCollections.Remove(playerData);
        _lifeChanges.Remove(playerData);
    }
}
