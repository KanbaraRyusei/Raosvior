using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �V���[�}��
/// �_���[�W���󂯂鎞
/// �`���L��1���̂Ăă_���[�W��1���炵�Ă��悢�B
/// </summary>
public class ShamanData : CharacterBase
{
    #region Unity Methods

    private void Awake()
    {
        ChangeIntervetion(true);//�������������
    }

    #endregion

    #region public method

    /// <summary>
    /// �_���[�W���󂯂�Ƃ��ɌĂяo�����\�b�h
    /// </summary>
    /// <param name="player"></param>
    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);

        //�`���L���̂Ă�
        Players[MyselfPlayerIndex]
            .OnReserveHand(Players[MyselfPlayerIndex]
                            .PlayerHands
                            .FirstOrDefault(x => x
                                .Hand == RSPParameter
                                    .Scissors));
    }

    #endregion
}
