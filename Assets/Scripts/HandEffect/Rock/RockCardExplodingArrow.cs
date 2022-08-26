using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �y�􂷂��(�O�[)
/// �������������肪�V�[���h�g�[�N�����������Ă���Ȃ炳���1�_���[�W��^����B
/// </summary>
public class RockCardExplodingArrow : HandEffect
{
    [SerializeField]
    [Header("���̌��ʂ����Ă���J�[�h")]
    PlayerHand _playerHand;

    PlayerBase[] _playerBase;
    int _player;
    int _enemy;

    const int ONE = 1;
    const int TWO = 2;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();   
    }

    public override  void Effect()
    { 
        //���肪�V�[���h�g�[�N�����������Ă���Ȃ�
        if (_playerBase[_enemy].Shild > 0)
        {
            _playerBase[_enemy].ReceiveDamage(TWO);//2�_���[�W��^����
        }
        else//�������Ă��Ȃ�������
        {
            _playerBase[_enemy].ReceiveDamage(ONE);//����Ƀ_���[�W��^����
        }

        _playerBase[_player].DeleteHand(_playerHand);//���̃J�[�h���̂Ă�  
    }
}
