using System.Collections;
using System.Collections.Generic;
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

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);

        if(true)
        {
            //�`���L���̂Ă�
            //Players[MyselfPlayerIndex].OnReserveHand();
        }
    }

    #endregion
}
