using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���J�[�h����D�ɖ߂����ǂ����̑I�����K�v
/// <summary>
/// �`�F�[���A�b�N�X(�`���L)
/// �������������̃J�[�h����D�ɖ߂���B
/// </summary>
public class ScissorsCardChainAx : HandEffect
{
    [SerializeField]
    [Header("���̌��ʂ����Ă���J�[�h")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;
    bool _isDelete;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[_enemy].ReceiveDamage(ONE);//����Ƀ_���[�W��^����

        //���̃J�[�h����D�ɖ߂��̂ŃJ�[�h���̂Ă邩�I���ł���悤�ɂ���


        //�̂Ă�Ȃ�
        if(_isDelete)
        {
            _playerBase[_player].DeleteHand(_playerHand);//���̃J�[�h���̂Ă�  
        }
    }

    public bool IsDelete(bool delete) => _isDelete = delete;
}
