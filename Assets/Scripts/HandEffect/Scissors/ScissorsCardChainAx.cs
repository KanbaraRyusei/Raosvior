using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// チェーンアックス(チョキ)
/// 勝利した時このカードを手札に戻せる。
/// </summary>
public class ScissorsCardChainAx : HandEffect
{
    PlayerBase[] _playerBase;

    void Awake()
    {
        _playerBase = FindObjectsOfType<PlayerBase>();
    }

    public override void Effect()
    {

    }
}
