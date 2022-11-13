using UniRx;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    PlayerData _playerData;

    [SerializeField]
    PlayerView _playerView;

    private void Awake()
    {
        _playerData = PlayerManager.Players[0];
    }

    private void Start()
    {
        _playerData.ObserveEveryValueChanged(x => _playerData.Life)
            .Subscribe(x => _playerView.ChangeLifeText(x)).AddTo(this);

        _playerData.ObserveEveryValueChanged(x => _playerData.Shield)
            .Subscribe(x => _playerView.ChangeShieldText(x)).AddTo(this);

        _playerData.ObserveEveryValueChanged(x => _playerData.PlayerReserve.Count)
            .Subscribe(x => _playerView.ChangeReserveText(x));

        for (int i = 0; i < _playerData.PlayerHands.Count; i++)
        {
            _playerData.ObserveEveryValueChanged(x => _playerData.PlayerHands[i])
                .Subscribe(x => _playerView.ChangeHandsImage(x.CardImage, i));
        }

        _playerData.ObserveEveryValueChanged(x => _playerData.PlayerSetHand.CardImage)
            .Subscribe(x => _playerView.ChangeSetHandImage(x));
    }
}
