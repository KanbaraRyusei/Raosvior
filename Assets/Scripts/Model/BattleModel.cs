using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BattleModel : MonoBehaviour
{
    public int InitialLife => _initialLife;
    public int InitialHandNum => _initialHandNum;

    [SerializeField]
    [Header("Playerの初期ライフ")]
    int _initialLife;

    [SerializeField]
    [Header("Playerの初期手札枚数")]
    int _initialHandNum;

    public IEnumerator WaitingForPreparation(bool flag)
    {
        Debug.Log("1回目");
        while (flag)
        {
            yield return null;
        }
        BattleJudgement();
    }

    private void BattleJudgement()
    {
        Debug.Log("2回目");
        
    }
}
