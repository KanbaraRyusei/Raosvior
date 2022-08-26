using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �W���~���O�E�F�[�u�EF(�`���L)
/// �����������_���[�W��^����O�ɑ���̃V�[���h�g�[�N����S�Ĕj�󂷂�
/// </summary>
public class FScissorsCardJammingWave : HandEffect
{
    [SerializeField]
    [Header("���̌��ʂ����Ă���J�[�h")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[_enemy].GetShield(-_playerBase[_enemy].Shild);//����̃V�[���h��S�Ĕj��
        _playerBase[_enemy].ReceiveDamage(ONE);//����Ƀ_���[�W��^����
        _playerBase[_player].DeleteHand(_playerHand);//���̃J�[�h���̂Ă�  
    }
}
