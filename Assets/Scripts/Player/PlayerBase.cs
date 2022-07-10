using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IAddHand, IReceiveDamage
{
    [SerializeField]
    [Header("PlayerÇÃî‘çÜ")]
    BattleManager.Player _player = BattleManager.Player.Player1;

    int _life;
    int _shield;
    List<PlayerHand> _playerHands = new List<PlayerHand>();

    public void AddHand(PlayerHand playerHand)
    {
        _playerHands.Add(playerHand);
        if(_playerHands.Count < 5)
        {

        }
    }

    public void ReceiveDamage(int damage)
    {
        if(_shield > 0)
        {
            if(_shield - damage > 0)
            {
                _shield -= damage;
            }
        }
    }

    public void OnClick()
    {
        BattleManager.Instance.SelectedNotification(_player);
    }
}
