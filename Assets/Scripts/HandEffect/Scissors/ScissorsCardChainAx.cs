using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �`�F�[���A�b�N�X(�`���L)
/// �������������̃J�[�h����D�ɖ߂���B
/// </summary>
public class ScissorsCardChainAx : HandEffect
{
    PlayerBase[] _playerBase;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {

    }
}
