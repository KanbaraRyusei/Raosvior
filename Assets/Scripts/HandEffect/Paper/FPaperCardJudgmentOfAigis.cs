using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �A�C�M�X�̍ق��EF(�p�[)
/// �����������A�����̃V�[���h�g�[�N����1�ɕt�������1�_���[�W��^���Ă��悢���������ꍇ�A�����̃V�[���h�g�[�N����j�󂷂�B
/// </summary>
public class FPaperCardJudgmentOfAigis : HandEffect
{
    PlayerBase[] _playerBase;

    int _addDamege = 0;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[1].ReceiveDamage(ONE);//����Ƀ_���[�W��^����

        //�����̃V�[���h��j�󂷂閇�������߂�

        _playerBase[0].GetShield(_addDamege);//�����̃V�[���h�����炷
        _playerBase[1].ReceiveDamage(_addDamege);//����ɂ̃_���[�W(�����̃V�[���h�����炵��������)��^����
    }

    /// <summary>�V�[���h�����炷�ʂ�ݒ肷��</summary>
    public int BreakMyShield(int num) => _addDamege -= num;
}
