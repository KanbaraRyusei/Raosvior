using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandCollection
{
    void SetHand(PlayerHand playerHand);
    void SetCardOnReserve();
    void AddHand(PlayerHand playerHand);
    void OnReserveHand(PlayerHand playerHand);
}
