using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �h���C���V�[���h(�p�[)
/// �����������A����̎�D��1���I��ŗ������̂܂܎����̃V�[���h�g�[�N���ɂ���B(���̃V�[���h�g�[�N���͔j�󂳂ꂽ�Ƃ��������̃��V�[�u�ɒu��)
/// ������̃J�[�h��I������K�v������B���̃J�[�h���j�󂳂ꂽ�Ƃ��̏������K�v
/// </summary>
public class PaperCardDrainShield : HandEffect
{
    public int AddShild => _addShild;

    [SerializeField]
    [Header("���̌��ʂ����Ă���J�[�h")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;
    int _addShild = 0;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[_enemy].ReceiveDamage(ONE);//����Ƀ_���[�W��^����

        //����̃J�[�h�������̃V�[���h�g�[�N���ɂ������̂�
        //�����ő���̎�D��I��
        _playerBase[_enemy]
            .DeleteHand(_playerBase[_enemy]
            .PlayerHands//�v���C���[�n���h�̃J�[�h��index(���Ԗڂ�)�ōi�荞��
            .FirstOrDefault(x => x == _playerBase[_enemy].PlayerHands[_addShild]));

        _playerBase[_player].GetShield(ONE);//�V�[���h��ǉ�
        _playerBase[_player].DeleteHand(_playerHand);//���̃J�[�h���̂Ă�  
    }

    /// <summary>�I�񂾃J�[�h�����Ԗڂ������߂�</summary>
    public int SelectCardNumber(int number) =>  _addShild = number;
}
