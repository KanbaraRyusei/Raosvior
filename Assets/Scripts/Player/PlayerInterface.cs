using System;

[Serializable]
public class PlayerInterface
{
    #region Public Properties

    public IPlayerParameter PlayerParameter { get; private set; }
    public IHandCollection HandCollection { get; private set; }
    public IChangeableLife ChangeableLife { get; private set; }
    public IGetableShield GetableShield { get; private set; }

    #endregion

    #region Public Methods

    public void SetInterface(PlayerData player)
    {
        PlayerParameter = player;
        HandCollection = player;
        ChangeableLife = player;
        GetableShield = player;
    }

    #endregion
}
