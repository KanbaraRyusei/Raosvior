using System;

[Serializable]
public class PlayerInterface
{
    #region Public Properties

    public IPlayerParameter PlayerParameter { get; private set; }
    public IHandCollection HandCollection { get; private set; }
    public IChangeableLife ChangeableLife { get; private set; }

    #endregion

    #region Public Methods

    public void SetInterface(PlayerData player)
    {
        PlayerParameter = player;
        HandCollection = player;
        ChangeableLife = player;
    }

    public void SetPlayerParameter(IPlayerParameter player) =>
        PlayerParameter = player;

    public void SetHandCollection(IHandCollection player) =>
        HandCollection = player;

    public void SetLifeChange(IChangeableLife player) =>
        ChangeableLife = player;

    #endregion
}
