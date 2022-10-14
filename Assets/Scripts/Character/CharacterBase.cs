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

    #region private member

    private bool _isIntervetion;

    private PlayerData[] _playerDatas = {new(),new()};

    #endregion

    #region public method

    /// <summary>
    /// �J�[�h�̌��ʂ�����
    /// </summary>
    public abstract void CardEffect();

    /// <summary>
    /// ��������̗L����ύX���郁�\�b�h
    /// </summary>
    protected bool ChangeIntervetion(bool isIntervetion)
    {
        return _isIntervetion = isIntervetion;
    }

    #endregion
}
