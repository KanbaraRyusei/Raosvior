using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�̃f�[�^�����N���X
/// </summary>
public class PlayerData : MonoBehaviour, IHandCollection, ILifeChange
{
    /// <summary>
    /// �v���C���[�̃��C�t�̓ǂݎ���p�v���p�e�B
    /// </summary>
    public int Life => _life;
    /// <summary>
    /// �v���C���[�̃V�[���h�̓ǂݎ���p�v���p�e�B
    /// </summary>
    public int Shild => _shield;
    /// <summary>
    /// �v���C���[�̎�D�̓ǂݎ���p�v���p�e�B
    /// �L���X�g���ĕύX���悤�Ƃ���z�͎E���̂ŗv����
    /// </summary>
    public IReadOnlyList<PlayerHand> PlayerHands => _playerHands;
    /// <summary>
    /// �v���C���[�̃��U�[�u�̓ǂݎ���p�v���p�e�B
    /// �L���X�g���ĕύX���悤�Ƃ���z�͎E���̂ŗv����
    /// </summary>
    public IReadOnlyList<PlayerHand> PlayerReserve => _playerReserve;
    /// <summary>
    /// ��ɃZ�b�g����J�[�h�̓ǂݎ���p�v���p�e�B
    /// </summary>
    public PlayerHand PlayerSetHand => _playerSetHand;

    /// <summary>
    /// �v���C���[�̃��C�t
    /// </summary>
    private int _life;
    /// <summary>
    /// �v���C���[�̃V�[���h
    /// </summary>
    private int _shield;

    /// <summary>
    /// �v���C���[�̎�D
    /// ��������܂��Ă��邽�߁A�v�f�����w�肵�ď����y������
    /// </summary>
    private List<PlayerHand> _playerHands = new List<PlayerHand>(5);
    /// <summary>
    /// �v���C���[�̃��U�[�u
    /// ��������܂��Ă��邽�߁A�v�f�����w�肵�ď����y������
    /// </summary>
    private List<PlayerHand> _playerReserve = new List<PlayerHand>(5);

    /// <summary>
    /// ��ɃZ�b�g����J�[�h
    /// </summary>
    private PlayerHand _playerSetHand;

    private void Start()
    {
        Init();
    }

    private void Init()// ����������֐�
    {
        _life = ConstParameter.LIFE_DEFAULT;
        _shield = ConstParameter.ZERO;
    }

    public void SetHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);// ��D��List����J�[�h���폜����
        _playerSetHand = playerHand;// ��ɃJ�[�h���Z�b�g
    }

    public void SetCardOnReserve()
    {
        _playerReserve.Add(_playerSetHand);// �Z�b�g����Ă���J�[�h�����U�[�u�ɑ���
        _playerSetHand = null;// �Z�b�g����Ă����J�[�h������
    }

    public void AddHand(PlayerHand playerHand)
    {
        _playerHands.Add(playerHand);// ��D�ɃJ�[�h��������
    }

    public void OnReserveHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);// ��D����J�[�h���폜
        _playerReserve.Add(playerHand);// ���U�[�u�ɃJ�[�h��ǉ�
    }

    public void HealLife(int heal)
    {
        _life += heal;// ���C�t���� ������Ȃ����ߗ]�v�ȏ����͂Ȃ�
    }

    public void ReceiveDamage(int damage)
    {
        if(_shield > ConstParameter.ZERO)// �V�[���h�����邩�ǂ�������
        {
            if(_shield - damage >= ConstParameter.ZERO)// �V�[���h�̐����_���[�W���傫��������
            {
                _shield -= damage;// �V�[���h�����
                return;
            }
            else// �V�[���h�̐����_���[�W��菭�Ȃ�������
            {
                damage -= _shield;// �_���[�W���V�[���h�̐����炷
                _shield = ConstParameter.ZERO;// �V�[���h��0�ɂ���
            }
        }
        _life -= damage;// ���C�t�����炷
    }

    public void GetShield(int num)
    {
        _shield += num;// �V�[���h��ǉ� ������Ȃ����ߗ]�v�ȏ����͂Ȃ�
    }
}
