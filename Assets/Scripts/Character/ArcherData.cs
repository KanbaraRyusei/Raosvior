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
    bool test;

    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        bool draw =
            Players[MyselfIndex].PlayerSetHand.Hand == 
            Players[EnemyIndex].PlayerSetHand.Hand;
        bool[] losePattern =
        {
            //�O�[<�p�[
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper,
            //�`���L<�O�[
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Scissors &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock,
            //�p�[<�`���L
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Scissors
        };
        bool lose =
            losePattern[ConstParameter.ZERO] ||
            losePattern[ConstParameter.ONE] ||
            losePattern[ConstParameter.TWO];
        if (draw)//���������Ȃ�
        {
            //1�_���[�W�^����B
            Players[EnemyIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
        else if(lose)//�����Ȃ�
        {
            //1�_���[�W���󂯂�B
            Players[MyselfIndex]
                .ReceiveDamage(ConstParameter.ONE);
        }
    }

    #endregion
}
