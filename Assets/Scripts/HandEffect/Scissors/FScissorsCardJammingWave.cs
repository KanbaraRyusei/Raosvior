using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �W���~���O�E�F�[�u�EF(�`���L)
/// �����������_���[�W��^����O�ɑ���̃V�[���h�g�[�N����S�Ĕj�󂷂�
/// </summary>
public class FScissorsCardJammingWave : HandEffect
{
    PlayerBase[] _playerBase;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {
        _playerBase[1].GetShield(-_playerBase[1].Shild);
        _playerBase[1].ReceiveDamage(ONE);
    }
}
