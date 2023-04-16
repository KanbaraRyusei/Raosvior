using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PhotonのRPC送受信を担うコンポーネント
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(PhotonView))]
public class RPCManager : MonoBehaviour
{
    #region Member Variables

    private PhotonView _photonView;

    #endregion

    #region Events

    public event Action OnReceiveStartGame;
    public event Action<int, string> OnSelectLeaderHand;
    public event Action<int, string[]> OnSelectRSPHand;
    public event Action<int, string> OnSetRSPHand;
    public event Action OnShaman;
    public event Action OnFPaperCardJudgmentOfAigis;
    public event Action OnPaperCardDrainShield;
    public event Action OnScissorsCardChainAx;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        TryGetComponent(out _photonView);
    }

    #endregion

    #region Public Methods

    public void SendStartGame()
    {
        if (PhotonNetwork.PlayerList.Length != 2) return;
        _photonView.RPC(nameof(StartGame), RpcTarget.All);
    }

    public void SendSelectLeaderHandGame()
    {
        _photonView.RPC(nameof(SelectLeaderHandGame), RpcTarget.All);
    }

    public void SendSelectRSPHand()
    {
        _photonView.RPC(nameof(SelectRSPHand), RpcTarget.All);
    }

    public void SendSetRSPHand()
    {
        _photonView.RPC(nameof(SetRSPHand), RpcTarget.All);
    }

    public void SendShaman()
    {
        _photonView.RPC(nameof(Shaman), RpcTarget.All);
    }

    public void SendFPaperCardJudgmentOfAigis()
    {
        _photonView.RPC(nameof(FPaperCardJudgmentOfAigis), RpcTarget.All);
    }

    public void SendPaperCardDrainShield()
    {
        _photonView.RPC(nameof(PaperCardDrainShield), RpcTarget.All);
    }

    public void SendScissorsCardChainAx()
    {
        _photonView.RPC(nameof(ScissorsCardChainAx), RpcTarget.All);
    }

    #endregion

    #region PunRPC Methods

    [PunRPC]
    private void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        OnReceiveStartGame?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void SelectLeaderHandGame()
    {
        OnReceiveStartGame?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void SelectRSPHand()
    {
        OnReceiveStartGame?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void SetRSPHand()
    {
        OnReceiveStartGame?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void Shaman()
    {
        OnReceiveStartGame?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void FPaperCardJudgmentOfAigis()
    {
        OnReceiveStartGame?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void PaperCardDrainShield()
    {
        OnReceiveStartGame?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void ScissorsCardChainAx()
    {
        OnReceiveStartGame?.Invoke();
        Debug.Log("Start");
    }

    #endregion
}
