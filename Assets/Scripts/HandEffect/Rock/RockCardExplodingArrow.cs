using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �y�􂷂��(�O�[)
/// �������������肪�V�[���h�g�[�N�����������Ă���Ȃ炳���1�_���[�W��^����B
/// </summary>
public class RockCardExplodingArrow : HandEffect
{
    PlayerBase[] _playerBase;

    const int ONE = 1;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();   
    }

    public override void Effect()
    {
        _playerBase[1].ReceiveDamage(ONE);

        if(_playerBase[0].Shild > 0)
        {
            _playerBase[1].ReceiveDamage(ONE);
        }
    }
}
