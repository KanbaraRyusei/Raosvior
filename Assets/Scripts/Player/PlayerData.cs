using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

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

    private const int LIFE_DEFAULT = 5;
    private const int ZERO = 0;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _life = LIFE_DEFAULT;
        _shield = ZERO;
    }

    public void AddHand(PlayerHand playerHand)
    {
        _playerHands.Add(playerHand);
    }

    public void DeleteHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);
        _playerTrashs.Add(playerHand);
        if(_playerHands.Count == ZERO)
        {
            _isRedraw = true;
        }
    }

    public void ReceiveDamage(int damage)
    {
        if(_shield > ZERO)
        {
            if(_shield - damage > ZERO)
            {
                _shield -= damage;
                return;
            }
            else
            {
                damage -= _shield;
                _shield = ZERO;
            }
        }
        _life -= damage;
    }

    public void GetShield(int num)
    {
        _shield += num;
    }
}
