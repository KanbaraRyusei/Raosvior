using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour, IAddHand, IDeleteHand, IReceiveDamage, IGetShield
{
    public int Life => _life;
    public int Shild => _shield;
    public bool IsRedraw => _isRedraw;
    public IReadOnlyList<PlayerHand> PlayerHands => _playerHands;
    public IReadOnlyList<PlayerHand> PlayerTrashs => _playerTrashs;

    private int _life;
    private int _shield;

    private bool _isRedraw = false;

    List<PlayerHand> _playerHands = new List<PlayerHand>();
    List<PlayerHand> _playerTrashs = new List<PlayerHand>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _life = ConstParameter.LIFE_DEFAULT;
        _shield = ConstParameter.ZERO;
    }

    public void AddHand(PlayerHand playerHand)
    {
        _playerHands.Add(playerHand);
    }

    public void DeleteHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);
        _playerTrashs.Add(playerHand);
        if(_playerHands.Count == ConstParameter.ZERO)
        {
            _isRedraw = true;
        }
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
