using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField]
    [Header("シールド破壊数追加ボタン")]
    Button _addBreakCountButton;

    [SerializeField]
    [Header("シールド破壊数削除ボタン")]
    Button _removeBreakCountButton;

    [SerializeField]
    [Header("シールド破壊数決定ボタン")]
    Button _desideBreakCountButton;

    [SerializeField]
    [Header("カードを戻すボタン")]
    Button _cardBackButton;

    [SerializeField]
    [Header("カードを戻さないボタン")]
    Button _notCardBackButton;

    /// <summary>
    /// 手札選択フェーズの手札を表示する関数
    /// </summary>
    public void SelectAllHand()
    {

    }

    /// <summary>
    /// 選択できるカードを表示する関数
    /// </summary>
    public void SelectRSPCard(IReadOnlyList<PlayerHand> playerHands)
    {

    }

    /// <summary>
    /// 選択できるチョキのカードを表示する関数
    /// </summary>
    /// <param name="scissorsHands"></param>
    public void SelectCardForLeaderCardShaman(IEnumerable<PlayerHand> scissorsHands)
    {

    }

    #region FPaperCardJudgmentOfAigis

    /// <summary>
    /// 自身のシールド数を表示する関数
    /// </summary>
    /// <param name="shildCount"></param>
    public void SelectCardForFPaperCardJudgmentOfAigis
        (int shildCount,UnityAction addMethod, UnityAction removeMethod, UnityAction desideMethod)
    {
        _addBreakCountButton.onClick.AddListener(addMethod);
        _removeBreakCountButton.onClick.AddListener(removeMethod);
        _desideBreakCountButton.onClick.AddListener(desideMethod);

        _addBreakCountButton.gameObject.SetActive(true);
        _removeBreakCountButton.gameObject.SetActive(true);
        _desideBreakCountButton.gameObject.SetActive(true);
    }

    public void InactiveBreakCountButton
        (UnityAction addMethod, UnityAction removeMethod, UnityAction desideMethod)
    {
        _addBreakCountButton.onClick.RemoveListener(addMethod);
        _removeBreakCountButton.onClick.RemoveListener(removeMethod);
        _desideBreakCountButton.onClick.RemoveListener(desideMethod);

        _addBreakCountButton.gameObject.SetActive(false);
        _removeBreakCountButton.gameObject.SetActive(false);
        _desideBreakCountButton.gameObject.SetActive(false);
    }

    #endregion

    /// <summary>
    /// 敵のカードを裏向きで表示する関数
    /// </summary>
    /// <param name="enemyHands"></param>
    public void SelectCardForPaperCardDrainShield(int enemyHandCount)
    {
        
    }

    #region ScissorsCardChainAx

    /// <summary>
    /// カードを戻せるかどうかの選択ボタンを表示する関数
    /// </summary>
    public void SelectCardForScissorsCardChainAx(UnityAction cardBackMethod, UnityAction notCardBackMethod)
    {
        _cardBackButton.onClick.AddListener(cardBackMethod);
        _notCardBackButton.onClick.AddListener(notCardBackMethod);

        _cardBackButton.gameObject.SetActive(true);
        _notCardBackButton.gameObject.SetActive(true);
    }

    public void InactiveCardBackButton(UnityAction cardBackMethod, UnityAction notCardBackMethod)
    {
        _cardBackButton.gameObject.SetActive(false);
        _notCardBackButton.gameObject.SetActive(false);

        _cardBackButton.onClick.RemoveListener(cardBackMethod);
        _notCardBackButton.onClick.RemoveListener(notCardBackMethod);
    }

    #endregion
}
