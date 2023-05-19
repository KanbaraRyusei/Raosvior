using System;
using Photon.Pun;
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

    //選択したカード
    public event Action<int, string> OnSelectLeaderHand;
    public event Action<int, string> OnSelectRSPHand;
    public event Action<int, string> OnSetRSPHand;

    //介入処理
    public event Action<int, string> OnShaman;
    public event Action<int, int> OnFPaperCardJudgmentOfAigis;
    public event Action<int, string> OnPaperCardDrainShield;
    public event Action<int> OnScissorsCardChainAx;

    //再接続
    public event Action<PhaseParameter, PhaseParameter> OnResetPhase;

    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        base.Awake();
        _photonView ??= GetComponent<PhotonView>();
        OnResetPhase += PhaseManager.SetPhase;
    }

    private void OnDestroy()
    {
        OnResetPhase -= PhaseManager.SetPhase;
    }

    #endregion

    #region Send Methods

    public void SendStartGame()
    {
        if (PhotonNetwork.PlayerList.Length != 2) return;
        _photonView.RPC(nameof(StartGame), RpcTarget.AllBuffered);
    }

    public void SendSelectLeaderHand(int index, string name = "")
    {
        _photonView.RPC(nameof(SelectLeaderHandGame), RpcTarget.AllBuffered, index, name);
    }

    public void SendSelectRSPHand(int index, string name = "")
    {
        _photonView.RPC(nameof(SelectRSPHand), RpcTarget.AllBuffered, index, name);
    }

    public void SendSetRSPHand(int index, string name = "")
    {
        _photonView.RPC(nameof(SetRSPHand), RpcTarget.AllBuffered, index, name);
    }

    public void SendShaman(int index, string name)
    {
        _photonView.RPC(nameof(Shaman), RpcTarget.AllBuffered, index, name);
    }

    public void SendFPaperCardJudgmentOfAigis(int index, int shild)
    {
        _photonView.RPC(nameof(FPaperCardJudgmentOfAigis), RpcTarget.AllBuffered, index, shild);
    }

    public void SendPaperCardDrainShield(int index, string name)
    {
        _photonView.RPC(nameof(PaperCardDrainShield), RpcTarget.AllBuffered, index, name);
    }

    public void SendScissorsCardChainAx(int index)
    {
        _photonView.RPC(nameof(ScissorsCardChainAx), RpcTarget.AllBuffered, index);
    }

    public void ResendSetPhase(PhaseParameter current, PhaseParameter old)
    {
        _photonView.RPC(nameof(ResetPhase), RpcTarget.OthersBuffered, current, old);
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
    }

    [PunRPC]
    private void FPaperCardJudgmentOfAigis(int index, int shild)
    {
        OnFPaperCardJudgmentOfAigis?.Invoke(index, shild);
    }

    [PunRPC]
    private void PaperCardDrainShield(int index, string name)
    {
        OnPaperCardDrainShield?.Invoke(index, name);
    }

    [PunRPC]
    private void ScissorsCardChainAx(int index)
    {
        OnScissorsCardChainAx?.Invoke(index);
    }

    [PunRPC]
    private void ResetPhase(PhaseParameter current, PhaseParameter old)
    {
        OnResetPhase?.Invoke(current, old);
    }

    #endregion
}
