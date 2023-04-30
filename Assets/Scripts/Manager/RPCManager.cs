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
public class RPCManager : SingletonMonoBehaviour<RPCManager>
{
    #region Member Variables

    private PhotonView _photonView;

    #endregion

    #region Events

    public event Action OnStartGame;
    public event Action<int, string> OnSelectLeaderHand;
    public event Action<int, string> OnSelectRSPHand;
    public event Action<int, string> OnSetRSPHand;
    public event Action<int, string> OnShaman;
    public event Action<int, int> OnFPaperCardJudgmentOfAigis;
    public event Action<int, string> OnPaperCardDrainShield;
    public event Action<int> OnScissorsCardChainAx;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        TryGetComponent(out _photonView);
    }

    #endregion

    #region Send Methods

    public void SendStartGame()
    {
        if (PhotonNetwork.PlayerList.Length != 2) return;
        _photonView.RPC(nameof(StartGame), RpcTarget.All);
    }

    public void SendSelectLeaderHand(int index, string name = "")
    {
        _photonView.RPC(nameof(SelectLeaderHandGame), RpcTarget.All, index, name);
    }

    public void SendSelectRSPHand(int index, string name = "")
    {
        _photonView.RPC(nameof(SelectRSPHand), RpcTarget.All, index, name);
    }

    public void SendSetRSPHand(int index, string name = "")
    {
        _photonView.RPC(nameof(SetRSPHand), RpcTarget.All, index, name);
    }

    public void SendShaman(int index, string name)
    {
        _photonView.RPC(nameof(Shaman), RpcTarget.All, index, name);
    }

    public void SendFPaperCardJudgmentOfAigis(int index, int shild)
    {
        _photonView.RPC(nameof(FPaperCardJudgmentOfAigis), RpcTarget.All, index, shild);
    }

    public void SendPaperCardDrainShield(int index, string name)
    {
        _photonView.RPC(nameof(PaperCardDrainShield), RpcTarget.All, index, name);
    }

    public void SendScissorsCardChainAx(int index)
    {
        _photonView.RPC(nameof(ScissorsCardChainAx), RpcTarget.All, index);
    }

    #endregion

    #region PunRPC Methods

    [PunRPC]
    private void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        OnStartGame?.Invoke();
        Debug.Log("Start");
    }

    [PunRPC]
    private void SelectLeaderHandGame(int index, string name)
    {
        OnSelectLeaderHand?.Invoke(index, name);
    }

    [PunRPC]
    private void SelectRSPHand(int index, string name)
    {
        OnSelectRSPHand?.Invoke(index, name);
    }

    [PunRPC]
    private void SetRSPHand(int index, string name)
    {
        OnSetRSPHand?.Invoke(index, name);
    }

    [PunRPC]
    private void Shaman(int index, string name)
    {
        OnShaman?.Invoke(index, name);
        Debug.Log("Start");
    }

    [PunRPC]
    private void FPaperCardJudgmentOfAigis(int index, int shild)
    {
        OnFPaperCardJudgmentOfAigis?.Invoke(index, shild);
        Debug.Log("Start");
    }

    [PunRPC]
    private void PaperCardDrainShield(int index, string name)
    {
        OnPaperCardDrainShield?.Invoke(index, name);
        Debug.Log("Start");
    }

    [PunRPC]
    private void ScissorsCardChainAx(int index)
    {
        OnScissorsCardChainAx?.Invoke(index);
        Debug.Log("Start");
    }

    #endregion
}
