using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのデータを持つクラス
/// </summary>
public class PlayerData : MonoBehaviour, IHandCollection, ILifeChange
{
    public int Life => _life;
    public int Shild => _shield;
    public IReadOnlyList<PlayerHand> PlayerHands => _playerHands;
    public IReadOnlyList<PlayerHand> PlayerReserve => _playerReserve;
    public PlayerHand PlayerSetHand => _playerSetHand;

    private int _life;
    private int _shield;

    private List<PlayerHand> _playerHands = new List<PlayerHand>(5);
    private List<PlayerHand> _playerReserve = new List<PlayerHand>(5);

   private PlayerHand _playerSetHand;

    private void Start()
    {
        Init();
    }

    private void Init()// 初期化する関数
    {
        _life = ConstParameter.LIFE_DEFAULT;
        _shield = ConstParameter.ZERO;
    }

    public void SetHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);
        _playerSetHand = playerHand;
    }

    public void SetCardOnReserve()
    {
        _playerReserve.Add(_playerSetHand);
    }

    public void AddHand(PlayerHand playerHand)
    {
        _playerHands.Add(playerHand);
    }

    public void OnReserveHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);
        _playerReserve.Add(playerHand);
    }

    public void HealLife(int heal)
    {
        _life += heal;
    }

    public void ReceiveDamage(int damage)
    {
        if(_shield > ConstParameter.ZERO)
        {
            if(_shield - damage >= ConstParameter.ZERO)
            {
                _shield -= damage;
                return;
            }
            else
            {
                damage -= _shield;
                _shield = ConstParameter.ZERO;
            }
        }
        _life -= damage;
    }

    public void GetShield(int num)
    {
        _shield += num;
    }
}
