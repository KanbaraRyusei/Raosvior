using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    #region Public Property

    //PlayerのInterfaceを参照できる
    public static IReadOnlyList<PlayerInterface> Players => _players;

    #endregion

    #region Private Member

    private static List<PlayerInterface> _players = new List<PlayerInterface>();

    #endregion

    #region Public Methods

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

    #endregion
}
