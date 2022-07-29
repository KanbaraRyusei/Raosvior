using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class BattleManager : SingletonMonoBehaviour<BattleManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    //ƒ^ƒvƒ‹‚Ì‘‚«•ûƒƒ‚
    //private (int, bool) TupleTest(int num)
    //{
    //    int a = num;
    //    bool b = true;
    //    if (num > 5)
    //    {
    //        b = false;
    //    }
    //    return (a, b);
    //}

    //private int GetTupleTest((int, bool) tuple)
    //{
    //    if (tuple.Item2)
    //    {
    //        return 5;
    //    }
    //    return tuple.Item1;
    //}

    //private void Tuple()
    //{
    //    GetTupleTest(TupleTest(0));
    //}
}
