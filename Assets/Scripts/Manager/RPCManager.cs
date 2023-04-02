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
    public event Action<string> OnSelectLeaderHand;
    public event Action<string[]> OnSelectRSPHand;
    public event Action<string> OnSetRSPHand;
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
        _photonView.RPC(nameof(StartGame), RpcTarget.AllViaServer);
    }

    public void SendSelectLeaderHandGame()
    {
        _photonView.RPC(nameof(SelectLeaderHandGame), RpcTarget.AllViaServer);
    }

    public void SendSelectRSPHand()
    {
        _photonView.RPC(nameof(SelectRSPHand), RpcTarget.AllViaServer);
    }

    public void SendSetRSPHand()
    {
        _photonView.RPC(nameof(SetRSPHand), RpcTarget.AllViaServer);
    }

    public void SendShaman()
    {
        _photonView.RPC(nameof(Shaman), RpcTarget.AllViaServer);
    }

    public void SendFPaperCardJudgmentOfAigis()
    {
        _photonView.RPC(nameof(FPaperCardJudgmentOfAigis), RpcTarget.AllViaServer);
    }

    public void SendPaperCardDrainShield()
    {
        _photonView.RPC(nameof(PaperCardDrainShield), RpcTarget.AllViaServer);
    }
    public void SendScissorsCardChainAx()
    {
        _photonView.RPC(nameof(ScissorsCardChainAx), RpcTarget.AllViaServer);
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
