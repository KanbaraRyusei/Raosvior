using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �i�C�g
/// �p�[�ŏ���������
/// ����̃��U�[�u�̐�����
/// �V�[���h�g�[�N�����l������B
/// </summary>
public class knightData : CharacterBase
{
    #region public method

    /// <summary>
    /// �p�[�ŏ��������Ƃ��ɌĂяo��
    /// </summary>
    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);

        if(true)//�p�[�ŏ���������
        {
            // �V�[���h�g�[�N�����l������B
            Players[MyselfPlayerIndex]
                .GetShield(Players[EnemyPlayerIndex]
                            .PlayerReserve
                            .Count);
        }
    }

    #endregion
}
