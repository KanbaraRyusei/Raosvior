using UnityEngine;

public enum PhaseParameter
{
    [Tooltip("��D�I���t�F�[�Y")]
    HandSelect,
    [Tooltip("�J�[�h�̑I���t�F�[�Y")]
    CardSelect,
    [Tooltip("�o�g�����s���菈���t�F�[�Y")]
    Battle,
    [Tooltip("���҂̃_���[�W�����t�F�[�Y")]
    WinnerDamageProcess,
    [Tooltip("���҂̃J�[�h���ʏ����t�F�[�Y")]
    WinnerCardEffect,
    [Tooltip("�L�����N�^�[�̌��ʏ����t�F�[�Y")]
    CharacterEffect,
    [Tooltip("��������t�F�[�Y")]
    Intervention,
    [Tooltip("���U�[�u�����t�F�[�Y")]
    UseCardOnReserve,
    [Tooltip("���t���b�V�������t�F�[�Y")]
    Refresh,
    [Tooltip("���������t�F�[�Y")]
    Judgement
}
