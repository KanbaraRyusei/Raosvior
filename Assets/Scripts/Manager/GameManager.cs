using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ConnectionManager _connectionManager = null;

    [SerializeField]
    private RPCManager _rpcManager = null;

    [SerializeField]
    private BattleManager _battleManager = null;

    [SerializeField]
    private PlayerPresenter[] _playerPresenter = new PlayerPresenter[2];

    [SerializeField]
    private Button _startGameButton = null;

    private void Awake()
    {
        _connectionManager.OnJoinedRoomEvent += room =>
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        _startGameButton.gameObject.SetActive(true);
                        _startGameButton.onClick.AddListener(_rpcManager.SendStartGame);
                    }
                };

        _rpcManager.OnReceiveStartGame += StartGame;
    }

    /// <summary>
    /// ゲームを始めると呼ばれる関数
    /// </summary>
    private void StartGame()
    {
        _startGameButton.gameObject.SetActive(false);

        if(PhotonNetwork.IsMasterClient)
        {
            PlayerManager.Register(_playerPresenter[0].PlayerData);
        }
        else
        {
            PlayerManager.Register(_playerPresenter[1].PlayerData);
        }

        _battleManager.SetPlayerIndex();
        _battleManager.AllPhase();
    }
}
