using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.UI;

public class BattleView : MonoBehaviour
{
    //�v���p�e�B
    public IObservable<bool> SelectedObservable => _isSelected;

    [SerializeField]
    [Header("�J�[�h�I������{�^��")]
    Button _cardSelectButton;

    private Subject<bool> _isSelected = new Subject<bool>();

    private void Start()
    {
        _cardSelectButton.onClick.AddListener(OnSelect);
        Debug.Log("View");
    }

    public void CanSelect()
    {
        _cardSelectButton.enabled = true;
    }

    private void OnSelect()
    {
        _cardSelectButton.enabled = false;
        _isSelected.OnNext(true);
        Debug.Log("OnSelect");
    }
}
