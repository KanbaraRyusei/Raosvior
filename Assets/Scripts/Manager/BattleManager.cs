using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    public int InitialLife => _initialLife;
    public int InitialHandNum => _initialHandNum;

    [SerializeField]
    [Header("Player�̏������C�t")]
    int _initialLife;

    [SerializeField]
    [Header("Player�̏�����D����")]
    int _initialHandNum;

    private bool _isSelectedPlayer1 = false;
    private bool _isSelectedPlayer2 = false;

    public enum Player
    {
        Player1,
        Player2
    }

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Player����������J�[�h��I�����I������Ƃ��ɌĂяo�����֐�
    /// </summary>
    public void SelectedNotification(Player player)
    {
        if(player == Player.Player1)
        {
            _isSelectedPlayer1 = true;
        }
        else if(player == Player.Player2)
        {
            _isSelectedPlayer2 = true;
        }
        if(_isSelectedPlayer1 && _isSelectedPlayer2)
        {
            BattleStart();
        }
    }

    private void BattleStart()
    {
        BattleJudgement();
    }

    private void BattleJudgement()
    {

    }

    private void CharacterCardEffect()
    {
        
    }

    private void BattleEnd()
    {

    }
}
