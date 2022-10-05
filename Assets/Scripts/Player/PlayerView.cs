using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    [Header("���C�t�̃e�L�X�g")]
    Text _lifeText;

    [SerializeField]
    [Header("�V�[���h�̃e�L�X�g")]
    Text _shieldText;

    [SerializeField]
    [Header("��n�̖����̃e�L�X�g")]
    Text _playerReserve;

    [SerializeField]
    [Header("�Z�b�g����J�[�h�̉摜")]
    Image _playerSetHand;

    [SerializeField]
    [Header("��D�̉摜")]
    Image[] _playerHands;

    public void ChangeLifeText(int value)//�e�L�X�g�Ɉ�������
    {
        _lifeText.text = value.ToString();
    }

    public void ChangeShieldText(int value)//�e�L�X�g�Ɉ�������
    {
        _shieldText.text = value.ToString();
    }

    public void ChangeReserveText(int value)//�e�L�X�g�Ɉ�������
    {
        _playerReserve.text = value.ToString();
    }

    public void ChangeHandsImage(Image[] img)//Image�Ɉ�������
    {
        for (int i = 0; i < ConstParameter.FIVE; i++)
        {
            _playerHands[i] = img[i];
        }

    }

    public void ChangeSetHandImage(Image img)//Image�Ɉ�������
    {
        _playerSetHand = img;
    }

}
