using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandCollection
{
    /// <summary>
    /// ��D����ɃZ�b�g����֐�
    /// </summary>
    /// <param name="playerHand"></param>
    void SetHand(PlayerHand playerHand);

    /// <summary>
    /// ��ɃZ�b�g���ꂽ�J�[�h�����U�[�u�ɑ���֐�
    /// </summary>
    void SetCardOnReserve();

    /// <summary>
    /// ��D�ɃJ�[�h��������֐�
    /// </summary>
    /// <param name="playerHand"></param>
    void AddHand(PlayerHand playerHand);

    /// <summary>
    /// ��D�����U�[�u�ɑ���֐�
    /// </summary>
    /// <param name="playerHand"></param>
    void OnReserveHand(PlayerHand playerHand);
}
