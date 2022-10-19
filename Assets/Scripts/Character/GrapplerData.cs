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

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        // �O�[>�`���L
        bool win =
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Scissors;
        if (win)//�O�[�ŏ���������
        {
            //���ʂ�������x����
            Players[MyselfIndex].PlayerSetHand.HandEffect.Effect();
        }
    }

    #endregion
}
