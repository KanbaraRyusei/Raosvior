using System;
using Cysharp.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

/// <summary>
/// Photonの接続周りの処理をコールバックを担うコンポーネント
/// </summary>
[DisallowMultipleComponent]
public class ConnectionManager : MonoBehaviourPunCallbacks
{
    #region Private Variables

    private Action _onSuccess;
    private Action<string> _onError;
    private bool _isLefting = false;

    #endregion

    #region Events

    public event Action<Room> OnJoinedRoomEvent;
    public event Action OnJoinedRoomGeneratePlayer;
    public event Action<Player> OnPlayerEnteredEvent;
    public event Action<Player> OnPlayerLeftEvent;

    #endregion

    #region Unity Method

    private void OnDestroy()
    {
        Debug.Log("Disconnect");
        PhotonNetwork.Disconnect();
    }

    #endregion

    #region PunCallbacks Methods

    /// <summary>
    /// 接続
    /// </summary>
    public void Connect(string nickName, Action onSuccess, Action<string> onError)
    {
        _onSuccess = onSuccess;
        _onError = onError;

        PhotonNetwork.NickName = nickName;

        if (!PhotonNetwork.IsConnected) PhotonNetwork.ConnectUsingSettings();
        else PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// マスターに接続した
    /// </summary>
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    /// <summary>
    /// ランダムで参加できるルームが存在しないなら、新規でルームを作成する
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        var roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    /// <summary>
    /// ルームに参加したら呼ばれる関数
    /// </summary>
    public override void OnJoinedRoom()
    {
        _onSuccess?.Invoke();
        OnJoinedRoomEvent?.Invoke(PhotonNetwork.CurrentRoom);
        OnJoinedRoomGeneratePlayer?.Invoke();
    }

    /// <summary>
    /// ルームの作成に失敗したら呼ばれる関数
    /// </summary>
    public override void OnCreateRoomFailed(short returnCode, string message) =>
        _onError?.Invoke($"CreateRoomFailed: {message} ({returnCode})");

    /// <summary>
    /// ルームの参加に失敗したら呼ばれる関数
    /// </summary>
    public override void OnJoinRoomFailed(short returnCode, string message) =>
        _onError?.Invoke($"JoinRoomFailed: {message} ({returnCode})");

    /// <summary>
    /// 切断されたら呼ばれる関数
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        var isCause = cause != DisconnectCause.None;
        if (isCause) _onError?.Invoke($"Disconnected: {cause}");
    }

    /// <summary>
    /// 部屋に入ったら呼ぶ関数
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        OnPlayerEnteredEvent?.Invoke(newPlayer);

        //再接続
        if (_isLefting && PhotonNetwork.IsMasterClient)
        {
            _isLefting = false;

            var rpc = RPCManager.Instance;

            for (int i = 0; i < PlayerManager.Instance.Players.Length; i++)
            {
                var player = PlayerManager.Instance.Players[i];
                rpc.SendSelectLeaderHand(i, player.PlayerParameter.LeaderHand.Hand.CardName);

                foreach (var hand in player.PlayerParameter.RSPHands)
                    rpc.SendSelectRSPHand(i, hand.Hand.CardName);

                rpc.SendSetRSPHand(i, player.PlayerParameter.SetRSPHand.Hand.CardName);
                rpc.SendSetPhase(PhaseManager.CurrentPhase, PhaseManager.OldPhase);
            }
            rpc.SendRestartGame();
        }
    }

    /// <summary>
    /// 部屋から去ったら呼ぶ関数
    /// </summary>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        OnPlayerLeftEvent?.Invoke(otherPlayer);
        _isLefting = true;
    }

    #endregion
}
