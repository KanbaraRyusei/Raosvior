using UniRx;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    #region Properties

    public PlayerData PlayerData { get; private set; }

    #endregion

    #region Inspector Variables

    [SerializeField]
    private PlayerView _playerView;

    #endregion

    #region Unity Methods

    private void Start()
    {
        PlayerData
            .ObserveEveryValueChanged(x => PlayerData.Life)
            .Subscribe(life => _playerView.ChangeLifeText(life))
            .AddTo(this);

        PlayerData
            .ObserveEveryValueChanged(x => PlayerData.Shield)
            .Subscribe(shild => _playerView.ChangeShieldText(shild))
            .AddTo(this);

        PlayerData
            .ObserveEveryValueChanged(x => PlayerData.Reserve.Count)
            .Subscribe(reserveCount => _playerView.ChangeReserveText(reserveCount))
            .AddTo(this);

        for (int i = 0; i < PlayerData.RSPHands.Count; i++)
        {
            PlayerData
                .ObserveEveryValueChanged(x => PlayerData.RSPHands[i])
                .Subscribe(hand => _playerView.ChangeHandsImage(hand.Sprite, i))
                .AddTo(this);
        }

        PlayerData
            .ObserveEveryValueChanged(x => PlayerData.SetRSPHand.Sprite)
            .Subscribe(cardImage => _playerView.ChangeSetHandImage(cardImage))
            .AddTo(this);
    }

    #endregion
}
