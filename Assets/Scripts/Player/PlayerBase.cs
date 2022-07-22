using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IAddHand, IDeleteHand, IReceiveDamage
{
    public int Life => _life;
    public int Shild => _shield;

    [SerializeField]
    [Header("Player‚Ì”Ô†")]
    BattleManager.Player _player = BattleManager.Player.Player1;

    private int _life;
    private int _shield;
    List<PlayerHand> _playerHands = new List<PlayerHand>();

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _life = BattleManager.Instance.InitialLife;
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
}
