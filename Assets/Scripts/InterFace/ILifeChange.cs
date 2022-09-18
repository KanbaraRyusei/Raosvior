using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILifeChange
{
    void HealLife(int heal);
    void ReceiveDamage(int damage);
    void GetShield(int num);
}
