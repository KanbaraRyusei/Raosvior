using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    /// <summary> Playerを参照できる。</summary>
    public static IReadOnlyList<PlayerData> Players => _players;

    private static List<PlayerData> _players = new List<PlayerData>(2);// 2人対戦のため

    public static void Register(PlayerData playerData)
    {
        _players.Add(playerData);
    }

    public static void Release(PlayerData playerData)
    {
        _players.Remove(playerData);
    }
}
