using Cysharp.Threading.Tasks;
using Photon.Pun;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/// <summary>
/// 最初の画面
/// </summary>
[DisallowMultipleComponent]
public class StartPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiRoot = null;

    [SerializeField]
    private Button _joinButton = null;

    [SerializeField]
    private ConnectionManager _connectionManager = null;

    private void Awake()
    {
        _joinButton.onClick.AddListener(OnStartButtonClicked);

        _uiRoot.SetActive(true);
    }

    async private void OnStartButtonClicked()
    {
        _joinButton.interactable = false;

        var nickName = "";
        var roomName = $"room-{100:D03}";

        _connectionManager
            .Connect
                (nickName, Transition, Debug.LogError);

        await UniTask.Delay(TimeSpan.FromSeconds(10f));

        _joinButton.interactable = true;
    }

    private void Transition()
    {
        _uiRoot.SetActive(false);
    }
}
