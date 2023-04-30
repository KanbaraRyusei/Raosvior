using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    #region Public Property

    //PlayerのInterfaceを参照できる
    public PlayerInterface[] Players { get; private set; } =
        new PlayerInterface[2];

    #endregion

    #region Inspector Variables


    [SerializeField]
    private PlayerPresenter[] _players = null;

    #endregion

    #region Member Variables

    private int _currentIndex = 0;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        Caster(_players[0].PlayerData);
        Caster(_players[1].PlayerData);
    }

    #endregion

    #region Public Methods

    public void Caster(PlayerData playerData)
    {
        Players[_currentIndex].SetInterface(playerData);
        _currentIndex++;
    }

    [Button]
    public void FindPresenter()
    {
        _players = FindObjectsOfType<PlayerPresenter>();
    }

    #endregion
}
