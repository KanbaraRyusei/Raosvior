using UnityEngine;

/// <summary>
/// �Q�[���̐i�s�Ǘ�������N���X
/// Presenter�݂̂��Q�Ƃ�����
/// </summary>
public class PhaseManager : MonoBehaviour
{
    #region public property

    /// <summary>
    /// ���݂̃t�F�[�Y�����J����v���p�e�B
    /// </summary>
    public PhaseParameter CurrentPhasePropety { get; private set;}

    #endregion

    #region private member

    private PhaseParameter _currentPhase;// ���݂̃t�F�[�Y
    private PhaseParameter _oldPhase;// 1�O�̃t�F�[�Y

    #endregion

    #region public method

    /// <summary>
    /// �t�F�[�Y��i�߂�֐�
    /// �������������Ƃ��݈̂�����true��n��
    /// </summary>
    /// <param name="isIntervetion"></param>
    public void OnNextPhase(bool isIntervetion = false)
    {
        if(_currentPhase == PhaseParameter.Judgement)// �����������肾������
        {
            _oldPhase = _currentPhase;// �O�̃t�F�[�Y��ۑ�
            _currentPhase = PhaseParameter.CardSelect;// �ŏ��̃t�F�[�Y�ɖ߂�
            CurrentPhasePropety = _currentPhase;// �O���Ɍ��݂̃t�F�[�Y�����J����
            return;
        }
        if(isIntervetion)// ���������������������
        {
            _oldPhase = _currentPhase;// �O�̃t�F�[�Y��ۑ�
            _currentPhase = PhaseParameter.Intervention;// ����t�F�[�Y�ɂ���
            CurrentPhasePropety = _currentPhase;// �O���Ɍ��݂̃t�F�[�Y�����J����
            return;
        }
        if(_currentPhase == PhaseParameter.Intervention)// �������������������
        {
            _currentPhase = _oldPhase;// ����O�̃t�F�[�Y�ɖ߂�
        }
        _oldPhase = _currentPhase;// �O�̃t�F�[�Y��ۑ�
        _currentPhase++;// ���݂̃t�F�[�Y�����̃t�F�[�Y�ɐi�߂�
        CurrentPhasePropety = _currentPhase;// �O���Ɍ��݂̃t�F�[�Y�����J����
    }

    #endregion
}
