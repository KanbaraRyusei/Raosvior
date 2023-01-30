using UniRx;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    public PlayerData PlayerData { get; private set; }

    [SerializeField]
    private PlayerView _playerView;

    private void Awake()
    {
        //_playerData = PlayerManager.Players[0];
    }

    private void Start()
    {
        PlayerData.ObserveEveryValueChanged(x => PlayerData.Life)
            .Subscribe(x => _playerView.ChangeLifeText(x)).AddTo(this);

        PlayerData.ObserveEveryValueChanged(x => PlayerData.Shield)
            .Subscribe(x => _playerView.ChangeShieldText(x)).AddTo(this);

        PlayerData.ObserveEveryValueChanged(x => PlayerData.PlayerReserve.Count)
            .Subscribe(x => _playerView.ChangeReserveText(x)).AddTo(this);

        for (int i = 0; i < PlayerData.PlayerHands.Count; i++)
        {
            PlayerData.ObserveEveryValueChanged(x => PlayerData.PlayerHands[i])
                .Subscribe(x => _playerView.ChangeHandsImage(x.CardImage, i)).AddTo(this);
        }

        PlayerData.ObserveEveryValueChanged(x => PlayerData.PlayerSetHand.CardImage)
            .Subscribe(x => _playerView.ChangeSetHandImage(x)).AddTo(this);
    }
}
