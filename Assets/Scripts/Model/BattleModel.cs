using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BattleModel : MonoBehaviour
{
    public int InitialLife => _initialLife;
    public int InitialHandNum => _initialHandNum;

    [SerializeField]
    [Header("Player�̏������C�t")]
    int _initialLife;

    [SerializeField]
    [Header("Player�̏�����D����")]
    int _initialHandNum;

    public IEnumerator WaitingForPreparation(bool flag)
    {
        Debug.Log("1���");
        while (flag)
        {
            yield return null;
        }
        BattleJudgement();
    }

    private void BattleJudgement()
    {
        Debug.Log("2���");
        
    }
}
