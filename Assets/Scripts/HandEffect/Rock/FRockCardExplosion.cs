using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G�N�X�v���[�W�����EF(�O�[)
/// ���������������̃��U�[�t�ɃJ�[�h��3���ȏ゠��΂����2�_���[�W��^����B
/// </summary>
public class FRockCardExplosion : HandEffect
{
    PlayerBase[] _playerBase;

    const int THREE_CARDS = 3;
    const int ONE = 1;
    const int TWO = 2;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[1].ReceiveDamage(ONE);

        if(_playerBase[0].PlayerTrashs.Count >= THREE_CARDS)
        {
            _playerBase[1].ReceiveDamage(TWO);
        }
    }
}
