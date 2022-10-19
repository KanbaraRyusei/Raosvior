using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �V���[�}��
/// �_���[�W���󂯂鎞
/// �`���L��1���̂Ă�
/// �_���[�W��1���炵�Ă��悢�B
/// </summary>
public class ShamanData : CharacterBase
{
    #region private menber

    /// <summary>
    /// �I�񂾃`���L�̃J�[�h��ۑ�
    /// </summary>
    PlayerHand _scissorsHand;

    #endregion

    #region public method

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        //�`���L�̃J�[�h���i�荞��
        PlayerHand[] scissorsHands =
            Players[MyselfIndex]
                .PlayerHands
                .Where(x => x.Hand == RSPParameter.Scissors).ToArray();
        //�`���L�̃J�[�h���Ȃ�������
        if (scissorsHands[ConstParameter.ZERO] == null)
        {
            return;
        }
        else//�`���L�̃J�[�h����������
        {
            ChangeIntervetion(true);//�����������ɕύX

            //�`���L�̃J�[�h��I��
            _scissorsHand = scissorsHands[default];
            //�`���L���̂Ă�
            Players[MyselfIndex]
            .OnReserveHand(_scissorsHand);
            //1��(�_���[�W��1���炵�Ă��悢�̑����)
            Players[MyselfIndex]
            .HealLife(ConstParameter.ONE);
        }  
    }

    #endregion
}
