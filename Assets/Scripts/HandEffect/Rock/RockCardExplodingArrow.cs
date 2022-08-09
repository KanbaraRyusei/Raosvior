using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 炸裂する矢(グー)
/// 勝利した時相手がシールドトークンを所持しているならさらに1ダメージを与える。
/// </summary>
public class RockCardExplodingArrow : HandEffect,IWin
{
    public const int DAMEGE = 1;

    public override void Effect()
    {

    
    }

    public void Win()
    {
        
    }
}
