using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    /// <summary> Playerを参照できる。</summary>
    public static IReadOnlyList<PlayerData> Players => _players;

    /// <summary> PlayerのIHandCollectionを参照できる。</summary>
    public static IReadOnlyList<IHandCollection> HandCollections => _handCollections;

    /// <summary> PlayerのILifeChangeを参照できる。</summary>
    public static IReadOnlyList<ILifeChange> LifeChanges => _lifeChanges;

    private static List<PlayerData> _players = new List<PlayerData>(2);// 2人対戦のため
    private static List<IHandCollection> _handCollections = new List<IHandCollection>(2);
    private static List<ILifeChange> _lifeChanges = new List<ILifeChange>(2);

    public static void Register(PlayerData playerData)
    {
        _players.Add(playerData);
        _handCollections.Add(playerData);
        _lifeChanges.Add(playerData);
    }

    public static void Release(PlayerData playerData)
    {
        _players.Remove(playerData);
        _handCollections.Remove(playerData);
        _lifeChanges.Remove(playerData);
    }
}
