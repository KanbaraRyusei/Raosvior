using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �G�N�X�v���[�W�����EF(�O�[)
/// ���������������̃��U�[�t�ɃJ�[�h��3���ȏ゠��΂����2�_���[�W��^����B
/// </summary>
public class FRockCardExplosion : HandEffect
{
    [SerializeField]
    [Header("���̌��ʂ����Ă���J�[�h")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player = 0;
    int _enemy = 1;

    const int ONE = 1;
    const int THREE = 3;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {

        //�����̃��U�[�t�ɃJ�[�h��3���ȏ゠���
        if (_playerBase[_player].PlayerTrashs.Count >= THREE)
        {
            _playerBase[_enemy].ReceiveDamage(THREE);//3�_���[�W��^����
        }
        else
        {
            _playerBase[_enemy].ReceiveDamage(ONE);//����Ƀ_���[�W��^����
        }

        _playerBase[_player].DeleteHand(_playerHand);//���̃J�[�h���̂Ă�  
    }
}
