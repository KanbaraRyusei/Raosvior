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

    public void SendSelectLeaderHandGame(int index, string name)
    {
        _photonView.RPC(nameof(SelectLeaderHandGame), RpcTarget.All, index, name);
    }

    public void SendSelectRSPHand(int index, string[] name)
    {
        _photonView.RPC(nameof(SelectRSPHand), RpcTarget.All, index, name);
    }

    public void SendSetRSPHand(int index, string name)
    {
        _photonView.RPC(nameof(SetRSPHand), RpcTarget.All, index, name);
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
    private void SelectLeaderHandGame(int index, string name)
    {
        OnSelectLeaderHand?.Invoke(index, name);
    }

    [PunRPC]
    private void SelectRSPHand(int index, string[] name)
    {
        OnSelectRSPHand?.Invoke(index, name);
    }

    [PunRPC]
    private void SetRSPHand(int index, string name)
    {
        OnSetRSPHand?.Invoke(index, name);
    }

    [PunRPC]
    private void Shaman()
    {
        OnShaman?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void FPaperCardJudgmentOfAigis()
    {
        OnFPaperCardJudgmentOfAigis?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void PaperCardDrainShield()
    {
        OnPaperCardDrainShield?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void ScissorsCardChainAx()
    {
        OnScissorsCardChainAx?.Invoke();
        Debug.Log("Start");
    }

    #endregion
}
