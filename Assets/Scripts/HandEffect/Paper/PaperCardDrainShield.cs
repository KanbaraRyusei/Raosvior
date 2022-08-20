using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �h���C���V�[���h(�p�[)
/// �����������A����̎�D��1���I��ŗ������̂܂܎����̃V�[���h�g�[�N���ɂ���B(���̃V�[���h�g�[�N���͔j�󂳂ꂽ�Ƃ��������̃��V�[�u�ɒu��)
/// </summary>
public class PaperCardDrainShield : HandEffect
{
    PlayerBase[] _playerBase;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[1].ReceiveDamage(ONE);//����Ƀ_���[�W��^����

        //�����ő���̎�D��I��

        _playerBase[0].GetShield(ONE);//�V�[���h��ǉ�
    }
}
