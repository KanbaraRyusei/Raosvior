using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UnityEngine.UI;

public class BattleView : MonoBehaviour
{
    //プロパティ
    public IObservable<(bool, RSPParam)> SelectedObservable => _isSelected;

    [SerializeField]
    [Header("カード選択決定ボタン")]
    Button _cardSelectButton;

    private Subject<(bool, RSPParam)> _isSelected = new Subject<(bool, RSPParam)>();
    RSPParam _selectCard;

    private void Start()
    {
        _cardSelectButton.onClick.AddListener(OnSelect);
        Debug.Log("View");
    }

    public void CanSelect()
    {
        _cardSelectButton.enabled = true;
    }

    public void TentativeSelect()
    {
        
    }

    private void OnSelect()
    {
        _cardSelectButton.enabled = false;
        //_isSelected.OnNext((true, ));
        Debug.Log("OnSelect");
    }
}
