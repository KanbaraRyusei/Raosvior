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
    private BattleManager _battleManager = null;

    [SerializeField]
    private Button _startGameButton = null;

    private void Awake()
    {
        Register();
    }

    /// <summary>
    /// ゲームを始めると呼ばれる関数
    /// </summary>
    private void StartGame()
    {
        _startGameButton.gameObject.SetActive(false);

        _battleManager.SetPlayerIndex();
        _battleManager.AllPhase();

        new RoomIDSaveManager()
            .SaveAsync(PhotonNetwork.CurrentRoom.Name)
            .Forget();
    }

    private void Register()
    {
        _connectionManager
            .OnJoinedRoomEvent += room =>
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    _startGameButton.gameObject.SetActive(true);
                    _startGameButton.onClick.AddListener(RPCManager.Instance.SendStartGame);
                }
            };

        RPCManager.Instance.OnStartGame += StartGame;
    }
}
