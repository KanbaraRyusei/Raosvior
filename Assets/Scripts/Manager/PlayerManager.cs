using System.Collections;
using System.Collections.Generic;
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
    private PlayerPresenter _client = null;

    [SerializeField]
    private PlayerPresenter _other = null;

    [SerializeField]
    private HandSetter _handSetter = null;

    #endregion

    #region Member Variables

    private int _currentIndex = 0;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        Caster(_client.PlayerData);
        Caster(_other.PlayerData);
    }

    #endregion

    #region Public Methods

    public void Caster(PlayerData playerData)
    {
        Players[_currentIndex].SetInterface(playerData);
        _currentIndex++;
    }

    #endregion
}
