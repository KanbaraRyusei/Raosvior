using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILifeChange
{
    /// <summary>
    /// �v���C���[�̃��C�t���񕜂���֐�
    /// </summary>
    /// <param name="heal"></param>
    void HealLife(int heal);
    /// <summary>
    /// �v���C���[�̃��C�t�Ƀ_���[�W��^����֐�
    /// �V�[���h������ΐ�ɍ���Ă��烉�C�t�Ƀ_���[�W��^����
    /// </summary>
    /// <param name="damage"></param>
    void ReceiveDamage(int damage);
    /// <summary>
    /// �v���C���[�̃V�[���h�𑝂₷�֐�
    /// </summary>
    /// <param name="num"></param>
    void GetShield(int num);
}
