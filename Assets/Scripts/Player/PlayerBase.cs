using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBase : MonoBehaviour, IAddHand, IDeleteHand, IReceiveDamage, IGetShield
{
    public int Life => _life;
    public int Shild => _shield;
    public List<PlayerHand> PlayerHands => _playerHands;

    private int _life;
    private int _shield;
    List<PlayerHand> _playerHands = new List<PlayerHand>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _life = 5;
        _shield = 0;
    }

    public void AddHand(PlayerHand playerHand)
    {
        _playerHands.Add(playerHand);
    }

    public void DeleteHand(PlayerHand playerHand)
    {
        _playerHands.Remove(playerHand);
    }

    public void ReceiveDamage(int damage)
    {
        if(_shield > 0)
        {
            if(_shield - damage > 0)
            {
                _shield -= damage;
                return;
            }
            else
            {
                damage -= _shield;
                _shield = 0;
            }
        }
        _life -= damage;
    }

    public void GetShield(int num)
    {
        _shield += num;
    }
}
