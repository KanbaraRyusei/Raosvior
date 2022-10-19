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

    public override void CardEffect(PlayerData player)
    {
        ChangePlayersIndex(player);
        // �p�[>�O�[
        bool win =
            Players[MyselfIndex].PlayerSetHand.Hand ==
            RSPParameter.Paper &&
            Players[EnemyIndex].PlayerSetHand.Hand ==
            RSPParameter.Rock;
        if (win)//�p�[�ŏ���������
        {
            // ����̃��U�[�u�̐������V�[���h�g�[�N�����l��
            Players[MyselfIndex]
                .GetShield(Players[EnemyIndex]
                            .PlayerReserve
                            .Count);
        }
    }

    #endregion
}
