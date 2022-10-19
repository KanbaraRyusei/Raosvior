using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����̃f�[�^�̊��N���X
/// �v���C���[���Q�Ƃ���K�v�����邪
/// ��i���Ȃ����߂����ŉ��ɗp�ӂ��Ă܂�
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
    protected int MyselfIndex => _MyselfIndex;
    protected int EnemyIndex => _enemyIndex;

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
    private int _MyselfIndex;
    private int _enemyIndex;

    /// <summary>
    /// �v���C���[���Q�Ƃ�����̂��Ȃ����߉��ŗp�ӂ����z��(��)
    /// </summary>
    private PlayerData[] _players = {new(),new()};

    #endregion

    #region public method

    /// <summary>
    /// ����񂯂�̏��s�������Ƃ��ɌĂяo�����
    /// </summary>
    public abstract void CardEffect(PlayerData player);

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
            _MyselfIndex = ConstParameter.ZERO;
            _enemyIndex = ConstParameter.ONE;
        }
        else
        {
            _MyselfIndex = ConstParameter.ONE;
            _enemyIndex = ConstParameter.ZERO;
        }     
    }

    #endregion
}
