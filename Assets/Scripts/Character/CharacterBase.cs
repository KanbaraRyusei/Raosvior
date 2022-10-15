using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����̃f�[�^�̊��N���X
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    #region public property

    /// <summary>
    /// ��������̗L�������J����v���p�e�B
    /// </summary>
    public bool IsIntervetion => _isIntervetion;

    #endregion

    #region protected property

    // �v���C���[���Q�Ƃ���z��p�̃C���f�b�N�X
    protected int MyselfPlayerIndex => _MyselfPlayerIndex;
    protected int EnemyPlayerIndex => _enemyPlayerIndex;

    /// <summary>
    /// �v���C���[���Q�Ƃ�����̂��Ȃ�����
    /// ���ŗp�ӂ����h���N���X�p�v���p�e�B(��)
    /// </summary>
    protected PlayerData[] Players => _players;

    #endregion

    #region private member
    // <summary>
    /// ��������̗L���̃����o�[�ϐ�
    /// </summary>
    private bool _isIntervetion;

    //�v���C���[���Q�Ƃ���z��p�̃C���f�b�N�X
    private int _MyselfPlayerIndex;
    private int _enemyPlayerIndex;

    /// <summary>
    /// �v���C���[���Q�Ƃ�����̂��Ȃ����߉��ŗp�ӂ����z��(��)
    /// </summary>
    private PlayerData[] _players = {new(),new()};

    #endregion

    #region public method

    /// <summary>
    /// �J�[�h�̌��ʂ�����
    /// </summary>
    public abstract void CardEffect(PlayerData player);

    //public abstract void CardEffect(PlayerData player,);


    #endregion

    #region protected method

    /// <summary>
    /// ��������̗L����ύX���郁�\�b�h
    /// </summary>
    protected bool ChangeIntervetion(bool isIntervetion)
    {
        return _isIntervetion = isIntervetion;
    }

    /// <summary>
    /// �v���C���[��؂�ւ��郁�\�b�h(��)
    /// </summary>
    protected void ChangePlayersIndex(PlayerData player)
    {
        if(_players[ConstParameter.ZERO] == player)
        {
            _MyselfPlayerIndex = ConstParameter.ZERO;
            _enemyPlayerIndex = ConstParameter.ONE;
        }
        else
        {
            _MyselfPlayerIndex = ConstParameter.ONE;
            _enemyPlayerIndex = ConstParameter.ZERO;
        }     
    }

    #endregion
}
