using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �O���b�v���[
/// �O�[�ŏ��������Ƃ�
/// �J�[�h���ʂ�������x��������B
/// </summary>
public class GrapplerData : CharacterBase
{
    #region public method

    /// <summary>
    /// �O�[�ŏ��������Ƃ��ɌĂяo��
    /// </summary>
    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        if(true)//�O�[�ŏ���������
        {
            Players[MyselfPlayerIndex].PlayerSetHand.HandEffect.Effect();
        }
    }

    #endregion
}
