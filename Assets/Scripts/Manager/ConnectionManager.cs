using System;
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

    private string _roomName;
    private Action _onSuccess;
    private Action<string> _onError;

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

    #region Public Methods

    /// <summary>
    /// 接続
    /// </summary>
    public void Connect(string nickName, string roomName, Action onSuccess, Action<string> onError)
    {
        _roomName = roomName;
        _onSuccess = onSuccess;
        _onError = onError;

        PhotonNetwork.NickName = nickName;

        if (!PhotonNetwork.IsConnected) PhotonNetwork.ConnectUsingSettings();
        else JoinOrCreateRoom();
    }

    /// <summary>
    /// マスターに接続した
    /// </summary>
    public override void OnConnectedToMaster() =>
        JoinOrCreateRoom();

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
    public override void OnPlayerEnteredRoom(Player newPlayer) =>
        OnPlayerEnteredEvent?.Invoke(newPlayer);

    /// <summary>
    /// 部屋から去ったら呼ぶ関数
    /// </summary>
    public override void OnPlayerLeftRoom(Player otherPlayer) =>
        OnPlayerLeftEvent?.Invoke(otherPlayer);

    #endregion

    #region Private Methods

    /// <summary>
    /// ルームに参加するか作成する関数
    /// </summary>
    private void JoinOrCreateRoom()
    {
        PhotonNetwork
            .JoinOrCreateRoom
                (_roomName,
                    new RoomOptions { MaxPlayers = 2 },
                    TypedLobby.Default);
    }

    #endregion
}
