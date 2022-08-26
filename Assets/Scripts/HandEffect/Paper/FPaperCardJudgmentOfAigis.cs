using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�M�X�̍ق��EF(�p�[)
/// �����������A�����̃V�[���h�g�[�N����1�ɕt�������1�_���[�W��^���Ă��悢���������ꍇ�A�����̃V�[���h�g�[�N����j�󂷂�B
/// �������̃V�[���h�����炷���������߂鏈�����K�v
/// </summary>
public class FPaperCardJudgmentOfAigis : HandEffect
{
    public int AddDamege => _addDamege;

    [SerializeField]
    [Header("���̌��ʂ����Ă���J�[�h")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;
    int _addDamege = 0;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        //�����̃V�[���h��j�󂷂閇�������߂��悤�ɂ���


        //�����̃V�[���h�����炷
        _playerBase[_player].GetShield(_addDamege);

        //�ʏ�_���[�W+�ǉ��_���[�W(�V�[���h�����炵��������)��^����
        _playerBase[_enemy].ReceiveDamage(ONE + _addDamege);

        _playerBase[_player].DeleteHand(_playerHand);//���̃J�[�h���̂Ă�
    }

    /// <summary>�V�[���h�����炷�ʂ�ݒ肷��</summary>
    public int SelectBreakShields(int num) => _addDamege -= num;
}
