using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�[�`���[
/// ������������1�_���[�W�^����B
/// ���������A1�_���[�W���󂯂�B
/// </summary>
public class ArcherData : CharacterBase
{
    #region public method

    /// <summary>
    /// �����ȊO�̎��ɌĂяo��
    /// </summary>
    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);

        if(true)//�����Ȃ�
        {
            //1�_���[�W���󂯂�B
            Players[MyselfPlayerIndex].ReceiveDamage(ConstParameter.ONE);
        }
        else//���������Ȃ�
        {
            //1�_���[�W�^����B
            Players[EnemyPlayerIndex].ReceiveDamage(ConstParameter.ONE);
        }
    }

    #endregion
}
