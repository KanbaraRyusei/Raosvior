using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    //PlayerのInterfaceを参照できる
    public static IReadOnlyList<PlayerInterface> Players => _players;

    private static List<PlayerInterface> _players = new List<PlayerInterface>(2);// 2人対戦のため

    public static void Register(PlayerData playerData)
    {
        var player = new PlayerInterface();
        _players.Add(player);
        player.SetInterface(playerData);
    }

    public static void Release(PlayerData playerData)
    {
        foreach (var player in _players)
        {
            var isPlayer = player.PlayerParameter == playerData;
            if (isPlayer) _players.Remove(player);
        }
    }
}
