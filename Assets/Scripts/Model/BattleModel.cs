using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BattleModel : MonoBehaviour
{
    public int InitialLife => _initialLife;
    public int InitialHandNum => _initialHandNum;

    [SerializeField]
    [Header("Player‚Ì‰Šúƒ‰ƒCƒt")]
    int _initialLife;

    [SerializeField]
    [Header("Player‚Ì‰ŠúèD–‡”")]
    int _initialHandNum;

    public IEnumerator WaitingForPreparation(bool flag)
    {
        Debug.Log("1‰ñ–Ú");
        while (flag)
        {
            yield return null;
        }
        BattleJudgement();
    }

    private void BattleJudgement()
    {
        Debug.Log("2‰ñ–Ú");
        
    }
}
